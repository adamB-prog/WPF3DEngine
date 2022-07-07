using SharpGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF3DEngine.Classes
{
    public class VertexBuffer
    {
        uint[] vbo;

        public VertexBuffer()
        {
            vbo = new uint[1];
            
        }

        public void Create(OpenGL gl)
        {
            gl.GenBuffers(1, vbo);
        }


        public void Bind(OpenGL gl)
        {
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, vbo[0]);

        }
        public void Unbind(OpenGL gl)
        {
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, 0);
        }

        public void SetBufferData(OpenGL gl, float[] data, uint usage)
        {
            gl.BufferData(OpenGL.GL_ARRAY_BUFFER, data, usage);
        }
        public void SetBufferData(OpenGL gl, ushort[] data, uint usage)
        {
            gl.BufferData(OpenGL.GL_ARRAY_BUFFER, data, usage);
        }
        

        public void VertexAttribPointer(OpenGL gl, uint index, int size, uint type, bool normalized, int stride, IntPtr pointer)
        {

            gl.VertexAttribPointer(index, size, OpenGL.GL_FLOAT, false, stride, pointer);
        }
        public void EnableVertexAttribArray(OpenGL gl, uint index)
        {
            
            gl.EnableVertexAttribArray(index);
        }
        public void DisableVertexAttribArray(OpenGL gl, uint index)
        {
            gl.DisableVertexAttribArray(index);
        }
    }
}
