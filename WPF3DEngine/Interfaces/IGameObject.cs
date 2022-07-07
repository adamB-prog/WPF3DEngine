using GlmNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF3DEngine.Classes;

namespace WPF3DEngine.Interfaces
{
    interface IGameObject
    {
        VertexArray Vao { get; }
        
        IndexBuffer Ibo { get; }
        ShaderProgram Program { get; }

        Texture Texture { get; }

        public mat4 ModelMatrix { get; }
    }
}
