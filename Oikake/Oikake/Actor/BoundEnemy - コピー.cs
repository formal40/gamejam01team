/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

using Oikake.Def;

namespace Oikake.Actor
{
    class BoundEnemy2 : Character
    {
        private Vector2 velocity;
        private static Random rnd = new Random();

        public BoundEnemy2() : base("black")
        {
            velocity = Vector2.Zero;
        }

        public override void Initialize()
        {
            position = new Vector2(rnd.Next(Screen.Width - 64), rnd.Next(Screen.Height - 64));

            velocity = new Vector2(10f, 0);
        }

        public override void Shutdown()
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            if (position.X < 0)
            {
                velocity = -velocity;
            }

            if (position.X > Screen.Width - 64)
            {
                velocity = -velocity;
            }
            

            if (position.Y < 0)
            {
                velocity = -velocity;
            }

            if (position.Y > Screen.Height - 64)
            {
                velocity = -velocity;
            }

            position += velocity;
        }
    }
}
*/
