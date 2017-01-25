//TODO: Redo file

using System;
using System.ComponentModel.Design;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using BimmCore.MonoGame.Graphics;

namespace BimmCore.MonoGame.Components
{
    /// <summary>
    /// Notifications
    /// </summary>
    public class Notification : DrawableGameComponent
    {
        private static int width = 390;
        private static int height = 150;

        private static Sprite _spriteImage;
        private static Rectangle _rectangle;

        private int x;
        private int y;
        private Sprite _backgroundSprite;
        private Button _button;
        private Screen _screen;

        private Color _titleColor = Color.White;
        private string _title;
        private SpriteFont _titleFont;
        private Color _textColor = Color.Black;
        private string[] _text;
        private SpriteFont _textFont;

        private Action _onButtonClick;


        /// <summary>
        /// Create a notification with only text
        /// </summary>
        /// <param name="towerDefense"></param>
        /// <param name="screen"></param>
        /// <param name="title"></param>
        /// <param name="text"></param>
        /// <param name="onBtn"></param>
        public Notification(Screen screen, string title, string[] text, Action onBtn, Sprite backgroundSprite, Sprite buttonSprite, SpriteFont titleFont, SpriteFont textFont)
            : base(MonoHelper.Game)
        {
            //Set everything up
            x = (int)MonoHelper.Middle.X - width / 2;
            y = 175;
            _rectangle = new Rectangle(x, y, width, height);
            _backgroundSprite = backgroundSprite;
            _titleFont = titleFont;
            _textFont = textFont;

            _onButtonClick = onBtn;

            //Create a rectangle for the button
            Rectangle buttonSize = new Rectangle(x + width / 2 - buttonSprite.Source.Width / 2,
                y + height - 10 - buttonSprite.Source.Height, buttonSprite.Source.Width,
                buttonSprite.Source.Height);


            //Create an action that will draw a gray rectangle over the button
            Action<Button, MouseState> draw =
                (button, state) => { Drawer.drawRectangle(buttonSize, Color.White * .4f); };

            _button = new Button(buttonSize)
                .setSprite(buttonSprite)
                .setSpriteFont(textFont).setText("Ok").setTextColor(Color.White)
                .setClickEvent(
                    //On Click Perform following code
                    (b, m) =>
                    {
                        if (_screen.LastMouseState.LeftButton != ButtonState.Pressed)
                        {
                            hide();
                            _onButtonClick?.Invoke();
                        }
                    })
                    .setHoverEvent(
                    //On Hover Perform the following code
                    (b, s) =>
                    {
                        //Set the Draw action to draw the rectangle
                        b.setAftereDraw((b2, s2) => { draw(b, s); });
                    }).setNotClickEvent((b, s) =>
                    {
                        if (b.getAfterDraw() != null)
                            b.setAftereDraw(null);
                    }
            );

            _screen = screen;
            _title = title;
            _text = text;
            show();
        }

        /// <summary>
        /// Create a notification with an image
        /// </summary>
        /// <param name="towerDefense"></param>
        /// <param name="screen"></param>
        /// <param name="title"></param>
        /// <param name="text"></param>
        /// <param name="onBtn"></param>
        /// <param name="sprite"></param>
        public Notification(Screen screen, string title, string[] text, Action onBtn,
            Sprite sprite, Sprite backgroundSprite, Sprite buttonSprite, SpriteFont titleFont, SpriteFont textFont)
            : this(screen, title, text, onBtn, backgroundSprite, buttonSprite, titleFont, textFont)
        {
            _spriteImage = sprite;
        }

        /// <summary>
        /// Show the notification
        /// </summary>
        /// <returns></returns>
        public Notification show()
        {
            _screen.Components.Add(this);
            return this;
        }

        /// <summary>
        /// Hide the notification
        /// </summary>
        /// <returns></returns>
        public Notification hide()
        {
            _screen.Components.Remove(this);
            return this;
        }

        /// <summary>
        /// Update the button in the notification
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            _button.Update(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// Draw the notification
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = MonoHelper.SpriteBatch;

            //Draw background and button
            _backgroundSprite.draw(spriteBatch, new Vector2(x, y), 0.99f);
            _button.Draw(gameTime);

            //Draw the image/text
            _spriteImage?.draw(spriteBatch, new Vector2(x + 1, y + 50));

            Vector2 titleLoc = Utils.centerText(_titleFont, _title, new Rectangle(x, y, width, 30));
            //spriteBatch.DrawString(_titleFont, _title, new Vector2(titleLoc.X + 1, titleLoc.Y + 1), Color.Black);
            spriteBatch.DrawString(_titleFont, _title, new Vector2(titleLoc.X - 1, titleLoc.Y + 1), Color.Black);
            spriteBatch.DrawString(_titleFont, _title, titleLoc, _titleColor);

            for (int i = 0; i < _text.Length; i++)
            {
                Vector2 loc = new Vector2(x + 10 + (_spriteImage != null ? _spriteImage.Source.Width : 0),
                    y + 35 + i * 20);
                spriteBatch.DrawString(_textFont, _text[i], loc, _textColor);
            }


            base.Draw(gameTime);
        }
    }
}