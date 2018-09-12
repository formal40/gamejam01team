
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Oikake.Device;
using Oikake.Scene;
using Oikake.Util;

namespace Oikake.Scene
{
    class Title : IScene
    {
        private bool isEndFlag;
        private Sound sound;
        private Motion motion;
        private int number;
        private Scene scene;

        

        public Title()
        {
            isEndFlag = false;
            var gameDevice = GameDevice.Instance();
            sound = gameDevice.GetSound();
        }

        /// <summary>
        /// 描画
        /// </summary>
        /// <param name="renderer">描画オブジェクト</param>
        public void Draw(Renderer renderer)
        {
            renderer.Begin();

            renderer.DrawTexture("rogo", Vector2.Zero);
            renderer.DrawTexture("startButton",new Vector2(240,500));

            renderer.DrawTexture("titlebg", Vector2.Zero);
            renderer.DrawTexture("title111", Vector2.Zero);
            if (number ==0)
            {
                renderer.DrawTexture("Carrot", new Vector2(400, 400));
            }
            else if(number == 1)
            {
                renderer.DrawTexture("Carrot", new Vector2(450, 500));
            }
            else if(number == 2)
            {
                renderer.DrawTexture("Carrot", new Vector2(450, 600));
            }

            renderer.End();
        }

        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialize()
        {
            isEndFlag = false;

            motion = new Motion();

            for (int i = 0; i <= 5; i++)
            {
                motion.Add(i, new Rectangle(64 * i, 0, 64, 64));
            }

            motion.Initialize(new Range(0, 5), new CountDownTimer(0.05f));
        }

        /// <summary>
        /// 終了か？
        /// </summary>
        /// <returns>シーンが終わってたらtrue</returns>
        public bool IsEnd()
        {
            return isEndFlag;
        }

        /// <summary>
        /// 次のシーンへ
        /// </summary>
        /// <returns>次のシーン</returns>
        public Scene Next()
        {
            return scene;            
        }

        /// <summary>
        /// 終了
        /// </summary>
        public void Shutdown()
        {
            sound.StopBGM();
        }

        /// <summary>
        /// 更新
        /// タイトルのGamePlay,Credit,exitの選択。選択した画面への移行。
        /// </summary>
        /// <param name="gameTime">ゲーム時間</param>
        public void Update(GameTime gameTime)
        {
            sound.PlayBGM("titlebgm");
            motion.Update(gameTime);

            if(Input.GetKeyTrigger(Keys.Space))
            {
                isEndFlag = true;
                sound.PlaySE("titlese");
            }

            if (number == 0)
            {
                scene = Scene.GamePlay;
            }
            else if(number == 1)
            {
                scene = Scene.Credit;
            }
            else if(number == 2)
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

            if(Input.IsButtonDown(Buttons.LeftThumbstickDown))//左スティックが上に行ったときnumberが増える
            {
                number += 1;
            }

            if(Input.IsButtonDown(Buttons.LeftThumbstickUp))
            {
                number -= 1;
            }

            if(number <0)
            {
                number = 0;
            }
            else if(number>2)
            {
                number = 2;
            }
        }
    }
}
