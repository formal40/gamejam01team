using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using Oikake.Device;

namespace Oikake.Scene
{
    class Ending : IScene
    {
        private bool isEndFlag;
        IScene backGroundScene;
        private Sound sound;
        private int number;
        private Scene scene;

        public Ending(IScene scene)
        {
            isEndFlag = false;
            backGroundScene = scene;
            var gameDevice = GameDevice.Instance();
            sound = gameDevice.GetSound();
        }

        public void Drow(Renderer renderer)
        {
            backGroundScene.Drow(renderer);

            renderer.Begin();
            if(number == 1)
            {
                renderer.DrawTexture("Carrot", new Vector2(1000, 550));
            }
            else if(number == 2)
            {
                renderer.DrawTexture("Carrot", new Vector2(1000, 650));
            }
            renderer.End();
        }

        public void Initialize()
        {
            isEndFlag = false;
        }

        public bool IsEnd()
        {
            return isEndFlag;
        }

        public Scene Next()
        {
            return Scene.Title;
        }

        public void Shutdown()
        {
            sound.StopBGM();
        }

        public void Update(GameTime gameTime)
        {
            sound.PlayBGM("endingbgm");

            if (Input.GetKeyTrigger(Keys.Space))
            {
                isEndFlag = true;
                sound.PlaySE("endingse");
            }

            if (Input.GetKeyTrigger(Keys.Space))
            {
                isEndFlag = true;
            }

            if(number == 2)
            {
                scene = Scene.Title;
            }
            else if(number == 3)
            {
                Game1.exit = true;
            }

            if (Input.IsButtonDown(Buttons.A))
            {
                isEndFlag = true;
            }

            if (Input.IsButtonDown(Buttons.LeftThumbstickUp))
            {
                number += 1;
            }

            if (Input.IsButtonDown(Buttons.LeftThumbstickDown))
            {
                number -= 1;
            }
        }
    }
}
