using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using System.Diagnostics;

namespace Oikake.Device
{
    class Sound
    {
        #region　フィールドとコンストラクタ
        private ContentManager contentManager;
        private Dictionary<string, Song> bgms;
        private Dictionary<string, SoundEffect> soundEffects;
        private Dictionary<string, SoundEffectInstance> seInstances;
        private Dictionary<string, SoundEffectInstance> sePlayDict;
        private string currentBGM;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="content">Game1のコンテンツ管理</param>
        public Sound(ContentManager content)
        {
            contentManager = content;
            MediaPlayer.IsRepeating = true;

            bgms = new Dictionary<string, Song>();
            soundEffects = new Dictionary<string, SoundEffect>();
            seInstances = new Dictionary<string, SoundEffectInstance>();

            sePlayDict = new Dictionary<string, SoundEffectInstance>();

            currentBGM = null;
        }

        /// <summary>
        /// 解放
        /// </summary>
        public void Unload()
        {
            bgms.Clear();
            soundEffects.Clear();
            seInstances.Clear();
            sePlayDict.Clear();
        }

        #endregion　フィールドとコンストラクタ

        private string ErrorMessage(string name)
        {
            return "再生する音データのアセット名（" + name + "）がありません" +
                "アセット名の確認、Dictionaryに登録しているか確認してください";
        }

        #region BGM(MP3:MediaPlayer)関連

        /// <summary>
        /// BGM（MP3）の読み込み
        /// </summary>
        /// <param name="name">アセット名</param>
        /// <param name="filephth">ファイルパス</param>
        public void LoadBGM(string name, string filephth = "./")
        {
            if(bgms.ContainsKey(name))
            {
                return;
            }

            bgms.Add(name, contentManager.Load<Song>(filephth + name));
        }

        /// <summary>
        /// BGMは停止中か？
        /// </summary>
        /// <returns>停止中ならtrue</returns>
        public bool IsStoppedBGM()
        {
            return (MediaPlayer.State == MediaState.Stopped);
        }

        /// <summary>
        /// BGMが再生中か？
        /// </summary>
        /// <returns>再生中ならtrue</returns>
        public bool IsPlayingBGM()
        {
            return (MediaPlayer.State == MediaState.Playing);
        }

        /// <summary>
        /// BGMが一時停止中か？
        /// </summary>
        /// <returns>一時停止中ならtrue</returns>
        public bool IsPausedBGM()
        {
            return (MediaPlayer.State == MediaState.Paused);
        }

        /// <summary>
        /// BGMを停止
        /// </summary>
        public void StopBGM()
        {
            MediaPlayer.Stop();
            currentBGM = null;
        }
        /// <summary>
        /// BGM再生
        /// </summary>
        /// <param name="name"></param>
        public void PlayBGM(string name)
        {
            Debug.Assert(bgms.ContainsKey(name), ErrorMessage(name));

            if (currentBGM == name)
            {
                return;
            }

            if(IsPlayingBGM())
            {
                StopBGM();
            }

            MediaPlayer.Volume = 0.5f;

            currentBGM = name;

            MediaPlayer.Play(bgms[currentBGM]);
        }

        /// <summary>
        /// BGMの一時停止
        /// </summary>
        public void PauseBGM()
        {
            if(IsPlayingBGM())
            {
                MediaPlayer.Pause();
            }
        }

        /// <summary>
        /// 一時停止からの再生
        /// </summary>
        public void ResumeBGM()
        {
            if(IsPausedBGM())
            {
                MediaPlayer.Resume();
            }
        }

        public void ChangeBGMLoopFlag(bool loopFlag)
        {
            MediaPlayer.IsRepeating = loopFlag;
        }

        #endregion BGM(MP3:MediaPlayer)関連



        #region WAV(SE;SoundEffect)関連

        public void LoadSE(string name, string filepath = "./")
        {
            if(soundEffects.ContainsKey(name))
            {
                return;
            }
            soundEffects.Add(name, contentManager.Load<SoundEffect>(filepath + name));
        }

        public void PlaySE(string name)
        {
            Debug.Assert(soundEffects.ContainsKey(name), ErrorMessage(name));

            soundEffects[name].Play();
        }

        #endregion //WAV(SE:SoundEffect)関連


        #region WAVインスタンスの作成

        /// <summary>
        /// WACインスタンスの作成
        /// </summary>
        /// <param name="name">アセット名</param>
        public void CreateSEInstance(string name)
        {
            if(seInstances.ContainsKey(name))
            {
                return;
            }
        }
        /// <summary>
        /// 指定SEの停止
        /// </summary>
        /// <param name="name"></param>
        /// <param name="no"></param>
        public void StoppedSE(string name, int no)
        {
            if(sePlayDict[name+no].State==SoundState.Playing)
            {
                sePlayDict[name + no].Stop();
            }
        }

        /// <summary>
        /// 再生中のSEｗｐすべて停止
        /// </summary>
        public void StoppedSE()
        {
            foreach(var se in sePlayDict)
            {
                if(se.Value.State==SoundState.Playing)
                {
                    se.Value.Stop();
                }
            }
        }

        /// <summary>
        /// 指定したSEｗｐ削除
        /// </summary>
        /// <param name="name">アセット名</param>
        /// <param name="no">管理番号</param>
        public void Remove(string name,int no)
        {
            if(sePlayDict.ContainsKey(name+no)==false)
            {
                return;
            }
            sePlayDict.Remove(name + no);
        }

        /// <summary>
        /// すべてのSEを削除
        /// </summary>
        public void RemoveSE()
        {
            sePlayDict.Clear();
        }

        /// <summary>
        /// 指定したSEを一時停止
        /// </summary>
        /// <param name="name">アセット名</param>
        /// <param name="no">管理番号</param>
        public void PauseSE(string name, int no)
        {
            if(sePlayDict.ContainsKey(name+no)==false)
            {
                return;
            }

            if (sePlayDict[name + no].State==SoundState.Playing)
            {
                sePlayDict[name + no].Pause();
            }
        }

        /// <summary>
        /// すべてのSEを一時停止
        /// </summary>
        public void PauseSE()
        {
            foreach(var se in sePlayDict)
            {
                if(se.Value.State==SoundState.Playing)
                {
                    se.Value.Pause();
                }
            }
        }
        /// <summary>
        /// 指定したSEを一時停止
        /// </summary>
        /// <param name="name">アセット名</param>
        /// <param name="no">管理番号</param>
        public void ResumeSE(string name,int no)
        {
            if(sePlayDict.ContainsKey(name+no)==false)
            {
                return;
            }

            if(sePlayDict[name+no].State==SoundState.Paused)
            {
                sePlayDict[name + no].Resume();
            }
        }

        /// <summary>
        /// 一時停止中のすべてのSEを復帰
        /// </summary>
        public void ResumeSE()
        {
            foreach(var se in sePlayDict)
            {
                if(se.Value.State==SoundState.Paused)
                {
                    se.Value.Resume();
                }
            }
        }

        /// <summary>
        /// SEインスタンスが再生中か？
        /// </summary>
        /// <param name="name">アセット名</param>
        /// <param name="no">管理番号</param>
        /// <returns>再生中ならtrue</returns>
        public bool IsPlayingSEInstance(string name,int no)
        {
            return sePlayDict[name + no].State == SoundState.Playing;
        }

        /// <summary>
        /// SEインスタンスが一時停止中か？
        /// </summary>
        /// <param name="name"></param>
        /// <param name="no"></param>
        /// <returns></returns>
        public bool IsStoppedSEInstance(string name, int no)
        {
            return sePlayDict[name + no].State == SoundState.Stopped;
        }

        public bool IsPausedSEInstance(string name,int no)
        {
            return sePlayDict[name + no].State == SoundState.Paused;
        }

        #endregion WAVインスタンス関連
    }
}