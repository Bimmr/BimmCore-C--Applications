namespace BimmCore.Misc
{
    public enum Direction
    {
        Left,
        Right,
        Up,
        Down
    }

    public class Directions
    {
        /// <summary>
        /// Get the opposite of the direction given
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        public static Direction getOpposite(Direction dir)
        {
            switch (dir)
            {
                case Direction.Up:
                    return Direction.Down;
                case Direction.Down:
                    return Direction.Up;
                case Direction.Left:
                    return Direction.Right;
                case Direction.Right:
                    return Direction.Left;
                default:
                    return Direction.Up;
            }
        }

        /// <summary>
        /// Check if the direction is vertical
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        public static bool isVertical(Direction dir)
        {
            return dir == Direction.Up || dir == Direction.Down;
        }

        /// <summary>
        /// Check if the direction is horizontal
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        public static bool isHorizontal(Direction dir)
        {
            return dir == Direction.Down || dir == Direction.Up;
        }
    }
}