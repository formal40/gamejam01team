﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Oikake.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oikake.Scene
{
    /// <summary>
    /// 作成者：近藤卓
    /// 作成日：2018/09/10
    /// 概要　：ランクAのエンディング
    /// </summary>
    class EndingA :IScene
    {
        private bool isEndFlag;
        private Sound sound;
        private Score score;

        public EndingA(Score score)
        {
            isEndFlag = false;
            var gameDevice = GameDevice.Instance();
            sound = gameDevice.GetSound();
            this.score = score;
        }

        public void Draw(Renderer renderer)
        {
            renderer.Begin();
            renderer.DrawTexture("ending", new Vector2(150, 150));
            renderer.DrawTexture("white", new Vector2(300, 200));
            score.Draw(renderer);
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
        }
    }
}