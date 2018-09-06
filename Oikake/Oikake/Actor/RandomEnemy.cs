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
    /// <summary>
    /// キャラクタークラスを継承した乱数エネミークラス
    /// </summary>
    class RandomEnemy : Character
    {
        private Sound sound;

        private static Random rnd = new Random();
        private int changeTime;

        ///<summary>
        ///コンストラクタ
        /// </summary>
        public RandomEnemy(IGameMediator mediator) : base("black", mediator)
        {
            var gameDevice = GameDevice.Instance();
            sound = gameDevice.GetSound();
        }

        ///<summary>
        ///初期化
        /// </summary>
        public override void Initialize()
        {
            position = new Vector2(rnd.Next(Screen.Width - 64), rnd.Next(Screen.Height - 64));
            changeTime = 60 * rnd.Next(2, 5);
        }

        ///<summary>
        ///終了処理
        /// </summary>
        public override void Shutdown()
        {
            sound.StopBGM();
        }

        ///<summary>
        ///更新
        /// </summary>
        /// <param name="gamaTime"></param>
        public override void Update(GameTime gameTime)
        {
            changeTime -= 1;

            if(changeTime<0)
            {
                Initialize();
            }
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
