using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Oikake.Device;
using Oikake.Scene;

namespace Oikake.Actor.Effects
{
    class ParticleManager
    {
        private List<Particle> particles = new List<Particle>();
        private List<Particle> addParticles = new List<Particle>();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ParticleManager()
        {

        }

        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialize()
        {
            particles.Clear();
            addParticles.Clear();
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            particles.ForEach(particle => particle.Update(gameTime));

            particles.AddRange(addParticles);
            addParticles.Clear();

            particles.RemoveAll(particle => particle.IsDead());
        }

        /// <summary>
        /// 終了
        /// </summary>
        public void Shutdown()
        {

        }

        /// <summary>
        /// 描画
        /// </summary>
        /// <param name="renderer"></param>
        public void Draw(Renderer renderer)
        {
            particles.ForEach(particle => particle.Draw(renderer));
        }

        public void Add(Particle particle)
        {
            //particles.Add(particle);
            addParticles.Add(particle);
        }
    }
}
