using GlmNet;
using SharpGL;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Media.Imaging;
using WPF3DEngine.Interfaces;

namespace WPF3DEngine.Classes
{
    public class GameObject : IGameObject
    {
        public VertexArray Vao { get; private set; }

        

        public IndexBuffer Ibo { get; private set; }

        public ShaderProgram Program { get; private set; }

        public Texture Texture { get; private set; }


        public mat4 ModelMatrix { get; private set; }

        public GameObject(OpenGL gl, Texture texture, ShaderProgram program, string path)
        {
            LoadOBJ(gl, path);
            Program = program;

            Texture = texture;


            ModelMatrix = glm.translate(mat4.identity(), new vec3(0, 0, 0));
        }
        public GameObject(OpenGL gl, string path)
        {
            LoadOBJ(gl, path);
            Program = new ShaderProgram(gl, File.ReadAllText("Shaders/vtextureShader.vert"), File.ReadAllText("Shaders/ftextureShader.frag"));

            Texture = new Texture(gl, "src/default.png");


            ModelMatrix = glm.translate(mat4.identity(), new vec3(0, 0, 0));
        }

        public void MoveTo(vec3 position)
        {
            ModelMatrix = glm.translate(mat4.identity(), position);
        }

        public void RotateTo(float angle, vec3 rotationVector)
        {
            ModelMatrix *= glm.rotate(ConvertToRadians(angle), rotationVector);
        }

        private float ConvertToRadians(float angle)
        {
            return (float)((Math.PI / 180) * angle);
        }

        //TODO: multiple object from 1 file
        private void LoadOBJ(OpenGL gl, string path)
        {/*
          * Goes through the file get vertices, uvs, normals, then pair them into the out_verices,then use the ordered Pairs to build the indices.
          * 
          * 
          * 
          */

            CultureInfo ci = (CultureInfo)CultureInfo.CurrentCulture.Clone();
            ci.NumberFormat.CurrencyDecimalSeparator = ".";
            string[] file = File.ReadAllLines(path);
            
            if (file.Equals(null))
            {
                throw new ArgumentNullException("Fájl nem létezik!");
            }

            Vao = new VertexArray();
            VertexBuffer vbo = new VertexBuffer();
            Ibo = new IndexBuffer();

            Vao.Create(gl);
            Vao.Bind(gl);
            vbo.Create(gl);
            vbo.Bind(gl);
            Ibo.Create(gl);
            Ibo.Bind(gl);

            //Initial things DONE
            //Time to read the datas

            List<vec3> vertices = new List<vec3>();
            List<vec2> uvs = new List<vec2>();
            List<vec3> normals = new List<vec3>();
            List<Pairs> pairs = new List<Pairs>();
            List<float> out_vertices = new List<float>();
            List<uint> indices = new List<uint>();


            foreach (var x in file)
            {
                string[] oneLineDatas = x.Split(' ');
                if (oneLineDatas[0].Equals("v"))
                {
                    vertices.Add(
                        new vec3(
                            float.Parse(
                                oneLineDatas[1], NumberStyles.Any, ci), 
                            float.Parse(
                                oneLineDatas[2], NumberStyles.Any, ci), 
                            float.Parse(
                                oneLineDatas[3], NumberStyles.Any, ci)));
                }
                else if (oneLineDatas[0].Equals("vt"))
                {
                    uvs.Add(
                        new vec2(
                            float.Parse(
                                oneLineDatas[1], NumberStyles.Any, ci), 
                            float.Parse(
                                oneLineDatas[2], NumberStyles.Any, ci)
                            )
                        );
                }
                else if (oneLineDatas[0].Equals("vn"))
                {
                    normals.Add(
                        new vec3(
                            float.Parse(
                                oneLineDatas[1], NumberStyles.Any, ci),
                            float.Parse(
                                oneLineDatas[2], NumberStyles.Any, ci),
                            float.Parse(
                                oneLineDatas[3], NumberStyles.Any, ci)));
                }
                else if (oneLineDatas[0].Equals("f"))
                {
                    
                    for (int i = 1; i < oneLineDatas.Length; i++)
                    {
                        string[] pairDatas = oneLineDatas[i].Split('/');

                        Pairs pair = new Pairs(pairDatas[0], pairDatas[1], pairDatas[2]);

                        if (!pairs.Contains(pair))
                        {
                            pairs.Add(pair);
                        }
                        


                    }

                }
            }
            Pairs[] Pairs = pairs.OrderBy(x => x.vertID).ThenBy(x => x.uvID).ThenBy(x => x.normalID).ToArray();

            //Pairing the pos to uvs
            foreach (var x in Pairs)
            {
                out_vertices.Add(vertices[x.vertID - 1].x);
                out_vertices.Add(vertices[x.vertID - 1].y);
                out_vertices.Add(vertices[x.vertID - 1].z);
                out_vertices.Add(uvs[x.uvID - 1].x);
                out_vertices.Add(uvs[x.uvID - 1].y);
                out_vertices.Add(normals[x.normalID - 1].x);
                out_vertices.Add(normals[x.normalID - 1].y);
                out_vertices.Add(normals[x.normalID - 1].z);
            }

            
            foreach (var x in file)
            {
                string[] oneLineDatas = x.Split(' ');
                if (oneLineDatas[0].Equals("f"))
                {

                    for (int i = 1; i < oneLineDatas.Length; i++)
                    {
                        string[] pairDatas = oneLineDatas[i].Split('/');

                        for (int j = 0; j < Pairs.Length; j++)
                        {
                            if (Convert.ToInt32(pairDatas[0]) == Pairs[j].vertID && Convert.ToInt32(pairDatas[1]) == Pairs[j].uvID && Convert.ToInt32(pairDatas[2]) == Pairs[j].normalID)
                            {
                                indices.Add((uint)j);
                            }
                        }



                    }

                }
            }

            vbo.SetBufferData(gl, out_vertices.ToArray(), OpenGL.GL_STATIC_DRAW);

            vbo.VertexAttribPointer(gl, 0, 3, OpenGL.GL_FLOAT, false, sizeof(float) * 8, IntPtr.Zero);
            vbo.VertexAttribPointer(gl, 1, 2, OpenGL.GL_FLOAT, false, sizeof(float) * 8, new IntPtr(sizeof(float) * 3));
            vbo.VertexAttribPointer(gl, 2, 3, OpenGL.GL_FLOAT, false, sizeof(float) * 8, new IntPtr(sizeof(float) * 5));
            vbo.EnableVertexAttribArray(gl, 0);
            vbo.EnableVertexAttribArray(gl, 1);
            vbo.EnableVertexAttribArray(gl, 2);

            Ibo.SetBufferData(gl, indices.ToArray(), OpenGL.GL_STATIC_DRAW);

            
            
        }
    }
}
