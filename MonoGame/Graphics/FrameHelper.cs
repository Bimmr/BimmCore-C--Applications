using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BimmCore.MonoGame.Graphics
{
    /// <summary>
    /// Framed Texture - Easy Animations
    /// </summary>
    public class FrameHelper
    {
        public int SelectedFrame;
        public List<Rectangle> Frames;
        public int Width, Height;

        private bool loop;
        private int startX, startY;
        private int row, col;

        /// <summary>
        /// Create a framed texture
        /// </summary>
        /// <param name="startX"></param>
        /// <param name="startY"></param>
        /// <param name="size"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        public FrameHelper(int startX, int startY, int width, int height, int row, int col)
        {
            SelectedFrame = -1;
            this.startX = startX;
            this.startY = startY;
            Width = width;
            Height = height;
            this.row = row;
            this.col = col;

            createFrames();
        }

        public FrameHelper(int startX, int startY, int width, int height, int row, int col, bool loop)
            : this(startX, startY, width, height, row, col)
        {
            this.loop = loop;
        }

        /// <summary>
        /// Create the frames
        /// </summary>
        private void createFrames()
        {
            Frames = new List<Rectangle>();
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    int x = j * Width + startX;
                    int y = i * Height + startY;

                    Frames.Add(new Rectangle(x, y, Width, Height));
                }
            }
        }

        /// <summary>
        /// Get the next frame
        /// </summary>
        /// <returns>Rectangle</returns>
        public Rectangle getNextFrame()
        {
            if (SelectedFrame + 1 < Frames.Count)
                SelectedFrame++;
            else if (loop)
                SelectedFrame = 0;


            return SelectedFrame <= Frames.Count && SelectedFrame >= 1 ? Frames[SelectedFrame] : Rectangle.Empty;
        }

        /// <summary>Rectangle
        /// Get the current frame
        /// </summary>
        /// <returns>Rectangle</returns>
        public Rectangle getCurrentFrame()
        {
            return SelectedFrame >= 0 && SelectedFrame < Frames.Count ? Frames[SelectedFrame] : Rectangle.Empty;
        }
    }
}