using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BimmCore.Misc;

namespace BimmCore.MonoGame.Graphics
{

    public class ScrollingBackground
    {
        private readonly Sprite sprite;
        private readonly Direction direction;
        private readonly Vector2 speed;
        private Vector2 location, location2;

        public ScrollingBackground(Sprite sprite, Vector2 location, int speed, Direction direction)
        {
            this.sprite = sprite;
            this.location = location;
            this.direction = direction;
            switch (direction)
            {
                case Direction.Left:
                    this.speed = new Vector2(-speed, 0);
                    location2 = new Vector2(location.X + sprite.getWidth(), this.location.Y);
                    break;
                case Direction.Right:
                    this.speed = new Vector2(speed, 0);
                    location2 = new Vector2(location.X - sprite.getWidth(), this.location.Y);
                    break;
                case Direction.Up:
                    this.speed = new Vector2(0, -speed);
                    location2 = new Vector2(location.X, this.location.Y + sprite.getHeight());
                    break;
                case Direction.Down:
                    this.speed = new Vector2(0, speed);
                    location2 = new Vector2(location.X, this.location.Y - sprite.getHeight());
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }

        /// <summary>
        /// Move the background
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void move()
        {
            location += speed;
            location2 += speed;


            switch (direction)
            {
                case Direction.Left:
                    if (location.X <= -sprite.getWidth())
                        location.X = location2.X + sprite.getWidth();
                    if (location2.X <= -sprite.getWidth())
                        location2.X = location.X + sprite.getWidth();
                    break;
                case Direction.Right:
                    if (location.X >= sprite.getWidth())
                        location.X = location2.X - sprite.getWidth();
                    if (location2.X >= sprite.getWidth())
                        location2.X = location.X - sprite.getWidth();
                    break;
                case Direction.Up:
                    if (location.Y <= -sprite.getHeight())
                        location.Y = location2.Y + sprite.getHeight();
                    if (location2.Y <= -sprite.getHeight())
                        location2.Y = location.Y + sprite.getHeight();
                    break;
                case Direction.Down:
                    if (location.Y >= sprite.getHeight())
                        location.Y = location2.Y - sprite.getHeight();
                    if (location2.Y >= sprite.getHeight())
                        location2.Y = location.Y - sprite.getHeight();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Draw the background
        /// </summary>
        public void draw()
        {
            SpriteBatch spriteBatch = MonoHelper.SpriteBatch;
            sprite.draw(spriteBatch, location);
            sprite.draw(spriteBatch, location2);
        }
    }
}