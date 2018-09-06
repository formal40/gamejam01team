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
    /// 白玉（プレイヤー）
    /// </summary>
    class Player : Character
    {
        //private Vector2 position;
        private Motion motion;

        private Direction direction;

        private enum Direction
        {
            DOWN, UP, RIGHT, LEFT
        };
        private Dictionary<Direction, Range> directionRange;

        ///<summary>
        ///コンストラクタ
        /// </summary>
        /*
        public Player(IGameMediator mediator) : base("white", mediator)
        {
            
        }
        */

        public Player(IGameMediator mediator) : base("oikake_player_4anime", mediator)
        {

        }

        ///<summary>
        ///初期化メソッド
        /// </summary>
        public override void Initialize()
        {
            position = new Vector2(300, 400);

            motion = new Motion();

            /*
            //下向き
            motion.Add(0, new Rectangle(64 * 0, 64 * 0, 64, 64));
            motion.Add(1, new Rectangle(64 * 1, 64 * 0, 64, 64));
            motion.Add(2, new Rectangle(64 * 2, 64 * 0, 64, 64));
            motion.Add(3, new Rectangle(64 * 3, 64 * 0, 64, 64));

            //上向き
            motion.Add(4, new Rectangle(64 * 0, 64 * 1, 64, 64));
            motion.Add(5, new Rectangle(64 * 1, 64 * 1, 64, 64));
            motion.Add(6, new Rectangle(64 * 2, 64 * 1, 64, 64));
            motion.Add(7, new Rectangle(64 * 3, 64 * 1, 64, 64));

            //下向き
            motion.Add(8, new Rectangle(64 * 0, 64 * 2, 64, 64));
            motion.Add(9, new Rectangle(64 * 1, 64 * 2, 64, 64));
            motion.Add(10, new Rectangle(64 * 2, 64 * 2, 64, 64));
            motion.Add(11, new Rectangle(64 * 3, 64 * 2, 64, 64));

            //下向き
            motion.Add(12, new Rectangle(64 * 0, 64 * 3, 64, 64));
            motion.Add(13, new Rectangle(64 * 1, 64 * 3, 64, 64));
            motion.Add(14, new Rectangle(64 * 2, 64 * 3, 64, 64));
            motion.Add(15, new Rectangle(64 * 3, 64 * 3, 64, 64));
            */

            for (int i = 0; i <= 15; i++)
            {
                motion.Add(i, new Rectangle(64 * (i % 4), 64 * (int)(i / 4), 64, 64));
            }

            motion.Initialize(new Range(0, 15), new CountDownTimer(0.2f));

            direction = Direction.DOWN;
            directionRange = new Dictionary<Direction, Range>()
            {
                {Direction.DOWN,new Range(0,3) },
                {Direction.UP,new Range(4,7) },
                {Direction.RIGHT,new Range(8,11) },
                {Direction.LEFT,new Range(12,15) }
            };
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
            var max = new Vector2(Screen.Width - 64, Screen.Height - 64);
            position = Vector2.Clamp(position, min, max);

            UpdateMotion();
            motion.Update(gameTime);

            if (Input.GetKeyTrigger(Keys.Z))
            {
                if ((velocity.Length()) <= 0)
                {
                    Dictionary<Direction, Vector2> velocityDict = new Dictionary<Direction, Vector2>()
                    {
                        {Direction.LEFT,new Vector2(-1,0) },
                        {Direction.RIGHT,new Vector2(1,0) },
                        {Direction.UP,new Vector2(0,-1) },
                        {Direction.DOWN,new Vector2(0,1) },
                    };
                    velocity = velocity = velocityDict[direction];
                }
                mediator.AddActor(
                    new PlayerBullet(
                        position,
                        mediator,
                        velocity));
            }
        }

        ///<summary>
        ///ChanracterクラスのDrawメソッドに代わって描画
        /// </summary>
        /// <param name="renderer">描画オブジェクト</param>

        public override void Draw(Renderer renderer)
        {
            renderer.DrawTexture(name, position, motion.DrawingRenge());
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
        public override void Hit(Character other)
        {

        }

        /// <summary>
        /// モーションの変更
        /// </summary>
        /// <param name="direction">変更したい向き</param>
        private void ChangeMotion(Direction direction)
        {
            this.direction = direction;
            motion.Initialize(directionRange[direction], new CountDownTimer(0.2f));
        }

        private void UpdateMotion()
        {
            Vector2 velocity = Input.Velocity();

            if (velocity.Length() <= 0.0f)
            {
                return;
            }

            if ((velocity.Y > 0.0f) && (direction != Direction.DOWN))
            {
                ChangeMotion(Direction.DOWN);
            }
            else if ((velocity.Y < 0.0f) && (direction != Direction.UP))
            {
                ChangeMotion(Direction.UP);
            }
            else if ((velocity.X > 0.0f) && (direction != Direction.RIGHT))
            {
                ChangeMotion(Direction.RIGHT);
            }
            else if ((velocity.X < 0.0f) && (direction != Direction.LEFT))
            {
                ChangeMotion(Direction.LEFT);
            }
        }
    }
}
