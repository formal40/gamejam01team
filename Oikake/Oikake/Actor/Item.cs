using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Oikake.Device;
using Oikake.Def;
using Oikake.Scene;
using Oikake.Util;

namespace Oikake.Actor
{
    /// <summary>
    /// 作成者：近藤卓
    /// 作成日：2018/09/09
    /// 概要　：ガジェットクラスを継承したアイテム抽象クラス
    /// </summary>
    abstract class Item : Gadget
    {
        protected int score;
        protected int speed;

        ///<summary>
        ///コンストラクタ
        /// </summary>
        public Item(string name, IGameMediator mediator) : base(name, mediator)
        {

        }

        ///<summary>
        ///初期化メソッド
        /// </summary>

        public override void Initialize()
        {
        }

        ///<summary>
        ///更新処理
        /// </summary>
        /// <param name="gameTime">ゲーム時間</param>
        public override void Update(GameTime gameTime)
        {
            position.X += speed;
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
            mediator.AddScore(score);
            isDeadFlag = true;
            mediator.AddActor(new BurstEffect(position, mediator));
        }
    }
}
