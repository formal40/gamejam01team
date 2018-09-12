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
        public void Drow(Renderer renderer)
        {
            renderer.Begin();
            renderer.DrawTexture("titlebg", Vector2.Zero);
            renderer.DrawTexture("title111", Vector2.Zero);
            if (number ==1)
            {
                renderer.DrawTexture("Carrot", new Vector2(500, 370));
            }
            else if(number == 2)
            {
                renderer.DrawTexture("Carrot", new Vector2(500, 470));
            }
            else if(number == 3)
            {
                renderer.DrawTexture("Carrot", new Vector2(500, 570));
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
            /*
            motion.Add(0, new Rectangle(64 * 0, 0, 64, 64));
            motion.Add(1, new Rectangle(64 * 1, 0, 64, 64));
            motion.Add(2, new Rectangle(64 * 2, 0, 64, 64));
            motion.Add(3, new Rectangle(64 * 3, 0, 64, 64));
            motion.Add(4, new Rectangle(64 * 4, 0, 64, 64));
            motion.Add(5, new Rectangle(64 * 5, 0, 64, 64));
            */

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
                scene = Scene.Title;
            }
            else if(number == 1)
            {
                scene = Scene.GamePlay;
            }
            else if(number == 2)
            {
                scene = Scene.Credit;
            }
            else if(number == 3)
            {
                if(Input.IsButtonDown(Buttons.A))
                {
                    Game1.exit = true;
                }
            }

            if (Input.IsButtonDown(Buttons.A))
            {
                isEndFlag = true;
            }

            if(Input.IsButtonDown(Buttons.LeftThumbstickUp))
            {
                number += 1;
            }

            if(Input.IsButtonDown(Buttons.LeftThumbstickDown))
            {
                number -= 1;
            }
        }
    }
}
