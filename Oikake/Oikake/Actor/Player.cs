using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Oikake.Device;
using Oikake.Def;
using Oikake.Scene;
using Oikake.Util;

namespace Oikake.Actor
{
    /// <summary>
    /// 作成者：近藤卓
    /// 作成日：2018/09/09
    /// 概要　：ガジェットクラスを継承したプレイヤー（照準）
    /// </summary>
    class Player : Gadget
    {
        ///<summary>
        ///コンストラクタ
        /// </summary>
        public Player(IGameMediator mediator) : base("white", mediator)
        {
            size = 64;
        }

        ///<summary>
        ///初期化メソッド
        /// </summary>
        public override void Initialize()
        {
            position = new Vector2(Screen.Width/2, Screen.Height/2);
        }

        ///<summary>
        ///更新処理
        /// </summary>
        /// <param name="gameTime">ゲーム時間</param>
        public override void Update(GameTime gameTime)
        {
            Vector2 velocity = Input.Velocity();

            float speed = 5.0f;
            position = position + Input.Velocity() * speed;

            var min = Vector2.Zero;
            var max = new Vector2(Screen.Width - size, Screen.Height - size);
            position = Vector2.Clamp(position, min, max);

            if (Input.GetKeyTrigger(Keys.Z))
            {
                mediator.AddActor(new PlayerBullet(mediator, position));
            }
        }

        ///<summary>
        ///ChanracterクラスのDrawメソッドに代わって描画
        /// </summary>
        /// <param name="renderer">描画オブジェクト</param>

        public override void Draw(Renderer renderer)
        {
            renderer.DrawTexture(name, position);
        }


        ///<summary>
        ///終了処理
        /// </summary>
        public override void Shutdown()
        {

        }

        /// <summary>
        /// ヒット通知
        /// </summary>
        /// <param name="other">衝突した相手</param>
        public override void Hit(Gadget other)
        {

        }
    }
}
