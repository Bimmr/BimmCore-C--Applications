using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace BimmCore.MonoGame.Graphics
{
    public static class Drawer
    {

        private static Dictionary<Color, Texture2D> texCache = new Dictionary<Color, Texture2D>();

        /// <summary>
        /// Draw a rectangle with a texture for just this instance
        /// </summary>
        /// <param name="rectangle"></param>
        /// <param name="color"></param>
        public static void drawRectangle(Rectangle rectangle, Color color)
        {
            Texture2D tex;

            if (texCache.ContainsKey(color))
                tex = texCache[color];
            else
            {
                tex = new Texture2D(MonoHelper.Graphics.GraphicsDevice, 1, 1);
                texCache.Add(color, tex);
            }

            tex.SetData(new[] {color});
            MonoHelper.SpriteBatch.Draw(tex, rectangle, color);
        }

    }
}