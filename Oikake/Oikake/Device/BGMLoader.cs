using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oikake.Device
{
    class BGMLoader :Loader
    {
        private Sound sound;

        public BGMLoader(string[,] resources)
            : base(resources)
        {
            sound = GameDevice.Instance().GetSound();
            Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            isEndFlag = true;
            if (counter < maxNum)
            {
                sound.LoadBGM(resources[counter, 0], resources[counter, 1]);
                counter++;
                isEndFlag = false;
            }
        }
    }
}
