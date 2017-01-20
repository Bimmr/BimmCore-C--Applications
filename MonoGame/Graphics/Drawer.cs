using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BimmCore.MonoGame.Graphics
{
    public class Drawer
    {
        
        /// <summary>
        /// Draw a rectangle
        /// </summary>
        /// <param name="rectangle">Contains x, y, width, height</param>
        /// <param name="color">Color of the rectangle</param>
        public static void drawRectangle(Rectangle rectangle, Color color)
        {

            Texture2D texture = new Texture2D(MonoHelper.Graphics.GraphicsDevice, 1, 1);
            texture.SetData(new[] {color});
            MonoHelper.SpriteBatch.Draw(texture, rectangle, color);
        }
    }
}