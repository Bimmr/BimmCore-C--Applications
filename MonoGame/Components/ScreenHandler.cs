using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace BimmCore.MonoGame.Components
{
    public class ScreenHandler : GameComponent
    {
        private Dictionary<string, Screen> screens;
        private Screen last, current;

        public KeyboardState curKeyboardState, lastKeyboardState;
        public MouseState curMouseState, lastMouseState;

        public ScreenHandler() : base(MonoHelper.Game)
        {
            screens = new Dictionary<string, Screen>();
        }

        /// <summary>
        /// Add screen to screen handler
        /// </summary>
        /// <param name="name"></param>
        /// <param name="screen"></param>
        public void addScreen(string name, Screen screen)
        {
            screens.Add(name, screen);
            screen.screenHandler = this;
        }

        /// <summary>
        /// Get the screen based on the name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Screen getScreen(string name)
        {
            foreach (KeyValuePair<string, Screen> entry in screens)
            {
                if (entry.Key.Equals(name))
                    return entry.Value;
            }
            return null;
        }

        /// <summary>
        /// Remove a screen
        /// </summary>
        /// <param name="screen"></param>
        public void remove(Screen screen)
        {
            Dictionary<string, Screen> s2 = new Dictionary<string, Screen>(screens);
            foreach (KeyValuePair<string, Screen> entry in s2)
            {
                if (entry.Value == screen)
                    screens.Remove(entry.Key);
            }
        }

        /// <summary>
        /// Remove a screen
        /// </summary>
        /// <param name="screen"></param>
        public void remove(string name)
        {
            screens.Remove(name);
        }

        /// <summary>
        /// Show the Screen based on the name
        /// </summary>
        /// <param name="name"></param>
        public void show(string name)
        {
            Screen screen = getScreen(name);
            current?.hide();

            last = current;

            current = screen;
            screen.show();
        }

        /// <summary>
        /// Get the last screen shown
        /// </summary>
        /// <returns></returns>
        public Screen getLast()
        {
            return last;
        }

        /// <summary>
        /// Get the current screen
        /// </summary>
        /// <returns></returns>
        public Screen getCurrent()
        {
            return current;
        }
        public override void Update(GameTime gameTime)
        {
            curMouseState = Mouse.GetState();
            curKeyboardState = Keyboard.GetState();

            current.Update(gameTime);

            lastMouseState = curMouseState;
            lastKeyboardState = curKeyboardState;
        }
    }
}