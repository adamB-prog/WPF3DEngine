using SharpGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF3DEngine.Classes
{
    public class VertexArray
    {
        uint[] vao;
        public VertexArray()
        {
            vao = new uint[1];

        }

        public void Create(OpenGL gl)
        {
            gl.GenVertexArrays(1, vao);
        }


        public void Bind(OpenGL gl)
        {
            gl.BindVertexArray(vao[0]);
        }

        public void Unbind(OpenGL gl)
        {
            gl.BindVertexArray(0);
        }

    }
}
