using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using Oikake.Device;
using Oikake.Util;

namespace Oikake.Scene
{
    class Ending : IScene
    {
        private bool isEndFlag;
        private Sound sound;

        private Score score;
        private CountDownTimer scorePrintTimer;

        private int number;
        private Scene scene;

        public Ending(Score score)
        {
            isEndFlag = false;
            var gameDevice = GameDevice.Instance();
            sound = gameDevice.GetSound();
            scorePrintTimer = new CountDownTimer(3);//スコア表示時間
            this.score = score;
        }

        public void Draw(Renderer renderer)
        {
            renderer.Begin();

            score.CenterDraw(renderer);    

            if (number == 0)
            {
                renderer.DrawTexture("Carrot", new Vector2(1000, 580));
            }
            else if(number == 1)
            {
                renderer.DrawTexture("Carrot", new Vector2(1000, 650));
            }
            renderer.End();
        }

        public void Initialize()
        {
            isEndFlag = false;
            scorePrintTimer.Initialize();
        }

        public bool IsEnd()
        {
            return isEndFlag;
        }

        public Scene Next()
        {
            //スコアで分岐
            if(score.GetScore() >= 300)
            {
                return Scene.EndingS;
            }
            else if(score.GetScore() >= 200)
            {
                return Scene.EndingA;
            }
            else if(score.GetScore() >= 100)
            {
                return Scene.EndingB;
            }
            else
            {
                return Scene.EndingC;
            }
        }

        public void Shutdown()
        {
            sound.StopBGM();
        }

        public void Update(GameTime gameTime)
        {
            sound.PlayBGM("endingbgm");
            scorePrintTimer.Update(gameTime);

            if (scorePrintTimer.IsTime())
            {
                isEndFlag = true;
            }

            if(number == 0)
            {
                scene = Scene.Title;
            }
            else if(number == 1)
            {
                if (Input.IsButtonDown(Buttons.A))
                {
                    Game1.exit = true;
                }
            }

            if (Input.IsButtonDown(Buttons.A))
            {
                isEndFlag = true;
            }

            if (Input.IsButtonDown(Buttons.LeftThumbstickDown))
            {
                number += 1;
            }

            if (Input.IsButtonDown(Buttons.LeftThumbstickUp))
            {
                number -= 1;
            }

            if (number < 0)
            {
                number = 0;
            }
            else if (number > 1)
            {
                number = 1;
            }
        }
    }
}
