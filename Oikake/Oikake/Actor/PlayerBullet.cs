using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Oikake.Scene;

namespace Oikake.Actor
{
    /// <summary>
    /// 作成者：近藤卓
    /// 作成日：2018/09/09
    /// 概要　：プレイヤー（照準）とアイテムとの当たり判定用（表示はしない）
    /// </summary>
    class PlayerBullet : Gadget
    {
        public PlayerBullet(IGameMediator mediator, Vector2 position) : base("white", mediator)
        {
            this.position = position;
            size = 64;
        }

        public override void Hit(Gadget other)
        {
            
        }

        public override void Initialize()
        {
            
        }

        public override void Shutdown()
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            //生成されたらすぐ消える
            isDeadFlag = true;
        }
    }
}
