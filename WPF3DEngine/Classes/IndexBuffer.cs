using SharpGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF3DEngine.Classes
{
    public class IndexBuffer
    {
        uint[] ibo;
        public uint Count { get; private set; }


        public IndexBuffer()
        {
            ibo = new uint[1];
        }

        public void Create(OpenGL gl)
        {
            gl.GenBuffers(1, ibo);
        }

        public void Bind(OpenGL gl)
        {
            gl.BindBuffer(OpenGL.GL_ELEMENT_ARRAY_BUFFER, ibo[0]);
        }
        public void Unbind(OpenGL gl)
        {
            gl.BindBuffer(OpenGL.GL_ELEMENT_ARRAY_BUFFER, 0);
        }

        public void SetBufferData(OpenGL gl, uint[] indices, uint usage)
        {
            Count = (uint)indices.Length;
            gl.BufferData(OpenGL.GL_ELEMENT_ARRAY_BUFFER, indices, usage);
        }
        
    }
}
