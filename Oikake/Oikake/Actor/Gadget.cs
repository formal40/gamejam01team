using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Oikake.Def;
using Oikake.Device;
using Oikake.Scene;

namespace Oikake.Actor
{
    /// <summary>
    /// 作成者：近藤卓
    /// 作成日：2018/09/09
    /// 概要　：ガジェット抽象クラス
    /// </summary>
    abstract class Gadget
    {
        protected Vector2 position;
        protected string name;
        protected int size; //画像サイズ
        protected bool isDeadFlag;
        protected IGameMediator mediator;

        ///<summary>
        ///コンストラクタ
        /// </summary>
        /// <param name="name">画像の名前</param>
        public Gadget(string name, IGameMediator mediator)
        {
            this.name = name;
            position = Vector2.Zero;
            isDeadFlag = false;
            this.mediator = mediator;
        }

        public abstract void Initialize();
        public abstract void Update(GameTime gameTime);
        public abstract void Shutdown();
        public abstract void Hit(Gadget other);

        public int GetSize()
        {
            return size;
        }

        ///<summary>
        ///死んでいるか？
        /// </summary>
        /// <remarks></remarks>
        public bool IsDead()
        {
            return isDeadFlag;
        }

        ///<summary>
        ///描画
        /// </summary>
        /// <param name="renderer">描画オブジェクト</param>
        public virtual void Draw(Renderer renderer)
        {
            renderer.DrawTexture(name, position);
        }

        ///<summary>
        ///衝突判定（2点間の距離と円の半径）
        /// </summary>
        /// <param name="other"></param>
        /// <remarks></remarks>
        public bool IsCollision(Gadget other)
        {
            float lengthh = (position - other.position).Length();

            float radiusSum = size/2 + other.size/2 ;  // 32f + 32f;

            if(lengthh<=radiusSum)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 位置の受け渡し
        /// (引数で渡された変数に自分の位置を渡す)
        /// </summary>
        /// <param name="other">位置を送りたい相手</param>
        public void SetPosition(ref Vector2 other)
        {
            other = position;
        }
    }
}
