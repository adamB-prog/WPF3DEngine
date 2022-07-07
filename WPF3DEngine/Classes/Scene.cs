using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF3DEngine.Classes
{
    class Scene
    {
        public List<GameObject> GameObjects { get; private set; }

        public List<Renderer> Renderers { get; private set; }

        public List<Camera> Cameras { get; private set; }


        public int ActiveCameraIndex { 
            get
            {
                return ActiveCameraIndex;
            }
            set
            {
                if (value < Cameras.Count && value >= 0)
                {
                    ActiveCameraIndex = value;
                }
            } 
        }

        public int ActiveRendererIndex
        {
            get
            {
                return ActiveRendererIndex;
            }
            set
            {
                if (value < Renderers.Count && value >= 0)
                {
                    ActiveRendererIndex = value;
                }
            }
        }


        public Scene()
        {

        }
        public Scene(GameObject gameObject, Renderer renderer, Camera camera)
        {
            this.GameObjects = new List<GameObject>() { gameObject };
            this.Renderers = new List<Renderer>() { renderer };
            this.Cameras = new List<Camera>() { camera };
        }


        public void AddGameObject(GameObject gameObject)
        {
           
        }
    }
}
