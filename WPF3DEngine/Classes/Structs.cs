using GlmNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF3DEngine.Classes
{
    public struct Pairs
    {
        public int vertID;
        public int uvID;
        public int normalID;

        public Pairs(string vertID, string uvID, string normalID)
        {
            this.vertID = Convert.ToInt32(vertID);
            this.uvID = Convert.ToInt32(uvID);
            this.normalID = Convert.ToInt32(normalID);
        }
    }
}
