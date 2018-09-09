using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using Oikake.Actor;
using Oikake.Device;
using Oikake.Util;

namespace Oikake.Scene
{
    interface IGameMediator
    {
        void AddActor(Character character);
        void AddScore();
        void AddScore(int num);
    }
    class GamePlay : IScene, IGameMediator
    {
        //private Player player; //プレイヤーとなる白玉
        //private List<Character> characters;

        private CharacterManager characterManager;
        private Timer timer;
        private TimerUI timerUI;
        private Score score;
        private bool isEndFlag;
        private Sound sound;

        public GamePlay()
        {
            isEndFlag = false;
            var gameDevice = GameDevice.Instance();
            sound = gameDevice.GetSound();
        }

        public void AddActor(Character character)
        {
            characterManager.Add(character);
        }

        public void AddScore()
        {
            score.Add();
        }

        public void AddScore(int num)
        {
            score.Add(num);
        }

        public void Draw(Renderer renderer)
        {
            //描画開始
            renderer.Begin();
            //背景を描画
            renderer.DrawTexture("stage", Vector2.Zero);

            characterManager.Draw(renderer);

            timerUI.Draw(renderer);
            score.Draw(renderer);

            /*
            if (timer.IsTime())
            {
                renderer.DrawTexture("ending", new Vector2(150, 150));
            }
            */

            //描画終了
            renderer.End();
        }

        public void Initialize()
        {
            isEndFlag = false;

            characterManager = new CharacterManager();

            // characterManager.Add(new Player(this));
            Player player = new Player(this);
            characterManager.Add(player);

            characterManager.Add(new Enemy(this, new AttackAI(player)));
            //characterManager.Add(new Enemy(this, new BoundAI()));
            //characterManager.Add(new Enemy(this, new BoundAI()));
            //characterManager.Add(new Enemy(this, new RandomAI()));
            //characterManager.Add(new Enemy(this, new RandomAI()));
            //characterManager.Add(new Enemy(this, new RandomAI()));
            characterManager.Add(new Enemy(this, new TraceAI()));

            /*
            characterManager.Add(new BoundEnemy(this));

            //10体登録
            for (int i = 0; i < 10; i++)
            {
                characterManager.Add(new RandomEnemy(this));
            }
            */

            timer = new CountDownTimer(10);
            timerUI = new TimerUI(timer);

            score = new Score();
        }

        public bool IsEnd()
        {
            return isEndFlag;
        }

        public Scene Next()
        {
            Scene nextScene = Scene.Ending;
            if (score.GetScore() >= 1000)
            {
                nextScene = Scene.GoodEnding;
            }
            return nextScene;
        }

        public void Shutdown()
        {
            sound.StopBGM();
        }

        public void Update(GameTime gameTime)
        {
            timer.Update(gameTime);
            score.Update(gameTime);

            characterManager.Update(gameTime);


            sound.PlayBGM("gameplaybgm");

            //時間切れか？
            if (timer.IsTime())
            {
                score.shutdown();
                isEndFlag = true;　//シーン終了へ
            }
        }
    }
}
