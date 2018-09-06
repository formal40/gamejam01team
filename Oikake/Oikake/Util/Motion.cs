using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oikake.Util
{
    class Motion
    {
        private Range range;
        private Timer timer;
        private int motionNumber;

        private Dictionary<int, Rectangle> rectangles = new Dictionary<int, Rectangle>();

        /// <summary>
        /// デフォルトコンストラクタ
        /// </summary>
        public Motion()
        {
            Initialize(new Range(0, 0), new CountDownTimer());
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="range">範囲</param>
        /// <param name="timer">モーション切り替え時間</param>
        public Motion(Range range,Timer timer)
        {
            Initialize(range, timer);
        }

        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="range">範囲</param>
        /// <param name="timer">モーション切り替え時間</param>
        public void Initialize(Range range,Timer timer)
        {
            this.range = range;
            this.timer = timer;

            motionNumber = range.First();
        }

        /// <summary>
        /// モーション短形情報の追加
        /// </summary>
        /// <param name="index"></param>
        /// <param name="rect"></param>
        public void Add(int index,Rectangle rect)
        {
            if(rectangles.ContainsKey(index))
            {
                return;
            }
            rectangles.Add(index, rect);
        }

        /// <summary>
        /// モーションの更新
        /// </summary>
        private void MotionUpdate()
        {
            motionNumber += 1;

            if(range.IsOutOfRange(motionNumber))
            {
                motionNumber = range.First();
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="gameTime">ゲーム時間</param>
        public void Update(GameTime gameTime)
        {
            if(range.IsOutOfRange())
            {
                return;
            }

            timer.Update(gameTime);

            if (timer.IsTime())
            {
                timer.Initialize();
                MotionUpdate();
            }
        }

        /// <summary>
        /// 描画範囲の指定
        /// </summary>
        /// <returns></returns>
        public Rectangle DrawingRenge()
        {
            return rectangles[motionNumber];
        }
    }
}
