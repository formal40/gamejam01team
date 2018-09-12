using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

using Oikake.Device;
using Oikake.Def;
using Oikake.Util;

namespace Oikake.GameObject
{
    class OneTimeBGObject
    {
        private Vector2 position;
        private float speed;
        private bool isEnd;
        


        public void Dorw(Renderer renderer, string stageName)
        {
            //背景を描画
            renderer.DrawTexture(stageName, position);
        }

        public void Initialize(float speed)
        {
            this.speed = speed;
            ResetPosition();
            isEnd = false;
        }

        public void ResetPosition()
        {
            position = new Vector2(0.0f + Screen.Width, position.Y);
        }

        public void Start()
        {
            isEnd = false;
        }

        public void Update()
        {
            if(!isEnd)
            {
                Vector2 velocity = new Vector2(1.0f, 0.0f);
                position -= velocity * speed;
            }

            if(position.X < -Screen.Width)
            {
                isEnd = true;
                ResetPosition();
            }
        }
    }
}
