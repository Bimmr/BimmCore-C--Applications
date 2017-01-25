using Microsoft.Xna.Framework;

namespace BimmCore.MonoGame
{
    /// <summary>
    /// Create an easy rectangle by giving 2 points
    /// Do not worry about if values are bigger/smaller then other values as EasyRectangle sorts it out for you.
    /// </summary>
    public class EasyRectangle
    {
        private Vector2 p1;
        private Vector2 p2;

        /// <summary>
        /// Create the rectangle
        /// </summary>
        /// <param name="p1">Point 1</param>
        /// <param name="p2">Point 2</param>
        public EasyRectangle(Vector2 p1, Vector2 p2)
        {
            if (p1.X < p2.X)
            {
                this.p1.X = p1.X;
                this.p2.X = p2.X;
            }
            else
            {
                this.p2.X = p1.X;
                this.p1.X = p2.X;
            }
            if (p1.Y < p2.Y)
            {
                this.p1.Y = p1.Y;
                this.p2.Y = p2.Y;
            }
            else
            {
                this.p2.Y = p1.Y;
                this.p1.Y = p2.Y;
            }
        }

        /// <summary>
        /// Get the Rectangle
        /// </summary>
        /// <returns>Rectangle</returns>
        public Rectangle getRectangle()
        {
            return new Rectangle((int) p1.X, (int) p1.Y, (int) (p2.X - p1.X), (int) (p2.Y - p1.Y));
        }
    }
}