using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oikake.Actor.Effects
{
    class ParticleFactory
    {
        private IParticleMediator mediator;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="mediator"></param>
        public ParticleFactory(IParticleMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// 作成
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Particle create(string name)
        {
            Particle particle = null;
            if(name=="Particle")
            {
                particle = new Particle(mediator);
            }
            else if(name=="ParticleBlue")
            {
                particle = new ParticleBlue(mediator);
            }
            return particle;
        }

        public Particle create(string name,Vector2 position,Vector2 velocity)
        {
            Particle particle = null;
            if (name == "Particle")
            {
                particle = new Particle("particle", position, velocity, mediator);
            }
            else if (name == "ParticleBlue")
            {
                particle = new ParticleBlue("particleBlue", position, velocity, mediator);
            }
            return particle;
        }
    }
}
