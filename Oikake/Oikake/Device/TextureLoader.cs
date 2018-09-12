using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oikake.Device
{
    class TextureLoader : Loader
    {
        private Renderer renderer;

        public TextureLoader(string[,] resources)
            : base(resources)
        {
            renderer = GameDevice.Instance().GetRenderer();
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            isEndFlag = true;
            if (counter < maxNum)
            {
                renderer.LoadContent(resources[counter, 0], resources[counter, 1]);
                counter++;
                isEndFlag = false;
            }
        }
    }
}
