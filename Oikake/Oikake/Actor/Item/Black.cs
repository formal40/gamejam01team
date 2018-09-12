using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Oikake.Def;
using Oikake.Scene;

namespace Oikake.Actor.Item
{
    /// <summary>
    /// 作成者：近藤卓
    /// 作成日：2018/09/09
    /// 概要　：アイテムクラスを継承したとりあえず収穫物の代わりの黒玉
    /// </summary>
    class Black : Item
    {
        public Black(IGameMediator mediator) : base("black", mediator)
        {
            size = 64;
            position = new Vector2(Screen.Width, 300);
            speed = -5;
            score = 100;
        }
    }
}
