using System;
using BimmCore.MonoGame.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BimmCore.MonoGame.Components
{
    /// <summary>
    /// Button, Don't forget to call the .Draw and .Update
    /// </summary>
    public class Button
    {
        private Action<Button, MouseState> onClick;
        private Action<Button, MouseState> onNotClick;
        private Action<Button, MouseState> onHover;
        private Action<Button, MouseState> onNotHover;

        private Action<GameTime> beforeUpdate, afterUpdate;
        private Action<GameTime, SpriteBatch> beforeDraw, afterDraw;

        private bool centerText = true;
        private string[] text;

        private Rectangle size;
        private Sprite sprite;
        private Color boxColor;

        private SpriteFont spriteFont;
        private Color textColor;

        /// <summary>
        /// Pointless default constructor
        /// </summary>
        /// <param name="size"></param>
        public Button(Rectangle size)
        {
            this.size = size;
        }

        /// <summary>
        /// Set the sprite
        /// </summary>
        /// <param name="sprite"></param>
        /// <returns></returns>
        public Button setSprite(Sprite sprite)
        {
            this.sprite = sprite;
            return this;
        }

        /// <summary>
        /// Set BoxColor
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public Button setBoxColor(Color color)
        {
            boxColor = color;
            return this;
        }

        /// <summary>
        /// Set SpriteFont
        /// </summary>
        /// <param name="spriteFont"></param>
        /// <returns></returns>
        public Button setSpriteFont(SpriteFont spriteFont)
        {
            this.spriteFont = spriteFont;
            return this;
        }

        /// <summary>
        /// Set Text
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public Button setText(string[] text)
        {
            this.text = text;
            return this;
        }

        /// <summary>
        /// Set Text
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public Button setText(string text)
        {
            this.text = new[] {text};
            return this;
        }

        /// <summary>
        /// Set TextColor
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public Button setTextColor(Color color)
        {
            textColor = color;
            return this;
        }

        /// <summary>
        /// Set OnClick Event
        /// </summary>
        /// <param name="clickEvent"></param>
        /// <returns></returns>
        public Button setClickEvent(Action<Button, MouseState> clickEvent)
        {
            onClick = clickEvent;
            return this;
        }

        /// <summary>
        /// Set OnNotClick Event
        /// </summary>
        /// <param name="notClickEvent"></param>
        /// <returns></returns>
        public Button setNotClickEvent(Action<Button, MouseState> notClickEvent)
        {
            onNotClick = notClickEvent;
            return this;
        }

        /// <summary>
        /// Set onHover Event
        /// </summary>
        /// <param name="hoverEvent"></param>
        /// <returns></returns>
        public Button setHoverEvent(Action<Button, MouseState> hoverEvent)
        {
            onHover = hoverEvent;
            return this;
        }

        /// <summary>
        /// Set the before draw action
        /// </summary>
        /// <param name="beforeEvent"></param>
        /// <returns></returns>
        public Button setBeforeDraw(Action<GameTime, SpriteBatch> beforeEvent)
        {
            beforeDraw = beforeEvent;
            return this;
        }

        /// <summary>
        /// Set the AfterDraw Action
        /// </summary>
        /// <param name="afterEvent"></param>
        /// <returns></returns>
        public Button setAftereDraw(Action<GameTime, SpriteBatch> afterEvent)
        {
            afterDraw = afterEvent;
            return this;
        }

        /// <summary>
        /// Set the Before Update Action
        /// </summary>
        /// <param name="updateEvent"></param>
        /// <returns></returns>
        public Button setBeforeUpdate(Action<GameTime> updateEvent)
        {
            beforeUpdate = updateEvent;
            return this;
        }

        /// <summary>
        /// Set the After update action
        /// </summary>
        /// <param name="updateEvent"></param>
        /// <returns></returns>
        public Button setAfterUpdate(Action<GameTime> updateEvent)
        {
            afterUpdate = updateEvent;
            return this;
        }

        /// <summary>
        /// Set if the text is centered
        /// </summary>
        /// <param name="center"></param>
        /// <returns></returns>
        public Button setCenterText(bool center)
        {
            centerText = center;
            return this;
        }

        /// <summary>
        /// Get the AfterDraw event
        /// </summary>
        /// <returns></returns>
        public Action<GameTime, SpriteBatch> getAfterDraw()
        {
            return afterDraw;
        }

        /// <summary>
        /// Get the size
        /// </summary>
        /// <returns></returns>
        public Rectangle getSize()
        {
            return size;
        }

        /// <summary>
        /// Set onNotHover Event
        /// </summary>
        /// <param name="notHoverEvent"></param>
        /// <returns></returns>
        public Button setNotHoverEvent(Action<Button, MouseState> notHoverEvent)
        {
            onNotHover = notHoverEvent;
            return this;
        }


        /// <summary>
        /// Draw the Button
        /// </summary>
        /// <param name="gameTime"></param>
        public void Draw(GameTime gameTime)
        {
            SpriteBatch sp = MonoHelper.SpriteBatch;

            beforeDraw?.Invoke(gameTime, sp);
            if (sprite != null)
                sprite.draw(sp, new Vector2(size.X, size.Y));
            else
                Drawer.drawRectangle(size, boxColor);

            if (text != null)
            {
                for (int i = 0; i < text.Length; i++)
                {
                    string line = text[i];

                    int y = size.Y + i * (size.Height / text.Length);

                    sp.DrawString(spriteFont, line,
                        centerText
                            ? Utils.centerText(spriteFont, line, size)
                            : new Vector2(size.X + (sprite?.getWidth() ?? 0), y + (sprite?.getHeight() / 10 ?? 0)),
                        textColor);
                }
            }
            afterDraw?.Invoke(gameTime, sp);
        }


        /// <summary>
        /// Update the button
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
                beforeUpdate?.Invoke(gameTime);
                Point mousePos = Mouse.GetState().Position;
                if (size.Contains(mousePos))
                    onHover?.Invoke(this, Mouse.GetState());
                else
                    onNotHover?.Invoke(this, Mouse.GetState());
                if (size.Contains(mousePos) && Mouse.GetState().LeftButton == ButtonState.Pressed)
                    onClick?.Invoke(this, Mouse.GetState());
                else
                    onNotClick?.Invoke(this, Mouse.GetState());
                afterUpdate?.Invoke(gameTime);
            
        }
    }
}