using System;
using BimmCore.MonoGame.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BimmCore.MonoGame.Components
{
    public class Input
    {
        private KeyBoardListener keyListener;
        private bool editing;

        private Button submitButton;
        private Rectangle size;
        private Color boxColor = Color.White;

        private SpriteFont textFont;
        private string text;
        private Color textColor = Color.Black;

        private Color previewTextColor = Color.Gray;
        private string previewText;

        private Action<Input> onSubmit;
        private Action<Input, Keys> onKeyType;

        public Input(Rectangle size, bool useButton)
        {
            this.size = size;
            this.keyListener = new KeyBoardListener((k)=>
            {
                OnKeyDown(k);
                onKeyType?.Invoke(this, k);
            }, null);

            if (useButton)
            {
                submitButton = new Button(new Rectangle(size.Right, size.Y, 75, size.Height)).setText("Submit")
                    .setTextColor(Color.Black)
                    .setBoxColor(Color.Gray)
                    .setHoverEvent((b, s) => b.setBoxColor(Color.LightGray))
                    .setNotHoverEvent((b, s) => { b.setBoxColor(Color.Gray); });
                submitButton?.setClickEvent((b, s) => submit());
            }
        }

        /// <summary>
        /// Set the box color
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public Input setBoxColor(Color color)
        {
            boxColor = color;
            return this;
        }

        /// <summary>
        /// Set the text
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public Input setText(string text)
        {
            this.text = text;
            return this;
        }

        /// <summary>
        /// Set the preview Text(Grayed out text)
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public Input setPreviewText(string text)
        {
            previewText = text;
            return this;
        }

        /// <summary>
        /// Set the text color
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public Input setTextColor(Color color)
        {
            textColor = color;
            return this;
        }

        //Set the text font
        public Input setTextFont(SpriteFont spriteFont)
        {
            textFont = spriteFont;
            submitButton?.setSpriteFont(spriteFont);
            return this;
        }

        /// <summary>
        /// Set the submit action
        /// </summary>
        /// <param name="onSubmit"></param>
        /// <returns></returns>
        public Input setSubmitEvent(Action<Input> onSubmit)
        {
            this.onSubmit = onSubmit;
            return this;
        }

        /// <summary>
        /// Set the KeyTyped action
        /// </summary>
        /// <param name="onKey"></param>
        /// <returns></returns>
        public Input setKeyTypeEvent(Action<Input, Keys> onKey)
        {
            onKeyType = onKey;
            return this;
        }

        /// <summary>
        /// Draw the Input
        /// </summary>
        /// <param name="gameTime"></param>
        public void Draw(GameTime gameTime)
        {
            Drawer.drawRectangle(size, boxColor);

            if (editing)
            {
                Drawer.drawRectangle(size, Color.Black * .5f);
                int currentLetterPos = (int) (size.X + textFont.MeasureString(text).X);
                Drawer.drawRectangle(new Rectangle(currentLetterPos, size.Y + 5, 2, size.Height - 10), Color.Black);
            }
            if (text != null || text == "")
                MonoHelper.SpriteBatch.DrawString(textFont, text,
                    new Vector2(size.X, Utils.centerText(textFont, text, size).Y), textColor);
            else
                MonoHelper.SpriteBatch.DrawString(textFont, previewText,
                    new Vector2(size.X, Utils.centerText(textFont, previewText, size).Y), previewTextColor);

            if (submitButton != null)
            {
                Drawer.drawRectangle(
                    new Rectangle(submitButton.getSize().X - 1, submitButton.getSize().Y - 1,
                        submitButton.getSize().Width + 2, submitButton.getSize().Height + 2), Color.Black);
                submitButton.Draw(gameTime);
            }
        }

        /// <summary>
        /// Update the Input
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            submitButton?.Update(gameTime);

            if (size.Contains(Mouse.GetState().Position) && Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                editing = true;
            }
            else if (editing && (!size.Contains(Mouse.GetState().Position) &&
                                 Mouse.GetState().LeftButton == ButtonState.Pressed))
                editing = false;
            if(editing)
                keyListener.update();
           
        }

        /// <summary>
        /// On a Key press
        /// </summary>
        /// <param name="key"></param>
        private void OnKeyDown(Keys key)
        {
            if (key == Keys.Back && text.Length > 0)
                text = text.Substring(0, text.Length - 1);
            else if (key.ToString().Length == 1)
                text += key.ToString();
        }

        /// <summary>
        /// When the submit button is pressed
        /// </summary>
        public void submit()
        {
            onSubmit?.Invoke(this);
        }

        /// <summary>
        /// Gets the text
        /// </summary>
        /// <returns></returns>
        public string getText()
        {
            return text;
        }
    }
}