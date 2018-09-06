using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Oikake.Util
{
    class CountDownTimer : Timer
    {
        public CountDownTimer() : base()
        {
            Initialize();
        }

        public CountDownTimer(float second) : base(second)
        {
            Initialize();
        }

        public override void Initialize()
        {
            currentTime = limitTime;
        }

        public override bool IsTime()
        {
            return currentTime <= 0.0f;
        }

        public override void Update(GameTime gameTime)
        {
            currentTime = Math.Max(currentTime - 1f, 0.0f);
        }

        public override float Rate()
        {
            return 1.0f - currentTime / limitTime;
        }
    }
}
