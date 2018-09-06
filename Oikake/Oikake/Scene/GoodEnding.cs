using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Oikake.Actor.Effects;
using Oikake.Device;
using Oikake.Util;

namespace Oikake.Scene
{
    class GoodEnding : IScene, IParticleMediator
    {
        private bool isEndingFlag;
        private IScene backGroundScene;
        private Sound sound;
        private ParticleManager particleManager;
        private ParticleFactory particleFactory;
        private Timer timer;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="scene"></param>
        public GoodEnding(IScene scene)
        {
            isEndingFlag = false;
            backGroundScene = scene;
            var gameDevice = GameDevice.Instance();
            sound = gameDevice.GetSound();

            particleManager = new ParticleManager();
            particleFactory = new ParticleFactory(this);
            timer = new CountDownTimer(1f);
        }

        /// <summary>
        /// 描画
        /// </summary>
        /// <param name="renderer"></param>
        public void Drow(Renderer renderer)
        {
            // シーンごとにrenderer.Begin()～End()を書いているのに注意
            // 背景となるゲームプレイシーン
            backGroundScene.Drow(renderer);

            renderer.Begin();
            renderer.DrawTexture("ending", new Vector2(150, 150));
            particleManager.Draw(renderer);
            renderer.DrawTexture("nc47171", new Vector2(300, 200));　//この画像は自分で準備
            renderer.End();
        }

        /// <summary>
        /// 生成
        /// </summary>
        /// <param name="name">パーティクル名</param>
        /// <returns>生成されたパーティクル</returns>
        public Particle generate(string name)
        {
            var particle = particleFactory.create(name);
            particleManager.Add(particle);
            return particle;
        }

        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialize()
        {
            isEndingFlag = false;
            particleManager.Initialize();
        }

        /// <summary>
        /// 終了か？
        /// </summary>
        /// <returns></returns>
        public bool IsEnd()
        {
            return isEndingFlag;
        }

        /// <summary>
        /// 次のシーンは？
        /// </summary>
        /// <returns></returns>
        public Scene Next()
        {
            return Scene.Title;
        }

        /// <summary>
        /// 終了
        /// </summary>
        public void Shutdown()
        {
            sound.StopBGM();
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            sound.PlayBGM("congratulation"); //おめでとうBGMは自分で準備

            if(Input.GetKeyTrigger(Keys.Space))
            {
                isEndingFlag = true;
                sound.PlaySE("endingse");
            }

            var random = GameDevice.Instance().GetRandom();
            var angel = MathHelper.ToRadians(random.Next(-100, -80));
            var velocity = new Vector2((float)Math.Cos(angel), (float)Math.Sin(angel));
            velocity *= 20.0f;
            //var praticle = new Particle("particle", new Vector2(50, 500), velocity);
            var particle = particleFactory.create("Particle");
            particle.SetPosition(new Vector2(50, 500));
            particle.SetVelocity(velocity);
            particleManager.Add(particle);

            angel = MathHelper.ToRadians(random.Next(-120, -60));
            velocity = new Vector2((float)Math.Cos(angel), (float)Math.Sin(angel));
            velocity *= 15.0f;
            //particle = new Particle("particleBlue", new Vector2(650, 500), velocity);
            particle = particleFactory.create("ParticleBlue");
            particle.SetPosition(new Vector2(650, 500));
            particle.SetVelocity(velocity);
            particleManager.Add(particle);

            timer.Update(gameTime);
            if(timer.IsTime())
            {
                timer.Initialize();
                for (int i = 0; i < 100; i++)
                {
                    angel = MathHelper.ToRadians(random.Next(-180, 180));
                    velocity = new Vector2((float)Math.Cos(angel), (float)Math.Sin(angel));
                    velocity *= 10.0f;
                    //particle = new Particle("particle", new Vector2(400, 100), velocity);
                    particle = particleFactory.create("Particle");
                    particle.SetPosition(new Vector2(400, 100));
                    particle.SetVelocity(velocity);
                    particleManager.Add(particle);
                }
            }

            particleManager.Update(gameTime);
        }
    }
}
