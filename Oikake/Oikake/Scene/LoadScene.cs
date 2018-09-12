using Microsoft.Xna.Framework;
using Oikake.Def;
using Oikake.Device;
using Oikake.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oikake.Scene
{
    class LoadScene : IScene
    {
        private Renderer renderer;

        private TextureLoader textureLoader;
        private BGMLoader bgmLoader;
        private SELoader seLoader;

        private int totalResouceNum;
        private bool isEndFlag;
        private Timer timer;

        #region テクスチャ用

        private string[,] textureMatrix()
        {
            string path = "./";

            string[,] data = new string[,]
            {
                {"black",path },
                {"ending",path },
                {"score",path },
                {"stage",path },
                {"timer",path },
                {"title",path },
                {"white",path },
                {"startButton",path },
                {"CabeFieldBgoBack",path },
                {"CabeFieldbgoSenter",path },
                {"CabeFieldBgoFlont",path },
                { "ForestfieldbgoBack", path },
                { "ForestFieldbgoCenter", path },
                { "ForestFieldbgoFront", path },
                {"nme",path },
                {"nok",path },
                {"FieldHundClose",path },
                {"FieldHundOpen",path },
                {"FieldFieldSweet potato",path },
                {"FieldFieldCarrot",path },
                {"FieldFieldEaRice",path },
                {"FieldForestMushroom",path },
                {"FieldCabeStone",path },
                {"FieldForestApple",path },
                {"FieldFieldPumpukin",path },
                {"FieldTruck4",path },
                {"FieldTruck3",path },
                {"FieldTruck2",path },
                {"FieldTruck1",path },
                {"FieldForestMandora",path },
                {"FieldCaveHousekiA",path },
                {"FieldCaveHousekiB",path },
                {"FieldForestChestnut",path },
                {"fe-doForest",path },
                {"fe-doBracl",path },
            };

            return data;
        }

        #endregion テクスチャ用

        #region BGM用

        private string[,] BGMMatrix()
        {
            string path = "./Sound/";

            string[,] data = new string[,]
            {
                {"endingbgm",path },
                {"FieldandForestBGM",path },
                {"titlebgm",path },
            };

            return data;

        }
        #endregion BGM用

        #region SE用

        private string[,] SEMatrix()
        {
            string path = "./Sound/";

            string[,] data = new string[,]
            {
                {"endingse",path },
                {"HandSE",path },
                {"titlese",path },
            };

            return data;
        }

        #endregion SE用

        public LoadScene()
        {
            renderer = GameDevice.Instance().GetRenderer();

            textureLoader = new TextureLoader(textureMatrix());
            bgmLoader = new BGMLoader(BGMMatrix());
            seLoader = new SELoader(SEMatrix());
            isEndFlag = false;

            timer = new CountDownTimer(0.1f);
        }

        public void Draw(Renderer renderer)
        {
            renderer.Begin();

            renderer.DrawTexture("rogo", Vector2.Zero);
            renderer.DrawTexture("loadNow", new Vector2(Screen.Width - 600, Screen.Height - 64));

            int currentCount =
                textureLoader.CurrentCount() +
                bgmLoader.CurrentCount() +
                seLoader.CurrentCount();

            if (totalResouceNum != 0)
            {
                float rate = (float)currentCount / totalResouceNum;

                //renderer.DrawNumber(
                //    "number",
                //    new Vector2(20,100),
                //    (int)(rate * 100.0f));

                renderer.DrawTexture(
                    "fade",
                    new Vector2(0, 600),
                    null,
                    0.0f,
                    Vector2.Zero,
                    new Vector2(rate * Screen.Width, 20));

                if (textureLoader.IsEnd() && bgmLoader.IsEnd() && seLoader.IsEnd())
                {
                    isEndFlag = true;
                }

                renderer.End();
            }
        }

        public void Initialize()
        {
            isEndFlag = false;
            textureLoader.Initialize();
            bgmLoader.Initialize();
            seLoader.Initialize();
            totalResouceNum =
                textureLoader.RegistMAXNum() +
                bgmLoader.RegistMAXNum() +
                seLoader.RegistMAXNum();
        }

        public bool IsEnd()
        {
            return isEndFlag;
        }

        public Scene Next()
        {
            return Scene.Title;
        }

        public void Shutdown()
        {

        }

        public void Update(GameTime gameTime)
        {
            timer.Update(gameTime);
            if (timer.IsTime() == false)
            {
                return;
            }
            timer.Initialize();

            if (textureLoader.IsEnd() == false)
            {
                textureLoader.Update(gameTime);
            }
            else if (bgmLoader.IsEnd() == false)
            {
                bgmLoader.Update(gameTime);
            }
            else if (seLoader.IsEnd() == false)
            {
                seLoader.Update(gameTime);
            }
        }
    }
}
