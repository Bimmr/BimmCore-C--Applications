using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using BimmCore.MonoGame.Graphics;

namespace BimmCore.MonoGame.Components
{
    public abstract class Screen : DrawableGameComponent
    {
        public KeyboardState KeyboardState;
        public KeyboardState LastKeyBoardState;
        public MouseState MouseState;
        public MouseState LastMouseState;

        public List<GameComponent> Components;

        public ScreenHandler screenHandler;
        public SpriteBatch SpriteBatch => MonoHelper.SpriteBatch;

        /// <summary>
        /// Constructor for the screen
        /// </summary>
        /// <param name="towerDefense">The Game</param>
        protected Screen() : base(MonoHelper.Game)
        {
            Components = new List<GameComponent>();

            hide();
        }

        /// <summary>
        /// Hide the screen
        /// </summary>
        public void hide()
        {
            Visible = false;
            Enabled = false;
            onHide();
        }

        /// <summary>
        /// Show the screen
        /// </summary>
        public void show()
        {
            Visible = true;
            Enabled = true;
            onShow();
        }

        /// <summary>
        /// Draw all components the screen has
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            foreach (GameComponent item in Components)
            {
                DrawableGameComponent drawableGameComponent = item as DrawableGameComponent;
                if (drawableGameComponent != null)
                    if (drawableGameComponent.Visible)
                        drawableGameComponent.Draw(gameTime);
            }

            base.Draw(gameTime);
        }

        /// <summary>
        /// Update all components the screen has
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            if (MonoHelper.Game.IsActive)
            {
                KeyboardState = Keyboard.GetState();
                MouseState = Mouse.GetState();
                var comps = new List<GameComponent>(Components);
                foreach (GameComponent item in comps)
                    if (item.Enabled)
                        item.Update(gameTime);

                base.Update(gameTime);

                LastMouseState = MouseState;
                LastKeyBoardState = KeyboardState;
            }
        }

        /// <summary>
        /// Called when the show method gets called
        /// </summary>
        public abstract void onShow();

        /// <summary>
        /// Called when the hide method gets called
        /// </summary>
        public abstract void onHide();


        /// <summary>
        /// Show a notification
        /// </summary>
        /// <param name="title"></param>
        /// <param name="text"></param>
        /// <param name="action"></param>
        /// <param name="sprite"></param>
        /// <returns></returns>
        public Notification showNotification(string title, string[] text, Action action, Sprite sprite = null)
        {
            return new Notification(this, title, text, action, sprite, null, null, null);
        }
    }
}