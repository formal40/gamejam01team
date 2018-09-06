using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oikake.Device
{
    static class Input
    {
        private static Vector2 velocity = Vector2.Zero;

        private static KeyboardState currentKey;
        private static KeyboardState previousKey;

        private static MouseState currentMouse;
        private static MouseState previousMouse;

        public static void Update()
        {
            previousKey = currentKey;
            currentKey = Keyboard.GetState();

            previousMouse = currentMouse;
            currentMouse = Mouse.GetState();

            UpdateVelocity();
        }

        public static Vector2 Velocity()
        {
            return velocity;
        }

        private static void UpdateVelocity()
        {
            velocity = Vector2.Zero;

            if(currentKey.IsKeyDown(Keys.Right))
            {
                velocity.X += 1.0f;
            }

            if (currentKey.IsKeyDown(Keys.Left))
            {
                velocity.X -= 1.0f;
            }

            if (currentKey.IsKeyDown(Keys.Up))
            {
                velocity.Y -= 1.0f;
            }

            if (currentKey.IsKeyDown(Keys.Down))
            {
                velocity.Y += 1.0f;
            }


            if (velocity.Length() != 0)
            {
                velocity.Normalize();
            }
        }

        /// <summary>
        /// キーが押された瞬間か？
        /// </summary>
        /// <param name="key">チェックしていたキー</param>
        /// <returns>現在キーが押されていて、1フレーム前に押されていなければtrue</returns>
        public static bool IsKeyDown(Keys key)
        {
            return currentKey.IsKeyDown(key) && !previousKey.IsKeyDown(key);
        }

        /// <summary>
        /// キーが押された瞬間か？
        /// </summary>
        /// <param name="key">チェックしたいキー</param>
        /// <returns>押された瞬間なら</returns>
        public static bool GetKeyTrigger(Keys key)
        {
            return IsKeyDown(key);
        }

        /// <summary>
        /// キーが押された瞬間か？
        /// </summary>
        /// <param name="key">調べたいキー</param>
        /// <returns>キーが押されていたらtrue</returns>
        public static bool GetKeyState(Keys key)
        {
            return currentKey.IsKeyDown(key);
        }

        ///マウス関連
        /// <summary>
        /// マウスの左ボタンが押された瞬間か？
        /// </summary>
        /// <returns>現在押されていて、1フレーム前に押されていなければture</returns>
        public static bool IsMouseLBottonDown()
        {
            return currentMouse.LeftButton == ButtonState.Pressed && previousMouse.LeftButton == ButtonState.Released;
        }
        
        /// <summary>
        /// マウスの左ボタンが押された瞬間か？
        /// </summary>
        /// <returns>現在離されていて、1フレーム前に押されていたらture</returns>
        public static bool IsMouseLBottonUp()
        {
            return currentMouse.LeftButton == ButtonState.Released && previousMouse.LeftButton == ButtonState.Pressed;
        }
        
        /// <summary>
        /// マウスの左ボタンが押されているか？
        /// </summary>
        /// <returns>左ボタンが押されていたらture</returns>
        public static bool IsMouseLBotton()
        {
            return currentMouse.LeftButton == ButtonState.Pressed;
        }
        
        /// <summary>
        /// マウスの右ボタンが押された瞬間か？
        /// </summary>
        /// <returns>現在押されていて、1フレーム前に押されていなければture</returns>
        public static bool IsMouseRBottonDown()
        {
            return currentMouse.RightButton == ButtonState.Pressed && previousMouse.RightButton == ButtonState.Released;
        }

        /// <summary>
        /// マウスの右ボタンが離された瞬間か？
        /// </summary>
        /// <returns>現在離されていて、1フレーム前に押されていたらture</returns>
        public static bool IsMouseRBottonUp()
        {
            return currentMouse.RightButton == ButtonState.Pressed && previousMouse.RightButton == ButtonState.Released;
        }

        /// <summary>
        /// マウスの右ボタンが押されているか？
        /// </summary>
        /// <returns>左ボタンが押されたいたらture</returns>
        public static bool IsMouseRBotton()
        {
            return currentMouse.RightButton == ButtonState.Pressed;
        }

        /// <summary>
        /// マウスの位置を返す
        /// </summary>
        public static Vector2 MousePosition
        {
            get
            {
                return new Vector2(currentMouse.X, currentMouse.Y);
            }
        }

        public static int GetMouseWheel()
        {
            return previousMouse.ScrollWheelValue - currentMouse.ScrollWheelValue;
        }
    }
}
