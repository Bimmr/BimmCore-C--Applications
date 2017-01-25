using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BimmCore.MonoGame
{
    public class KeyBoardListener
    {

        private Keys[] lastPressedKeys;
        private Action<Keys> keyDown;
        private Action<Keys> keyUp;


        /// <summary>
        /// Update the Keyboard Listener
        /// </summary>
        public void update()
        {

            KeyboardState currentState = Keyboard.GetState();
            Keys[] pressedKeys = currentState.GetPressedKeys();

            foreach (Keys key in pressedKeys)
                if (!lastPressedKeys.Contains(key))
                    keyDown?.Invoke(key);

            foreach (Keys key in lastPressedKeys)
                if (lastPressedKeys.Contains(key) && !pressedKeys.Contains(key))
                    keyUp?.Invoke(key);

            lastPressedKeys = pressedKeys;
        }

    }
}
