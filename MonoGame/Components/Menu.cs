using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using BimmCore.MonoGame.Graphics;

namespace BimmCore.MonoGame.Components
{
    public class Menu
    {
        public static Rectangle Size = new Rectangle(0, 0, 200, 50);
        public static int Spacing = 5;


        public Color ButtonColor = Color.Gray;
        public Color FontColor = Color.Black;

        public int Selected;
        public Vector2 Location;
        public List<Button> Buttons;
        
        public SpriteFont SpriteFont;


        public Menu(Vector2 location, SpriteFont font)
        {
            SpriteFont = font;
            Location = location;
            Buttons = new List<Button>();
        }

        public Menu(Vector2 location, Color buttonColor, Color fontColor, SpriteFont font)
            : this(location, font)
        {
            ButtonColor = buttonColor;
            FontColor = fontColor;
        }

        public void Update(GameTime gameTime)
        {
                foreach (Button btn in Buttons)
                    btn.Update(gameTime);
        }

        public void Draw(GameTime gameTime)
        {
            foreach (Button btn in Buttons)
                btn.Draw(gameTime);
        }

        public void addOption(string text, Sprite sprite, Action<Button, MouseState>onClick)
        {
            addOption(new[] {text}, sprite, onClick);
        }

        public void addOption(string[] text, Sprite sprite, Action<Button, MouseState> onClick)
        {
            int x = (int) Location.X;
            int y = (int) Location.Y + (Size.Height + Spacing) * Buttons.Count;

            Rectangle rec = new Rectangle(x, y, Size.Width, Size.Height);

            Button btn = new Button(rec)
                .setSprite(sprite)
                .setSpriteFont(SpriteFont)
                .setText(text)
                .setTextColor(FontColor)
                .setBoxColor(ButtonColor)
                .setClickEvent(onClick);

            if (sprite != null)
                btn.setCenterText(false);

            if (ButtonColor != Color.Transparent)
            {
                btn.setBeforeDraw((time, batch) =>
                {
                    Drawer.drawRectangle(new Rectangle(rec.X - 1, rec.Y - 1, rec.Width + 1, rec.Height + 1),
                        Color.Black);
                    Drawer.drawRectangle(rec, ButtonColor);
                });
            }
            Action<Button, MouseState> hover =
                (button, state) =>
                {
                    button.setAftereDraw((time, batch) =>
                    {
                        if (ButtonColor != Color.Transparent)
                            Drawer.drawRectangle(rec, Color.White * .4f);
                        Selected = Buttons.Count;
                    });
                };
            Action<Button, MouseState> notHover =
                (button, state) =>
                {
                    if (button.getAfterDraw() != null) button.setAftereDraw(null);
                    if (Selected == Buttons.Count)
                        Selected = -1;
                };

            btn.setHoverEvent(hover);
            btn.setNotHoverEvent(notHover);

            Buttons.Add(btn);
        }

        public void addOption(string text, Action<Button, MouseState> onClick)
        {
            addOption(text, null, onClick);
        }
    }
}