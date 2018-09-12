using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

using Oikake.Actor;
using Oikake.Def;
using Oikake.Device;
using Oikake.Util;

namespace Oikake.Scene
{
    class Score
    {
        private int poolScore;
        private int score;
        
        public void Initialize()
        {
            score = 0;
            poolScore = 0;
        }

        public void Add()
        {
            poolScore++;
        }

        public void Add(int num)
        {
            poolScore += num;
        }

        public void Update(GameTime gameTime)
        {
            if (poolScore > 0)
            {
                score += 1;
                poolScore -= 1;
            }
            else if (poolScore < 0)
            {
                score -= 1;
                poolScore += 1;
            }
        }

        public void Draw(Renderer renderer)
        {
            renderer.DrawTexture("score", new Vector2(50, 9));

            renderer.DrawNumber("number",new Vector2(250,10),score);
        }

        /// <summary>
        /// 画面の中央にスコアを表示
        /// </summary>
        /// <param name="renderer"></param>
        public void CenterDraw(Renderer renderer)
        {
            renderer.DrawTexture("score", new Vector2(Screen.Width/2 - 128, Screen.Height/3));

            renderer.DrawNumber("number", new Vector2(Screen.Width/2-64, Screen.Height/2), score);
        }

        public void shutdown()
        {
            score += poolScore;
            if (score < 0)
            {
                score = 0;
            }
            poolScore = 0;
        }

        public int GetScore()
        {
            int currentScore = score + poolScore;
            if (currentScore < 0)
            {
                currentScore = 0;
            }
            return currentScore;
        }
    }
}
