using Microsoft.Xna.Framework.Input;
using System;
using System.Linq;

namespace BimmCore.MonoGame
{
    public class KeyBoardListener
    {

        private Keys[] lastPressedKeys;
        private Action<Keys> keyDown;
        private Action<Keys> keyUp;

        public KeyBoardListener(Action<Keys> keyDown, Action<Keys> keyUp)
        {
            this.keyDown = keyDown;
            this.keyUp = keyUp;
        }
        /// <summary>
        /// Update the Keyboard Listener
        /// </summary>
        public void update()
        {

            KeyboardState currentState = Keyboard.GetState();
            Keys[] pressedKeys = currentState.GetPressedKeys();

            if (lastPressedKeys != null)
            {
                foreach (Keys key in pressedKeys)
                    if (!lastPressedKeys.Contains(key))
                        keyDown?.Invoke(key);

                foreach (Keys key in lastPressedKeys)
                    if (lastPressedKeys.Contains(key) && !pressedKeys.Contains(key))
                        keyUp?.Invoke(key);
            }
            lastPressedKeys = pressedKeys;
        }
    }
}
