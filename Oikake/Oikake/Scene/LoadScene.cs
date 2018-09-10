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
                {"oikake_enemy_4anime",path },
                {"oikake_player_4anime",path },
                {"particle",path },
                {"particleBlue",path },
                {"puddle",path },
                {"score",path },
                {"stage",path },
                {"timer",path },
                {"title",path },
                {"white",path },
                {"pipo-btleffect",path },
                {"nc47171",path }
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
                {"gameplaybgm",path },
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
                {"gameplayse",path },
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

            renderer.DrawTexture("load", new Vector2(20, 20));

            int currentCount =
                textureLoader.CurrentCount() +
                bgmLoader.CurrentCount() +
                seLoader.CurrentCount();

            if (totalResouceNum != 0)
            {
                float rate = (float)currentCount / totalResouceNum;
                renderer.DrawNumber(
                    "number",
                    new Vector2(20, 100),
                    (int)(rate * 100.0f));

                renderer.DrawTexture(
                    "fade",
                    new Vector2(0, 500),
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
