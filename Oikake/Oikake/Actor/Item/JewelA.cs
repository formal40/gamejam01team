﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Oikake.Def;
using Oikake.Scene;

namespace Oikake.Actor.Items
{
    /// <summary>
    /// 宝石A
    /// </summary>
    class JewelA : Item
    {
        public JewelA(string name, IGameMediator mediator) : base(name, mediator)
        {
            size = 80;
            position = new Vector2(Screen.Width, rnd.Next(0, Screen.Height - size));
            speed = -5;
            score = 1200;
        }
    }
}
