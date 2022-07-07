using GlmNet;
using SharpGL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF3DEngine.Classes
{
    public class ShaderProgram
    {
        uint shaderID;
        
        public ShaderProgram(OpenGL gl, string vertexShaderSource, string fragmentShaderSource)//, string geometryShaderSource = null)
        {
            shaderID = CreateShader(gl, vertexShaderSource, fragmentShaderSource);

        }
        //~ShaderProgram()
        //{
            
        //}

        private  uint CreateShader(OpenGL gl, string vertexShader, string fragmentShader)
        {
            uint program = gl.CreateProgram();

            uint vs = CompileShader(gl, OpenGL.GL_VERTEX_SHADER, vertexShader);
            uint fs = CompileShader(gl, OpenGL.GL_FRAGMENT_SHADER, fragmentShader);
            //uint gs = CompileShader(gl, OpenGL.GL_GEOMETRY_SHADER, geometryShader);

            gl.AttachShader(program, vs);
            gl.AttachShader(program, fs);
            //gl.AttachShader(program, gs);

            gl.LinkProgram(program);

            gl.ValidateProgram(program);


            gl.DeleteShader(vs);
            gl.DeleteShader(fs);
            //gl.DeleteShader(gs);


            return program;
        }

        private uint CompileShader(OpenGL gl, uint type, string source)
        {
            uint id = gl.CreateShader(type);

            gl.ShaderSource(id, source);

            gl.CompileShader(id);

            StringBuilder glError = new StringBuilder();

            gl.GetShaderInfoLog(id, 1000, new IntPtr(), glError);


            Debug.WriteLine(glError);//hibakiírás







            return id;
        }

        public void Bind(OpenGL gl)
        {
            gl.UseProgram(shaderID);
        }
        public void Unbind(OpenGL gl)
        {
            gl.UseProgram(0);
        }



        public void SetUniform1i(OpenGL gl, string name,int value)
        {
            int loc = gl.GetUniformLocation(shaderID, name);
            gl.Uniform1(loc, value);
        }

        public void SetUniform4f(OpenGL gl, string name, float v0, float v1, float v2, float v3)
        {
            int loc = gl.GetUniformLocation(shaderID, name);


            gl.Uniform4(loc, v0, v1, v2, v3);
        }

        public void SetUniformMat4f(OpenGL gl, string name, mat4 matrix)
        {
            int loc = gl.GetUniformLocation(shaderID, name);

            float[] asd = matrix.to_array();
            gl.UniformMatrix4(loc, 1, false, asd);


        }

        public void SetUniform1f(OpenGL gl, string name, float v0)
        {
            int loc = gl.GetUniformLocation(shaderID, name);

            gl.Uniform1(loc, v0);
        }

        public void SetUniform3f(OpenGL gl, string name, vec3 pos)
        {
            int loc = gl.GetUniformLocation(shaderID, name);

            gl.Uniform3(loc, pos.x, pos.y, pos.z);
        }
    }
}
