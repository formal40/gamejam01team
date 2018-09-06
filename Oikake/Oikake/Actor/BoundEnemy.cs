using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

using Oikake.Def;
using Oikake.Scene;
using Oikake.Device;

namespace Oikake.Actor
{
    class BoundEnemy : Character
    {
        private Sound sound;

        private Vector2 velocity;
        private static Random rnd = new Random();

        public BoundEnemy(IGameMediator mediator) : base("black", mediator)
        {
            var gameDevice = GameDevice.Instance();
            sound = gameDevice.GetSound();
        }

        public override void Initialize()
        {
            position = new Vector2(rnd.Next(Screen.Width - 64), rnd.Next(Screen.Height - 64));
            velocity = new Vector2(-10f, 0);

        }

        public override void Shutdown()
        {
            sound.StopBGM();
        }

        public override void Update(GameTime gameTime)
        {
            if (position.X < 0)
            {
                velocity.X = -velocity.X;
            }

            if (position.X > Screen.Width - 64)
            {
                velocity.X = -velocity.X;
            }

            if (position.Y < 0)
            {
                velocity.Y = -velocity.Y;
            }

            if (position.Y > Screen.Height - 64)
            {
                velocity.Y = -velocity.Y;
            }

            position += velocity;
        }

        /// <summary>
        /// ヒット通知
        /// </summary>
        /// <param name="other">衝突した相手</param>
        public override void Hit(Character other)
        {
            isDeadFlag = true;
            mediator.AddScore(100);

            mediator.AddActor(new BoundEnemy(mediator));
            mediator.AddActor(new BoundEnemy(mediator));
            mediator.AddActor(new BurstEffect(position, mediator));

            sound.PlaySE("gameplayse");
        }
    }
}
