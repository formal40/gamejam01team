using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using Oikake.Actor;
using Oikake.Actor.Items;
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

        private bool isOneTimeBGObject;

        private CountDownTimer countDownTimer;



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
            oneTimeBGObjectName = "nok";
            oneTimeBGObject.Initialize(35);

            backGroundObjectFront = new BackGroundObject();
            backGroundObjectFront.Initialize(20);
            backGroundObjectCenter = new BackGroundObject();
            backGroundObjectCenter.Initialize(10);
            backGroundObjectBack = new BackGroundObject();
            backGroundObjectBack.Initialize(3);

            isOneTimeBGObject = false;

            currentLocation = Location.NONE;
            LocationChange(Location.FIELD);

            countDownTimer = new CountDownTimer(1);

            
        }

        private void LocationChange(Location location)
        {
            if (currentLocation == location) return;

            currentLocation = location;

            switch (currentLocation)
            {
                case Location.FIELD:
                    backGroundObjectFrontName =  "FieldFieldbgoFront";
                    backGroundObjectCenterName = "FieldFieldbgoCenter";
                    backGroundObjectBackName = "FieldFieldbgoBack";
                    gadgetManager.ItemAllDead();
                    break;
                case Location.FOREST:
                    backGroundObjectFrontName = "ForestFieldbgoFront";
                    backGroundObjectCenterName = "ForestFieldbgoCenter";
                    backGroundObjectBackName = "ForestfieldbgoBack";
                    gadgetManager.ItemAllDead();
                    break;
                case Location.CAVE:
                    backGroundObjectFrontName = "CabeFieldBgoFlont";
                    backGroundObjectCenterName = "CabeFieldbgoSenter";
                    backGroundObjectBackName = "CabeFieldBgoBack";
                    gadgetManager.ItemAllDead();
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
        
            gadgetManager.Draw(renderer);

            oneTimeBGObject.Dorw(renderer, oneTimeBGObjectName);

            //timerUI.Draw(renderer);
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

            countDownTimer.Update(gameTime);

            if (countDownTimer.IsTime())
            {
                CreateItem();
                countDownTimer.Initialize();
            }


            sound.PlayBGM("FieldandForestBGM");


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
                oneTimeBGObjectName = "fe-doBracl";
                if (!isOneTimeBGObject)
                {
                    oneTimeBGObject.Start();
                    isOneTimeBGObject = true;
                }
                oneTimeBGObject.Update();
            }

            else if (timer.Now() <= 41)
            {
                oneTimeBGObjectName = "fe-doForest";
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

        private void CreateItem()
        {
            Random random = new Random();
            int rnd = random.Next(0, 4);

            int rnd2 = random.Next(0, 19);

            switch (currentLocation)
            {
                case Location.FIELD:
                    if (rnd == 0) gadgetManager.Add(new SweetPotato("FieldFieldSweet potato", this));
                    if (rnd == 1) gadgetManager.Add(new Pumpkin("FieldFieldPumpukin", this));
                    if (rnd == 2) gadgetManager.Add(new EarRice("FieldFieldEaRice", this));
                    if (rnd == 3) gadgetManager.Add(new Carrot("FieldFieldCarrot", this));
                    break;
                case Location.FOREST:
                    if (rnd == 0) gadgetManager.Add(new Chestnut("FieldForestChestnut", this));
                    if (rnd == 1) gadgetManager.Add(new Apple("FieldForestApple", this));
                    if (rnd == 2) gadgetManager.Add(new Mandragora("FieldForestMandora", this));
                    if (rnd == 3) gadgetManager.Add(new Mushroom("FieldForestMushroom", this));
                    break;
                case Location.CAVE:
                    if (rnd2 == 0) gadgetManager.Add(new JewelA("FieldCaveHousekiA", this));
                    else if (rnd2 == 1) gadgetManager.Add(new JewelB("FieldCaveHousekiB", this));
                    else  gadgetManager.Add(new Stone("FieldCabeStone", this));
                    break;
            }
        }

    }
}
