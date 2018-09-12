using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Oikake.Actor.Items;

using Oikake.Device;

namespace Oikake.Actor
{
    /// <summary>
    /// 作成者：近藤卓
    /// 作成日：2018/09/09
    /// 概要　：ガジェット管理クラス
    /// </summary>
    class GadgetManager
    {
        private List<Gadget> players;
        private List<Gadget> playerBullets;
        private List<Gadget> items;
        private List<Gadget> addNewGadgets;
        private List<Gadget> effects;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public GadgetManager()
        {
            Initialize();
        }

        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialize()
        {
            if (players != null)
            {
                players.Clear();
            }
            else
            {
                players = new List<Gadget>();
            }

            if (playerBullets != null)
            {
                playerBullets.Clear();
            }
            else
            {
                playerBullets = new List<Gadget>();
            }

            if (items != null)
            {
                items.Clear();
            }
            else
            {
                items = new List<Gadget>();
            }

            if (addNewGadgets != null)
            {
                addNewGadgets.Clear();
            }
            else
            {
                addNewGadgets = new List<Gadget>();
            }

            if (effects != null)
            {
                effects.Clear();
            }
            else
            {
                effects = new List<Gadget>();
            }
        }

        /// <summary>
        /// 追加
        /// </summary>
        /// <param name="gadget">追加するガジェット</param>
        public void Add(Gadget gadget)
        {
            if (gadget == null)
            {
                return;
            }
            addNewGadgets.Add(gadget);
        }

        private void HitToGadgets()
        {
            foreach(var playerBullet in playerBullets)
            {
                foreach(var item in items)
                {
                    if (playerBullet.IsDead() || item.IsDead())
                    {
                        continue;
                    }

                    if (playerBullet.IsCollision(item))
                    {
                        //player.Hit(item);
                        item.Hit(playerBullet);
                    }
                }
            }
        }

        /// <summary>
        /// 死亡キャラの削除
        /// </summary>
        public void RemoveDeadGadgets()
        {
            players.RemoveAll(p => p.IsDead());
            playerBullets.RemoveAll(pb => pb.IsDead());
            items.RemoveAll(i => i.IsDead());
            effects.RemoveAll(e => e.IsDead());
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="gameTime">ゲーム時間</param>
        public void Update(GameTime gameTime)
        {
            foreach(var p in players)
            {
                p.Update(gameTime);
            }
            foreach (var pb in playerBullets)
            {
                pb.Update(gameTime);
            }
            foreach (var i in items)
            {
                i.Update(gameTime);
            }
            /*
            foreach (var e in effects)
            {
                e.Update(gameTime);
            }
            */

            foreach (var newGadget in addNewGadgets)
            {
                if(newGadget is Player)
                {
                    newGadget.Initialize();
                    players.Add(newGadget);
                }
                else if(newGadget is PlayerBullet)
                {
                    newGadget.Initialize();
                    playerBullets.Add(newGadget);
                }
                else
                {
                    newGadget.Initialize();
                    items.Add(newGadget);
                }
            }
            addNewGadgets.Clear();
            HitToGadgets();
            RemoveDeadGadgets();
        }

        public void Draw(Renderer renderer)
        {

            foreach (var i in items)
            {
                i.Draw(renderer);
            }
            foreach (var p in players)
            {
                p.Draw(renderer);
            }
            foreach (var e in effects)
            {
                e.Draw(renderer);
            }
        }

        public void ItemAllDead()
        {
            items.Clear();

            List<Gadget> temp = new List<Gadget>();
            addNewGadgets.RemoveAll(a => IsItem(a));
        }

        private bool IsItem(Gadget other)
        {
            return other is Item;
        }
    }
}
