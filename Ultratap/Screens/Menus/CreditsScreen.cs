using System;
using System.Collections.Generic;
using GameStateManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Content;

namespace Ultratap
{
    /// <summary>
    /// Provides a basic base screen for menus on Windows Phone leveraging the Button class.
    /// </summary>
    class CreditsScreen : GameScreen
    {

        ContentManager content;

        InputAction menuCancel;



        /// <summary>
        /// Creates the PhoneMenuScreen with a particular title.
        /// </summary>
        /// <param name="title">The title of the screen</param>
        public CreditsScreen()
        {

            TransitionOnTime = TimeSpan.FromSeconds(0.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);

            // Create the menuCancel action
            menuCancel = new InputAction(new Buttons[] { Buttons.Back }, null, true);

            // We need tap gestures to hit the buttons
            EnabledGestures = GestureType.Tap;
        }

        public override void Activate(bool instancePreserved)
        {



            base.Activate(instancePreserved);
        }


        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {


            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        /// <summary>
        /// An overrideable method called whenever the menuCancel action is triggered
        /// </summary>
        void OnCancel()
        {

            LoadingScreen.Load(ScreenManager, false, null, new BackgroundScreen(), new PhoneMainMenuScreen());
        }

        public override void HandleInput(GameTime gameTime, InputState input)
        {
            // Test for the menuCancel action
            PlayerIndex player;
            if (menuCancel.Evaluate(input, ControllingPlayer, out player))
            {
                OnCancel();
            }



            base.HandleInput(gameTime, input);
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice graphics = ScreenManager.GraphicsDevice;
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            SpriteFont font = ScreenManager.Font;


            spriteBatch.Begin();

            // Make the menu slide into place during transitions, using a
            // power curve to make things look more interesting (this makes
            // the movement slow down as it nears the end).
            float transitionOffset = (float)Math.Pow(TransitionPosition, 2);

            // Draw the menu title centered on the screen
            Vector2 titlePosition = new Vector2(graphics.Viewport.Width / 2, 80);
            Vector2 titleOrigin = font.MeasureString("CREDITS") / 2;
            Color titleColor = new Color(255, 255, 255) * TransitionAlpha;
            float titleScale = 1.5f;

            titlePosition.Y -= transitionOffset * 100;

            spriteBatch.DrawString(font, "CREDITS", titlePosition, titleColor, 0,
                                   titleOrigin, titleScale, SpriteEffects.None, 0);

            spriteBatch.DrawString(font, "Please give feedback or report any bugs to: ", new Vector2(100, 150), Color.White);
            spriteBatch.DrawString(font, "thomas.blank@hotmail.de", new Vector2(200, 200), Color.Tan);

            spriteBatch.DrawString(font, "Special thanks to: ", new Vector2(100, 300), Color.White);
            spriteBatch.DrawString(font, "www.SoundJay.com for the music", new Vector2(150, 350), Color.Tan);


            spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}
