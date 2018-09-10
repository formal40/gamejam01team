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

        private static GamePadState currentButton;
        private static GamePadState previousButton;

        public static void Update()//変更点。
        {
            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);
            previousKey = currentKey;
            currentKey = Keyboard.GetState();

            previousMouse = currentMouse;
            currentMouse = Mouse.GetState();

            previousButton = currentButton;
            currentButton = gamePadState;

            UpdateVelocity(gamePadState);
        }

        public static Vector2 Velocity()
        {
            return velocity;
        }

        private static void UpdateVelocity(GamePadState gamePadState)//変更点。移動の出力の変更。
        {
            velocity = Vector2.Zero;

            if (currentKey.IsKeyDown(Keys.Right) || gamePadState.IsButtonDown(Buttons.DPadRight) || gamePadState.IsButtonDown(Buttons.LeftThumbstickRight))
            {
                velocity.X += 1.0f;
            }

            if (currentKey.IsKeyDown(Keys.Left) || gamePadState.IsButtonDown(Buttons.DPadLeft) || gamePadState.IsButtonDown(Buttons.LeftThumbstickLeft))
            {
                velocity.X -= 1.0f;
            }

            if (currentKey.IsKeyDown(Keys.Up) || gamePadState.IsButtonDown(Buttons.DPadUp) || gamePadState.IsButtonDown(Buttons.LeftThumbstickUp))
            {
                velocity.Y -= 1.0f;
            }

            if (currentKey.IsKeyDown(Keys.Down) || gamePadState.IsButtonDown(Buttons.DPadDown) || gamePadState.IsButtonDown(Buttons.LeftThumbstickDown))
            {
                velocity.Y += 1.0f;
            }

            if (velocity.Length() != 0)
            {
                velocity.Normalize();
            }
        }

        public static bool IsButtonDown(Buttons button)//ボタンが押されたか、前にフレームに押されていなければtrue
        {
            return currentButton.IsButtonDown(button) && !previousButton.IsButtonDown(button);
        }

        public static bool IsButtonUp(Buttons button)//ボタンが離れ続けているか
        {
            return !currentButton.IsButtonDown(button) && !previousButton.IsButtonDown(button);
        }

        public static bool GetButtonDown(Buttons button)//ボタンが押され続けているか
        {
            return currentButton.IsButtonDown(button);
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
    }
}
