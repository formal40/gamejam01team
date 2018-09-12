using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Oikake.Device;

namespace Oikake.Scene
{
    class Credit : IScene
    {
        private bool isEndFlag;
        private int number;

        public Credit()
        {
            isEndFlag = false;
        }

        public void Drow(Renderer renderer)
        {
            renderer.Begin();
            renderer.End();
        }

        public void Initialize()
        {
            isEndFlag = false;
        }
        
        public Scene Next()
        {
            return Scene.Title;
        }

        public bool IsEnd()
        {
            return isEndFlag;
        }

        public void Shutdown()
        {

        }

        public void Update(GameTime gameTme)
        {
            if (Input.IsButtonDown(Buttons.A))
            {
                number -= 1;
            }
        }

    }
}
