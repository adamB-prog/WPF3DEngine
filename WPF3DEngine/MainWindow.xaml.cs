using System;
using System.Text;
using System.Windows;
using SharpGL;
using System.IO;
using System.Diagnostics;
using WPF3DEngine.Classes;
using GlmNet;
using WPF3DEngine.Interfaces;
using System.Threading.Tasks;
using System.Media;
using System.Windows.Media;
using System.Windows.Input;
using System.Numerics;
using System.Collections.Generic;

namespace WPF3DEngine
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }






        Renderer renderer;

        List<GameObject> gameObjects = new List<GameObject>();

        Camera cam = new Camera(new vec3(0, 0, 0), new vec3(0, 0, 0));
        Stopwatch watch = new Stopwatch();


        vec3 lightPos;
        
        private void OpenGLControl_Resized(object sender, SharpGL.WPF.OpenGLRoutedEventArgs args)
        {
            OpenGL gl = args.OpenGL;


            

        }
        
        private void OpenGLControl_OpenGLInitialized(object sender, SharpGL.WPF.OpenGLRoutedEventArgs args)
        {
            

           
            //OpenGL gl = args.OpenGL;
           

            //gl.Enable(OpenGL.GL_DEPTH_TEST);
            //gl.Enable(OpenGL.GL_BLEND);
            //gl.BlendFunc(OpenGL.GL_SRC_ALPHA, OpenGL.GL_ONE_MINUS_SRC_ALPHA);


            //renderer = new Renderer(gl,(float)ActualWidth, (float)ActualHeight);
            






            //string[] paths = { "asd.obj", "cube.obj", "monkey.obj", "torus.obj", "korvalami.obj"};
            //Texture texture = new Texture(gl, "src/default.png");
            //ShaderProgram program = new ShaderProgram(gl, File.ReadAllText("Shaders/vtextureShader.vert"), File.ReadAllText("Shaders/ftextureShader.frag"));
            //Random rnd = new Random();
            //for (int i = 0; i < 100; i++)
            //{
            //    gameObjects.Add(new GameObject(gl, texture, program, paths[i % 5]));
            //    gameObjects[i].MoveTo(new vec3(rnd.Next(-10, 10), rnd.Next(-10, 10), rnd.Next(-10, 10)));

            //}


            //Task task = new Task(() =>
            //{
            //    float t = 0;
            //    while (true)
            //    {
            //        t += 0.01f;
            //        lightPos = new vec3(0, (float)Math.Sin(t), (float)Math.Cos(t));
            //        System.Threading.Thread.Sleep(1000 / 60);
            //    }
            //});
            //task.Start();

        }


        
       

    
        float pow = 0.1f;
        private void OpenGLControl_OpenGLDraw(object sender, SharpGL.WPF.OpenGLRoutedEventArgs args)
        {
            OpenGL gl = args.OpenGL;

            watch.Start();

            UpdateLogic();


           
            UpdateDrawing(gl);


            watch.Stop();
            this.Title = string.Format("DrawTime: {0}", watch.Elapsed);
            watch.Reset();
        }


        
       
        private void UpdateDrawing(OpenGL gl)
        {
            renderer.Clear();


           


            foreach (IGameObject gameObject in gameObjects)
            {                
                gameObject.Program.SetUniformMat4f(gl, "projection", renderer.ProjectionMatrix);
                gameObject.Program.SetUniformMat4f(gl, "view", cam.ViewMatrix);
                gameObject.Program.SetUniformMat4f(gl, "model", gameObject.ModelMatrix);
                gameObject.Program.SetUniform3f(gl, "viewPos", cam.Position);

                gameObject.Program.SetUniform3f(gl, "lightPos", lightPos);
                gameObject.Texture.Bind();
                renderer.Draw(gameObject);
            }
            
            

            
        }
        
        private void UpdateLogic()
        {


            if (Keyboard.IsKeyDown(Key.W))
            {

                cam.MoveCamera(cam.Forward, pow);
            }
            if (Keyboard.IsKeyDown(Key.A))
            {

                cam.MoveCamera(cam.Right, pow);
            }
            if (Keyboard.IsKeyDown(Key.S))
            {

                cam.MoveCamera(cam.Forward, -pow);
            }
            if (Keyboard.IsKeyDown(Key.D))
            {

                cam.MoveCamera(cam.Right, -pow);
            }

            if (Keyboard.IsKeyDown(Key.Left))
            {

                cam.TurnCamera(new vec3(0, 1, 0), pow * 10);
            }
            if (Keyboard.IsKeyDown(Key.Right))
            {

                cam.TurnCamera(new vec3(0, 1, 0), -pow * 10);
            }
            if (Keyboard.IsKeyDown(Key.Up))
            {

                cam.TurnCamera(new vec3(1, 0, 0), pow * 10);
            }

            if (Keyboard.IsKeyDown(Key.Down))
            {

                cam.TurnCamera(new vec3(1, 0, 0), -pow * 10);
            }
        }

        private void OpenGLControl_Loaded(object sender, RoutedEventArgs e)
        {
            OpenGL gl = OpenGLControl.OpenGL;


            gl.Enable(OpenGL.GL_DEPTH_TEST);
            gl.Enable(OpenGL.GL_BLEND);
            gl.BlendFunc(OpenGL.GL_SRC_ALPHA, OpenGL.GL_ONE_MINUS_SRC_ALPHA);


            renderer = new Renderer(gl, (float)ActualWidth, (float)ActualHeight);




            gameObjects.Add(new GameObject(gl, "cube.obj"));


            string[] paths = { "asd.obj", "cube.obj", "monkey.obj", "torus.obj", "korvalami.obj" };
            Texture texture = new Texture(gl, "src/default.png");
            ShaderProgram program = new ShaderProgram(gl, File.ReadAllText("Shaders/vtextureShader.vert"), File.ReadAllText("Shaders/ftextureShader.frag"));
            Random rnd = new Random();
            for (int i = 0; i < 100; i++)
            {
                gameObjects.Add(new GameObject(gl, texture, program, paths[i % 5]));
                gameObjects[i].MoveTo(new vec3(rnd.Next(-10, 10), rnd.Next(-10, 10), rnd.Next(-10, 10)));

            }


            Task task = new Task(() =>
            {
                float t = 0;
                while (true)
                {
                    t += 0.01f;
                    lightPos = new vec3(0, (float)Math.Sin(t), (float)Math.Cos(t));
                    System.Threading.Thread.Sleep(1000 / 60);
                }
            });
            task.Start();
        }
    }
}
