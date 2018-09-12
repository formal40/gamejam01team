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
using Oikake.GameObject;

namespace Oikake.Scene
{
    interface IGameMediator
    {
        void AddActor(Gadget character);
        void AddScore();
        void AddScore(int num);
    }
    class GamePlay : IScene, IGameMediator
    {
        private GadgetManager gadgetManager;
        private Timer timer;
        private TimerUI timerUI;
        private Score score;
        private bool isEndFlag;
        private Sound sound;


        private BackGroundObject backGroundObjectFront;
        private BackGroundObject backGroundObjectCenter;
        private BackGroundObject backGroundObjectBack;

        private string backGroundObjectFrontName;
        private string backGroundObjectCenterName;
        private string backGroundObjectBackName;

        private OneTimeBGObject oneTimeBGObject;
        private string oneTimeBGObjectName;



        private enum Location
        {
            FIELD, FOREST, CAVE, NONE
        }

        private Location currentLocation;

        public GamePlay()
        {
            isEndFlag = false;
            var gameDevice = GameDevice.Instance();
            sound = gameDevice.GetSound();
        }

        public void Initialize()
        {
            isEndFlag = false;

            gadgetManager = new GadgetManager();

            Player player = new Player(this);
            gadgetManager.Add(player);

            gadgetManager.Add(new Black(this));

            timer = new CountDownTimer(60);
            timerUI = new TimerUI(timer);

            score = new Score();


            oneTimeBGObject = new OneTimeBGObject();

            backGroundObjectFront = new BackGroundObject();
            backGroundObjectFront.Initialize(18);
            backGroundObjectCenter = new BackGroundObject();
            backGroundObjectCenter.Initialize(9);
            backGroundObjectBack = new BackGroundObject();
            backGroundObjectBack.Initialize(3);

            currentLocation = Location.NONE;
            LocationChange(Location.FIELD);

            
        }

        private void LocationChange(Location location)
        {
            if (currentLocation == location) return;

            currentLocation = location;

            switch (currentLocation)
            {
                case Location.FIELD:
                    backGroundObjectFrontName = "mme";
                    backGroundObjectCenterName = "mnk";
                    backGroundObjectBackName = "mok";
                    break;
                case Location.FOREST:
                    backGroundObjectFrontName = "kari１.2";
                    backGroundObjectCenterName = "kari１.1";
                    backGroundObjectBackName = "kari１.0";
                    break;
                case Location.CAVE:
                    backGroundObjectFrontName = "mme";
                    backGroundObjectCenterName = "mnk";
                    backGroundObjectBackName = "mok";
                    break;
            }
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
            
            backGroundObjectBack.Dorw(renderer, backGroundObjectBackName);
            backGroundObjectCenter.Dorw(renderer, backGroundObjectCenterName);
            backGroundObjectFront.Dorw(renderer, backGroundObjectFrontName);

            oneTimeBGObject.Dorw(renderer, oneTimeBGObjectName);
            
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

            gadgetManager.Update(gameTime);


            sound.PlayBGM("gameplaybgm");


            //もし残り時間が21秒以下だったら
            if (timer.Now() <= 20)
            {
                LocationChange(Location.CAVE);
            }

            //もし残り時間が42秒以下だったら
            else if (timer.Now() <= 40)
            {
                LocationChange(Location.FOREST);
            }

            if (timer.Now() <= 21)
            {
                oneTimeBGObjectName = "nme";
                oneTimeBGObject.Start();
                oneTimeBGObject.Update();
            }

            if (timer.Now() <= 41)
            {
                oneTimeBGObjectName = "nme";
                oneTimeBGObject.Initialize(20);
                oneTimeBGObject.Update();
            }

            //時間切れか？
            if (timer.IsTime())
            {
                score.shutdown();
                isEndFlag = true;　//シーン終了へ
            }

            backGroundObjectFront.Update();
            backGroundObjectCenter.Update();
            backGroundObjectBack.Update();
        }
    }
}
