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
    /// 黒玉（敵）
    /// </summary>

    class Enemy : Character
    {
        //private Sound sound;
        //private Vector2 position;

        private AI ai;
        private Random rnd;
        private State state;
        private Timer timer;
        private bool isDisplay;
        private readonly int Impression = 10;
        private int displayCount;

        ///<summary>
        ///コンストラクタ
        /// </summary>
        public Enemy(IGameMediator mediator, AI ai) : base("black", mediator)
        {
            //var gameDevice = GameDevice.Instance();
            //sound = gameDevice.GetSound();
            this.ai = ai;
            state = State.Preparation;
        }

        ///<summary>
        ///初期化メソッド
        /// </summary>
        
        public override void Initialize()
        {
            //position = new Vector2(100, 100);
            var gameDevice = GameDevice.Instance();
            rnd = gameDevice.GetRandom();
            position = new Vector2(
                rnd.Next(Screen.Width - 64),
                rnd.Next(Screen.Height - 64));

            state = State.Preparation;

            timer = new CountDownTimer(0.25f);
            isDisplay = true;
            displayCount = Impression;
        }

        private void PreparationUpdate(GameTime gameTime)
        {
            timer.Update(gameTime);
            if (timer.IsTime())
            {
                isDisplay = !isDisplay;
                displayCount -= 1;
                timer.Initialize();
            }
            if (displayCount == 0)
            {
                state = State.Alive;
                timer.Initialize();
                displayCount = Impression;
                isDisplay = true;
            }
        }

        private void PreparationDraw(Renderer renderer)
        {
            if(isDisplay)
            {
                base.Draw(renderer);
            }
        }

        private void AliveUpdate(GameTime gameTime)
        {
            position = ai.Think(this);
        }

        private void AliveDraw(Renderer renderer)
        {
            base.Draw(renderer);
        }

        private void DyingUpdate(GameTime gameTime)
        {
            timer.Update(gameTime);
            if(timer.IsTime())
            {
                displayCount -= 1;
                timer.Initialize();
                isDisplay = !isDisplay;
            }

            if (displayCount == 0)
            {
                state = State.Dead;
            }
        }

        private void DyingDraw(Renderer renderer)
        {
            if(isDisplay)
            {
                renderer.DrawTexture(name, position, Color.Red);
            }
            else
            {
                base.Draw(renderer);
            }
        }

        private void DeadUpdate(GameTime gameTime)
        {
            isDeadFlag = true;
            mediator.AddActor(new BurstEffect(position, mediator));
        }

        private void DeadDraw(Renderer renderer)
        {

        }
        

        ///<summary>
        ///更新処理
        /// </summary>
        /// <param name="gameTime">ゲーム時間</param>
        public override void Update(GameTime gameTime)
        {
            switch(state)
            {
                case State.Preparation:
                    PreparationUpdate(gameTime);
                    break;
                case State.Alive:
                    AliveUpdate(gameTime);
                    break;
                case State.Dying:
                    DyingUpdate(gameTime);
                    break;
                case State.Dead:
                    DeadUpdate(gameTime);
                    break;
            }
            //position = ai.Think(this);
        }

        ///<summary>
        ///描画メソッド
        /// </summary>
        /// <param name="renderer">描画オブジェクト</param>
        ///
        public override void Draw(Renderer renderer)
        {
            switch(state)
            {
                case State.Preparation:
                    PreparationDraw(renderer);
                    break;
                case State.Alive:
                    AliveDraw(renderer);
                    break;
                case State.Dying:
                    DyingDraw(renderer);
                    break;
                case State.Dead:
                    DeadDraw(renderer);
                    break;
            }
        }

        ///<summary>
        ///終了処理
        /// </summary>
        public override void Shutdown()
        {
            //sound.StopBGM();
        }

        /// <summary>
        /// ヒット通知
        /// </summary>
        /// <param name="other">衝突した相手</param>
        public override void Hit(Character other)
        {
            if (state != State.Alive)
            {
                return;
            }
            state = State.Dying;

            int score = 0;
            if(ai is BoundAI)
            {
                score = 1000;
            }
            else if(ai is RandomAI)
            {
                score = 500;
            }
            else if (ai is TraceAI)
            {
                score = 500;
            }
            else if(ai is AttackAI)
            {
                score = 50;
                mediator.AddScore(score);
                mediator.AddActor(new Enemy(mediator, ai));
                isDeadFlag = true;
                return;
            }

            mediator.AddScore(score);

            AI nextAI = new BoundAI();
            switch(rnd.Next(2))
            {
                case 0:
                    nextAI = new BoundAI();
                    break;
                case 1:
                    nextAI = new RandomAI();
                    break;
            }
            mediator.AddActor(new Enemy(mediator, nextAI));

            //isDeadFlag = true;
            //mediator.AddActor(new BurstEffect(position, mediator));
            
            //sound.PlaySE("gameplayse");
        }
    }
}
