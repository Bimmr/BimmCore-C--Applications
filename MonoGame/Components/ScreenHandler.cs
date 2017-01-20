using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BimmCore.MonoGame.Components
{
    public class ScreenHandler
    {

        private Dictionary<string, Screen> screens;
        private Screen last, current;

        public ScreenHandler()
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
        /// Show the Screen based on the name
        /// </summary>
        /// <param name="name"></param>
        public void show(string name)
        {
            Screen screen = getScreen(name);
            if (current != null)
                current.hide();
            last = current;

            current = screen;
            screen.show();
        }

        /// <summary>
        /// Get the last screen shown
        /// </summary>
        /// <returns></returns>
        public Screen getLast() { return last; }

        /// <summary>
        /// Get the current screen
        /// </summary>
        /// <returns></returns>
        public Screen getCurrent() { return current; }
    }
}
