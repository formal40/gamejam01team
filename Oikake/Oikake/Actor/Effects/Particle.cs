using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Oikake.Def;
using Oikake.Device;

namespace Oikake.Actor.Effects
{
    class Particle
    {
        protected readonly float GRAVITY = 0.5f;
        protected string name;
        protected bool isDeadFlag;
        protected Vector2 position;
        protected Vector2 velocity;
        protected IParticleMediator mediator;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="name"></param>
        /// <param name="position"></param>
        /// <param name="velocity"></param>
        /// <param name="mediator"></param>
        public Particle(string name, Vector2 position, Vector2 velocity, IParticleMediator mediator)
        {
            this.name = name;
            this.position = position;
            this.velocity = velocity;
            this.mediator = mediator;
            isDeadFlag = false;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="mediator"></param>
        public Particle(IParticleMediator mediator) : this("particle", Vector2.Zero, Vector2.Zero, mediator)
        {
            isDeadFlag = false;
        }

        /// <summary>
        /// テクスチャ名の設定
        /// </summary>
        /// <param name="name"></param>
        public void SetTexture(string name)
        {
            this.name = name;
        }

        /// <summary>
        /// 位置の設定
        /// </summary>
        /// <param name="position"></param>
        public void SetPosition(Vector2 position)
        {
            this.position = position;
        }

        /// <summary>
        /// 移動量の生成
        /// </summary>
        /// <param name="velocity"></param>
        public void SetVelocity(Vector2 velocity)
        {
            this.velocity = velocity;
        }

        /// <summary>
        /// テクスチャ名の取得
        /// </summary>
        /// <returns></returns>
        public string GetTexture()
        {
            return name;
        }

        /// <summary>
        /// 位置の取得
        /// </summary>
        /// <returns></returns>
        public Vector2 GetPosition()
        {
            return position;
        }

        /// <summary>
        /// 移動量の取得
        /// </summary>
        /// <returns></returns>
        public Vector2 GetVelocity()
        {
            return velocity;
        }

        /// <summary>
        /// 更新仮想メソッド
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Update(GameTime gameTime)
        {
            position += velocity;
            velocity.Y += GRAVITY;
            isDeadFlag = (position.Y > Screen.Height);
        }

        /// <summary>
        /// 描画仮想メソッド
        /// </summary>
        /// <param name="renderer"></param>
        public virtual void Draw(Renderer renderer)
        {
            renderer.DrawTexture(name, position);
        }

        /// <summary>
        /// 死亡か？
        /// </summary>
        /// <returns></returns>
        public bool IsDead()
        {
            return isDeadFlag;
        }
    }
}
