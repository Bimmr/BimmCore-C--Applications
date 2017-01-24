using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using BimmCore.MonoGame.Graphics;

namespace BimmCore.MonoGame.Components
{
    /// <summary>
    /// Button, Don't forget to call the .Draw and .Update
    /// </summary>
    public class Button : DrawableGameComponent
    {

        public Action<Button, MouseState> OnClick;
        public Action<Button, MouseState> OnNotClick;
        public Action<Button, MouseState> OnHover;
        public Action<Button, MouseState> OnNotHover;

        public Action<GameTime> BeforeUpdate, AfterUpdate;
        public Action<GameTime, SpriteBatch> BeforeDraw, AfterDraw;

        public bool CenterText = true;
        public string[] Text;

        public Rectangle Size;
        private Sprite _sprite;
        private Color _boxColor;

        private SpriteFont _spriteFont;
        private Color _textColor;

        /// <summary>
        /// Pointless default constructor
        /// </summary>
        /// <param name="size"></param>
        public Button(Rectangle size)
            : base(MonoHelper.Game)
        {
            Size = size;
        }

        /// <summary>
        /// Set the sprite
        /// </summary>
        /// <param name="sprite"></param>
        /// <returns></returns>
        public Button setSprite(Sprite sprite)
        {
            this._sprite = sprite;
            return this;
        }

        /// <summary>
        /// Set BoxColor
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public Button setBoxColor(Color color)
        {
            this._boxColor = color;
            return this;
        }

        /// <summary>
        /// Set SpriteFont
        /// </summary>
        /// <param name="spriteFont"></param>
        /// <returns></returns>
        public Button setSpriteFont(SpriteFont spriteFont)
        {
            this._spriteFont = spriteFont;
            return this;
        }

        /// <summary>
        /// Set Text
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public Button setText(String[] text)
        {
            this.Text = text;
            return this;
        }
        /// <summary>
        /// Set Text
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public Button setText(String text)
        {
            this.Text = new[] { text };
            return this;
        }

        /// <summary>
        /// Set TextColor
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public Button setTextColor(Color color)
        {
            this._textColor = color;
            return this;
        }

        /// <summary>
        /// Set OnClick Event
        /// </summary>
        /// <param name="clickEvent"></param>
        /// <returns></returns>
        public Button setClickEvent(Action<Button, MouseState> clickEvent)
        {
            this.OnClick = clickEvent;
            return this;
        }

        /// <summary>
        /// Set OnNotClick Event
        /// </summary>
        /// <param name="notClickEvent"></param>
        /// <returns></returns>
        public Button setNotClickEvent(Action<Button, MouseState> notClickEvent)
        {
            this.OnNotClick = notClickEvent;
            return this;
        }
        /// <summary>
        /// Set onHover Event
        /// </summary>
        /// <param name="hoverEvent"></param>
        /// <returns></returns>
        public Button setHoverEvent(Action<Button, MouseState> hoverEvent)
        {
            this.OnHover = hoverEvent;
            return this;
        }
        /// <summary>
        /// Set onNotHover Event
        /// </summary>
        /// <param name="notHoverEvent"></param>
        /// <returns></returns>
        public Button setNotHoverEvent(Action<Button, MouseState> notHoverEvent)
        {
            this.OnNotHover = notHoverEvent;
            return this;
        }


        public override void Draw(GameTime gameTime)
        {

            SpriteBatch sp = MonoHelper.SpriteBatch;

            BeforeDraw?.Invoke(gameTime, sp);
            if (_sprite != null)
                _sprite.draw(sp, new Vector2(Size.X, Size.Y));
            else
                Drawer.drawRectangle(Size, _boxColor);

            if (Text != null)
            {
                for (int i = 0; i< Text.Length; i++)
                {
                    string line = Text[i];

                    int y =Size.Y+i * (Size.Height / Text.Length);

                    sp.DrawString(_spriteFont, line,
                        CenterText
                            ? Utils.centerText(_spriteFont, line, Size)
                            : new Vector2(Size.X + (_sprite?.getWidth() ?? 0), y +(_sprite?.getHeight()/10 ?? 0)), _textColor);
                }
            }
            AfterDraw?.Invoke(gameTime, sp);
            base.Draw(gameTime);
        }


        public override void Update(GameTime gameTime)
        {
            BeforeUpdate?.Invoke(gameTime);
            Point mousePos = Mouse.GetState().Position;
            if (Size.Contains(mousePos))
                OnHover?.Invoke(this, Mouse.GetState());
            else
                OnNotHover?.Invoke(this, Mouse.GetState());
            if (Size.Contains(mousePos) && Mouse.GetState().LeftButton == ButtonState.Pressed)
                OnClick?.Invoke(this, Mouse.GetState());
            else
                OnNotClick?.Invoke(this, Mouse.GetState());
            AfterUpdate?.Invoke(gameTime);

            base.Update(gameTime);
        }
    }
}