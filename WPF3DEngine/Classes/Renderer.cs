using GlmNet;
using SharpGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPF3DEngine.Interfaces;

namespace WPF3DEngine.Classes
{
    class Renderer
    {
        OpenGL gl;
        float width = 1280f;
        float height = 704.04f;
        float fieldOfView = 60f;
        public mat4 ProjectionMatrix { get; private set; }
        public Renderer(OpenGL gl, float width, float height)
        {
            this.gl = gl;
            //this.width = width;
            //this.height = height;

            ModifyProjectionMatrix(gl,fieldOfView,this.width, this.height);
        }


        public void Clear()
        {
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
        }
        public void Draw(IGameObject go)
        {
            

            //go.program.SetUniformMat4f(gl, "u_MVP", projectionMatrix);
            go.Vao.Bind(gl);

            go.Program.Bind(gl);
            
            gl.DrawElements(OpenGL.GL_TRIANGLES, (int)go.Ibo.Count, OpenGL.GL_UNSIGNED_INT, IntPtr.Zero);
            
        }




        public void ModifyProjectionMatrix(OpenGL gl, float fieldofView, float screenWidth, float screenHeight)
        {
            ProjectionMatrix = glm.perspectiveFov(fieldofView / 100, screenWidth, screenHeight, 0.1f, 1000f);
        }
    }
}
