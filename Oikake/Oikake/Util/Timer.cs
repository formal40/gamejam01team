using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace Oikake.Util
{
    abstract class Timer
    {
        protected float limitTime;
        protected float currentTime;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="second">制限時間</param>
        public Timer(float second)
        {
            limitTime = 60 * second;
        }

        /// <summary>
        /// デフォルトコンストラクタ
        /// </summary>
        public Timer() : this(1)
        {

        }

        public abstract void Initialize();
        public abstract void Update(GameTime gameTime);
        public abstract bool IsTime();

        /// <summary>
        /// 制限時間を指定
        /// </summary>
        /// <param name="second"></param>
        public void SetTime(float second)
        {
            limitTime = 60 * second;
        }

        /// <summary>
        /// 現在時間の取得
        /// </summary>
        /// <returns>秒</returns>
        public float Now()
        {
            return currentTime / 60f;
        }

        public abstract float Rate();
    }
}
