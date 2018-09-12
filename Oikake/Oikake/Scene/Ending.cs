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

        public Ending(Score score)
        {
            isEndFlag = false;
            var gameDevice = GameDevice.Instance();
            sound = gameDevice.GetSound();
            this.score = score;
        }

        public void Draw(Renderer renderer)
        {
            renderer.Begin();
            renderer.DrawTexture("FieldScore",Vector2.Zero);
            score.CenterDraw(renderer);
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
            //スコアで分岐
            if (score.GetScore() >= 300)
            {
                return Scene.EndingS;
            }
            else if (score.GetScore() >= 200)
            {
                return Scene.EndingA;
            }
            else if (score.GetScore() >= 100)
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

            if (Input.GetKeyTrigger(Keys.Space))
            {
                isEndFlag = true;
            }
        }
    }
}
