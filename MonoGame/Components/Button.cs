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
        /// Sprite Construtor
        /// </summary>
        /// <param name="size"></param>
        /// <param name="sprite"></param>
        /// <param name="boxColor"></param>
        public Button(Rectangle size, Sprite sprite, Color boxColor)
            : this(size)
        {
            _boxColor = boxColor;
            _sprite = sprite;
        }


        /// <summary>
        /// Text Constructor
        /// </summary>
        /// <param name="size"></param>
        /// <param name="spriteFont"></param>
        /// <param name="text"></param>
        /// <param name="textColor"></param>
        /// <param name="boxColor"></param>
        public Button(Rectangle size, SpriteFont spriteFont, string[] text, Color textColor, Color boxColor)
            : this(size)
        {
            Text = text;
            _spriteFont = spriteFont;
            _textColor = textColor;
            _boxColor = boxColor;
        }

        /// <summary>
        /// Text Constructor
        /// </summary>
        /// <param name="size"></param>
        /// <param name="spriteFont"></param>
        /// <param name="text"></param>
        /// <param name="textColor"></param>
        /// <param name="boxColor"></param>
        public Button(Rectangle size, SpriteFont spriteFont, string text, Color textColor, Color boxColor)
            : this( size, spriteFont, new []{text}, textColor, boxColor)
        {
        }


        /// <summary>
        /// Sprite and Text Constructor
        /// </summary>
        /// <param name="size"></param>
        /// <param name="sprite"></param>
        /// <param name="spriteFont"></param>
        /// <param name="text"></param>
        /// <param name="textColor"></param>
        /// <param name="boxColor"></param>
        public Button(Rectangle size, Sprite sprite, SpriteFont spriteFont, string[] text, Color textColor, Color boxColor)
            : this( size, spriteFont, text, textColor, boxColor)
        {
            _sprite = sprite;
        }


        /// <summary>
        /// Text and OnClick Constructor
        /// </summary>
        /// <param name="size"></param>
        /// <param name="spriteFont"></param>
        /// <param name="text"></param>
        /// <param name="textColor"></param>
        /// <param name="boxColor"></param>
        /// <param name="onClick"></param>
        public Button(Rectangle size, SpriteFont spriteFont, string[] text, Color textColor, Color boxColor,
            Action<Button, MouseState> onClick)
            : this( size, spriteFont, text, textColor, boxColor)
        {
            OnClick = onClick;
        }

        /// <summary>
        /// Text and OnClick Constructor
        /// </summary>
        /// <param name="size"></param>
        /// <param name="spriteFont"></param>
        /// <param name="text"></param>
        /// <param name="textColor"></param>
        /// <param name="boxColor"></param>
        /// <param name="onClick"></param>
        public Button( Rectangle size, SpriteFont spriteFont, string text, Color textColor, Color boxColor,
            Action<Button, MouseState> onClick)
            : this( size, spriteFont, new[]{text}, textColor, boxColor, onClick)
        {
        }


        /// <summary>
        /// Sprite and OnClick Constructor
        /// </summary>
        /// <param name="size"></param>
        /// <param name="sprite"></param>
        /// <param name="onClick"></param>
        public Button(Rectangle size, Sprite sprite, Action<Button, MouseState> onClick)
            : this( size, sprite)
        {
            OnClick = onClick;
        }


        /// <summary>
        /// Text, OnClick, and OnHover Constructor
        /// </summary>
        /// <param name="size"></param>
        /// <param name="spriteFont"></param>
        /// <param name="text"></param>
        /// <param name="color"></param>
        /// <param name="onClick"></param>
        /// <param name="onHover"></param>
        public Button( Rectangle size, SpriteFont spriteFont, string[] text, Color color,
            Action<Button, MouseState> onClick,
            Action<Button, MouseState> onHover)
            : this( size, spriteFont, text, color)
        {
            OnClick = onClick;
            OnHover = onHover;
        }
        /// <summary>
        /// Text, OnClick, and OnHover Constructor
        /// </summary>
        /// <param name="size"></param>
        /// <param name="spriteFont"></param>
        /// <param name="text"></param>
        /// <param name="color"></param>
        /// <param name="onClick"></param>
        /// <param name="onHover"></param>
        public Button(Rectangle size, SpriteFont spriteFont, string text, Color color,
            Action<Button, MouseState> onClick,
            Action<Button, MouseState> onHover)
            : this( size, spriteFont, new[]{text}, color, onClick, onHover)
        {
        }

        /// <summary>
        /// Sprite, OnClick, and OnHover Constructor
        /// </summary>
        /// <param name="size"></param>
        /// <param name="sprite"></param>
        /// <param name="onClick"></param>
        /// <param name="onHover"></param>
        public Button(Rectangle size, Sprite sprite, Action<Button, MouseState> onClick,
            Action<Button, MouseState> onHover)
            : this( size, sprite)
        {
            OnClick = onClick;
            OnHover = onHover;
        }


        /// <summary>
        /// Sprite, Text, OnClick and OnHover Constructor
        /// </summary>
        /// <param name="size"></param>
        /// <param name="sprite"></param>
        /// <param name="spriteFont"></param>
        /// <param name="text"></param>
        /// <param name="color"></param>
        /// <param name="onClick"></param>
        /// <param name="onHover"></param>
        public Button(Rectangle size, Sprite sprite, SpriteFont spriteFont, string[] text, Color color,
            Action<Button, MouseState> onClick,
            Action<Button, MouseState> onHover)
            : this( size, spriteFont, text, color)
        {
            _sprite = sprite;
            OnClick = onClick;
            OnHover = onHover;
        }

        /// <summary>
        /// Sprite, Text, OnClick and OnHover Constructor
        /// </summary>
        /// <param name="size"></param>
        /// <param name="sprite"></param>
        /// <param name="spriteFont"></param>
        /// <param name="text"></param>
        /// <param name="color"></param>
        /// <param name="onClick"></param>
        /// <param name="onHover"></param>
        public Button( Rectangle size, Sprite sprite, SpriteFont spriteFont, string text, Color color,
            Action<Button, MouseState> onClick,
            Action<Button, MouseState> onHover)
            : this( size, sprite, spriteFont, new[]{text}, color, onClick, onHover)
        {
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