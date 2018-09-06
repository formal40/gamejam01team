using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Oikake.Device;
using Oikake.Util;

namespace Oikake.Actor.Effects
{
    class ParticleMiddle : Particle
    {
        private Timer timer;

        public ParticleMiddle(string name, Vector2 position, Vector2 velocity, IParticleMediator mediator)
            : base(name, position, velocity, mediator)
        {
            var random = GameDevice.Instance().GetRandom();
            timer = new CountDownTimer((float)random.NextDouble() - 0.2f - 0.2f);
        }

        public ParticleMiddle(IParticleMediator mediator) : base(mediator)
        {
            var random = GameDevice.Instance().GetRandom();
            timer = new CountDownTimer(random.Next(1, 3));
            name = "particleMiddle";
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            velocity -= velocity * 0.006f;

            

            timer.Update(gameTime);
            isDeadFlag = timer.IsTime();
        }
    }
}
