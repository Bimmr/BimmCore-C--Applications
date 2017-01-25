using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BimmCore.MonoGame.Graphics
{
    public class Drawer
    {

        /// <summary>
        /// Draw a rectangle with a public texture
        /// </summary>
        /// <param name="rectangle">Contains x, y, width, height</param>
        /// <param name="color">Color of the rectangle</param>
        /// <param name="newTex">Create a new Texture for the drawing</param>
        public static void drawRectangle(Rectangle rectangle, Color color)
        {
            Texture2D tex = new Texture2D(MonoHelper.Graphics.GraphicsDevice, 1, 1);

            tex.SetData(new[] { color });
            MonoHelper.SpriteBatch.Draw(tex, rectangle, color);
        }

        private Texture2D tex;
        /// <summary>
        /// Draw a rectangle with a texture for just this instance
        /// </summary>
        /// <param name="rectangle"></param>
        /// <param name="color"></param>
        public void drawRectangleP(Rectangle rectangle, Color color)
        {

            if(tex == null)
                tex = new Texture2D(MonoHelper.Graphics.GraphicsDevice, 1, 1);

            tex.SetData(new[] {color});
            MonoHelper.SpriteBatch.Draw(tex, rectangle, color);
        }
    }
}