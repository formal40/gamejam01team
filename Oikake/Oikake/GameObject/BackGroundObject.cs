using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

using Oikake.Device;
using Oikake.Def;

namespace Oikake.GameObject
{
    class BackGroundObject
    {
        private Vector2 position;
        private float speed;

        public void Dorw(Renderer renderer)
        {
            //背景を描画
            renderer.DrawTexture("stage", position);
            renderer.DrawTexture("stage", new Vector2(position.X + Screen.Width, position.Y));
        }

        public void Initialize(float speed)
        {
            this.speed = speed;
            PositionReset();
        }

        private void PositionReset()
        {
            position = Vector2.Zero;
        }

        public void Update()
        {
            Vector2 velocity = new Vector2(1.0f, 0.0f);
            position -= velocity * speed;

            if (position.X + Screen.Width <= 0.0f)
            {
                PositionReset();
            }
        }
    }
}
