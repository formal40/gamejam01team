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
    class ParticleBig : Particle
    {
        private Timer timer;

        public ParticleBig(string name, Vector2 position, Vector2 velocity, IParticleMediator mediator)
            : base(name, position, velocity, mediator)
        {
            var random = GameDevice.Instance().GetRandom();
            timer = new CountDownTimer((float)random.NextDouble() - 0.4f);
        }

        public ParticleBig(IParticleMediator mediator) : base(mediator)
        {
            var random = GameDevice.Instance().GetRandom();
            timer = new CountDownTimer(random.Next(1, 3));
            name = "particleSmall";
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
