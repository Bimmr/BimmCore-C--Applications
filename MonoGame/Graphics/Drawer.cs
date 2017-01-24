using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BimmCore.MonoGame.Graphics
{
    public class Drawer
    {

        private static Texture2D tex;
        
        /// <summary>
        /// Draw a rectangle
        /// </summary>
        /// <param name="rectangle">Contains x, y, width, height</param>
        /// <param name="color">Color of the rectangle</param>
        public static void drawRectangle(Rectangle rectangle, Color color)
        {

            if(tex == null)
                tex = new Texture2D(MonoHelper.Graphics.GraphicsDevice, 1, 1);

            tex.SetData(new[] {color});
            MonoHelper.SpriteBatch.Draw(tex, rectangle, color);
        }
    }
}