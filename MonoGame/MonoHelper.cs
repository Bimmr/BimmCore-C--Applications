using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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

        /// <summary>
        /// Setup MonoHelper for your MonoGame project
        /// </summary>
        /// <param name="game"></param>
        /// <param name="spriteBatch"></param>
        /// <param name="graphics"></param>
        public static void setup(Game game, SpriteBatch spriteBatch, GraphicsDeviceManager graphics)
        {
            Game = game;
            SpriteBatch = spriteBatch;
            Graphics = graphics;
            Size = new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
        }
    }
}