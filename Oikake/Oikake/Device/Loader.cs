using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oikake.Device
{
    abstract class Loader
    {
        protected string[,] resources;
        protected int counter;
        protected int maxNum;
        protected bool isEndFlag;

        public Loader(string[,] resources)
        {
            this.resources = resources;
        }

        public void Initialize()
        {
            counter = 0;
            isEndFlag = false;
            maxNum = 0;

            Debug.Assert(resources != null,
                "リソースデータ登録情報がおかしいです");
            maxNum = resources.GetLength(0);
        }

        public int RegistMAXNum()
        {
            return maxNum;
        }

        public int CurrentCount()
        {
            return counter;
        }

        public bool IsEnd()
        {
            return isEndFlag;
        }

        public abstract void Update(GameTime gameTime);

    }
}
