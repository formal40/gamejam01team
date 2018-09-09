using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

using Oikake.Actor;
using Oikake.Def;
using Oikake.Device;
using Oikake.Scene;
using Oikake.Util;

namespace Oikake.Actor
{
    class BurstEffect : Gadget
    {
        private Timer timer;
        private int counter;
        private readonly int pictureNum = 7;

        /// <summary>
        /// コンストラクタ
        /// エフェクト画像は1枚絵で、7つの絵でアニメーション
        /// </summary>
        /// <param name="position">表示位置</param>
        /// <param name="mediator">仲介者</param>
        public BurstEffect(Vector2 position, IGameMediator mediator) : base("pipo-btleffect", mediator)
        {
            this.position = position;
        }

        /// <summary>
        /// ヒット通知
        /// </summary>
        /// <param name="other"></param>
        public override void Hit(Gadget other)
        {
            
        }

        /// <summary>
        /// 初期化
        /// </summary>
        public override void Initialize()
        {
            counter = 0;
            isDeadFlag = false;
            timer = new CountDownTimer(0.05f);
        }

        /// <summary>
        /// 終了
        /// </summary>
        public override void Shutdown()
        {
            
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            timer.Update(gameTime);

            if(timer.IsTime())
            {
                counter += 1;
                timer.Initialize();
                if(counter>=pictureNum)
                {
                    isDeadFlag = true;
                }
            }
        }

        public override void Draw(Renderer renderer)
        {
            renderer.DrawTexture(name, position, new Rectangle(counter * 120, 0, 120, 120));
        }
    }
}
