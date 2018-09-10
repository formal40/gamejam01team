﻿using Microsoft.Xna.Framework;
using Oikake.Actor;
using Oikake.Device;
using Oikake.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oikake.Scene
{
    /// <summary>
    /// 作成者：近藤卓
    /// 作成日：2018/09/10
    /// 概要　：ゲーム本編の畑クラス
    /// </summary>
    class Field : IScene, IGameMediator
    {
        private GadgetManager gadgetManager;
        private Timer timer;
        private TimerUI timerUI;
        private Score score;
        private bool isEndFlag;
        private Sound sound;

        public Field(Score score)
        {
            isEndFlag = false;
            var gameDevice = GameDevice.Instance();
            sound = gameDevice.GetSound();
            this.score = score;
        }

        public void Initialize()
        {
            isEndFlag = false;

            gadgetManager = new GadgetManager();

            Player player = new Player(this);
            gadgetManager.Add(player);

            gadgetManager.Add(new Black(this));

            timer = new CountDownTimer(10);
            timerUI = new TimerUI(timer);
            score.Initialize();

            //score = new Score();
        }

        public void AddActor(Gadget character)
        {
            gadgetManager.Add(character);
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

            gadgetManager.Draw(renderer);

            timerUI.Draw(renderer);
            score.Draw(renderer);

            //描画終了
            renderer.End();
        }



        public bool IsEnd()
        {
            return isEndFlag;
        }

        public Scene Next()
        {
            return Scene.Forest;
        }

        public void Shutdown()
        {
            sound.StopBGM();
        }

        public void Update(GameTime gameTime)
        {
            timer.Update(gameTime);
            score.Update(gameTime);

            gadgetManager.Update(gameTime);

            if (timer.Now() == 5.0)
            {
                gadgetManager.Add(new Black(this));
            }

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
