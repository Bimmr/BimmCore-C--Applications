using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BimmCore.MonoGame
{
    public class MonoHelper
    {
        public static SpriteBatch SpriteBatch;
        public static GraphicsDeviceManager Graphics;
        public static Vector2 Size;
        public static Game Game;

        //Middle point of the window
        public static Vector2 Middle => new Vector2(Size.X / 2, Size.Y / 2);

        public static void setup(Game game, SpriteBatch spriteBatch, GraphicsDeviceManager graphics)
        {
            MonoHelper.Game = game;
            MonoHelper.SpriteBatch = spriteBatch;
            MonoHelper.Graphics = graphics;
            MonoHelper.Size = new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
        }
    }
}
