using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

using Oikake.Device;

namespace Oikake.Actor
{
    class CharacterManager
    {
        private List<Character> players;
        private List<Character> enemys;
        private List<Character> addNewCharacters;
        private List<Character> effects;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CharacterManager()
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
                players = new List<Character>();
            }

            if (enemys != null)
            {
                enemys.Clear();
            }
            else
            {
                enemys = new List<Character>();
            }

            if (addNewCharacters != null)
            {
                addNewCharacters.Clear();
            }
            else
            {
                addNewCharacters = new List<Character>();
            }

            if (effects != null)
            {
                effects.Clear();
            }
            else
            {
                effects = new List<Character>();
            }
        }

        /// <summary>
        /// 追加
        /// </summary>
        /// <param name="character">追加するキャラクター</param>
        public void Add(Character character)
        {
            if (character == null)
            {
                return;
            }
            addNewCharacters.Add(character);
        }

        private void HitToCharacters()
        {
            foreach(var player in players)
            {
                foreach(var enemy in enemys)
                {
                    if (player.IsDeed() || enemy.IsDeed())
                    {
                        continue;
                    }

                    if (player.IsCollision(enemy))
                    {
                        player.Hit(enemy);
                        enemy.Hit(player);
                    }
                }
            }
        }

        /// <summary>
        /// 死亡キャラの削除
        /// </summary>
        public void RemoveDeedCharacters()
        {
            players.RemoveAll(p => p.IsDeed());
            enemys.RemoveAll(e => e.IsDeed());
            effects.RemoveAll(e => e.IsDeed());
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
            foreach(var e in enemys)
            {
                e.Update(gameTime);
            }
            /*
            foreach (var e in effects)
            {
                e.Update(gameTime);
            }
            */

            foreach (var newChara in addNewCharacters)
            {
                if(newChara is Player)
                {
                    newChara.Initialize();
                    players.Add(newChara);
                }
                else if(newChara is PlayerBullet)
                {
                    newChara.Initialize();
                    players.Add(newChara);
                }
                else
                {
                    newChara.Initialize();
                    enemys.Add(newChara);
                }
            }
            addNewCharacters.Clear();
            HitToCharacters();
            RemoveDeedCharacters();
        }

        public void Draw(Renderer renderer)
        {
            foreach(var p in players)
            {
                p.Draw(renderer);
            }
            foreach (var e in enemys)
            {
                e.Draw(renderer);
            }
            foreach (var e in effects)
            {
                e.Draw(renderer);
            }
        }
    }
}
