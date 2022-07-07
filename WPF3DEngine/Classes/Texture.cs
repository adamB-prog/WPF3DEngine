using SharpGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF3DEngine.Classes
{
    public class Texture
    {
        OpenGL gl;
        uint[] textureID = new uint[1];
        public Texture(OpenGL gl, string path)
        {
            Bitmap img = new Bitmap(path);

            this.gl = gl;

            gl.GenTextures(1, textureID);
            gl.BindTexture(OpenGL.GL_TEXTURE_2D, textureID[0]);

            gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_MIN_FILTER, OpenGL.GL_LINEAR);
            gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_MAG_FILTER, OpenGL.GL_LINEAR);

            gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_WRAP_S, OpenGL.GL_CLAMP);
            gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_WRAP_T, OpenGL.GL_CLAMP);

            byte[] pixels = BitmapToByteArray(img);



            gl.TexImage2D(OpenGL.GL_TEXTURE_2D, 0, OpenGL.GL_RGBA8, img.Width, img.Height, 0, OpenGL.GL_RGBA, OpenGL.GL_UNSIGNED_BYTE, pixels);

            gl.BindTexture(OpenGL.GL_TEXTURE_2D, 0);
        
        
        
        }




        public void Bind(uint slot = 0)
        {
            gl.ActiveTexture(OpenGL.GL_TEXTURE0 + slot);
            gl.BindTexture(OpenGL.GL_TEXTURE_2D, textureID[0]);
        }

        public void Unbind()
        {
            gl.BindTexture(OpenGL.GL_TEXTURE_2D, 0);

        }







        private byte[] BitmapToByteArray(Bitmap img)
        {
            List<byte> bytes = new List<byte>();

            for (int x = 0; x < img.Height; x++)
            {
                for (int y = img.Width - 1; y >= 0; y--)
                {
                    Color pixel = img.GetPixel(y, x);
                    byte red = pixel.R;
                    byte green = pixel.G;
                    byte blue = pixel.B;
                    byte alpha = pixel.A;

                    
                    
                    
                    bytes.Add(alpha);
                    bytes.Add(blue);
                    bytes.Add(green);
                    bytes.Add(red);



                }
            }
            //TODO: do a mirror on the Y
            bytes.Reverse();
            return bytes.ToArray();
        }

    }
}
