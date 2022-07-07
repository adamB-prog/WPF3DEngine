using GlmNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF3DEngine.Classes
{
    class Camera
    {
        public vec3 Position { get; private set; }
        public vec3 Rotation { get; private set; }

        public vec3 Forward { get; private set; }
        public vec3 Right { get; private set; }

        public mat4 ViewMatrix { get; private set; }

        public Camera(vec3 startPos, vec3 startRot)
        {
            Position = startPos;
            Rotation = startRot;
            
            Forward = new vec3(-(float)Math.Sin(glm.radians(Rotation.y)), 0, (float)Math.Cos(glm.radians(Rotation.y)));


            Right = new vec3((float)Math.Cos(glm.radians(Rotation.y)), 0, (float)Math.Sin(glm.radians(Rotation.y)));

            ViewMatrix = glm.rotate(mat4.identity(), glm.radians(Rotation.x), new vec3(1, 0, 0));
            ViewMatrix *= glm.rotate(mat4.identity(), glm.radians(Rotation.y), new vec3(0, 1, 0));
            ViewMatrix *= glm.translate(mat4.identity(), Position);
            /*
             * A sorrend fontos!!!!!!!!!!
             * 
             * 
             */
        }
        public void TurnCamera(vec3 axis, float angle)
        {
            Rotation -= axis * angle;
            Rotation = new vec3(Rotation.x % 360, Rotation.y % 360, Rotation.z % 360);
            ReCalculateRelativeVariables();

        }
        public void MoveCamera(vec3 axis, float power)
        {
            Position += axis * power;
            ReCalculateRelativeVariables();
        }
        private void ReCalculateRelativeVariables()
        {
            ViewMatrix = glm.rotate(mat4.identity(), glm.radians(Rotation.x), new vec3(1, 0, 0));
            ViewMatrix *= glm.rotate(mat4.identity(), glm.radians(Rotation.y), new vec3(0, 1, 0));
            ViewMatrix *= glm.translate(mat4.identity(), Position);
            Forward = new vec3(-(float)Math.Sin(glm.radians(Rotation.y)), 0, (float)Math.Cos(glm.radians(Rotation.y)));


            Right = new vec3((float)Math.Cos(glm.radians(Rotation.y)), 0, (float)Math.Sin(glm.radians(Rotation.y)));
        }
    }
}
