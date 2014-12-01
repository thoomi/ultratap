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
    class HighscoreScreen : GameScreen
    {

        ContentManager content;

        InputAction menuCancel;

        highscore Highscore;


        /// <summary>
        /// Creates the PhoneMenuScreen with a particular title.
        /// </summary>
        /// <param name="title">The title of the screen</param>
        public HighscoreScreen()
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
            if (content == null)
                content = new ContentManager(ScreenManager.Game.Services, "Content");


            
            // Load highscore
            Highscore = new highscore();
            


            base.Activate(instancePreserved);
        }


        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {


            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        /// <summary>
        /// An overrideable method called whenever the menuCancel action is triggered
        /// </summary>
        void OnCancel() {

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
            Vector2 titleOrigin = font.MeasureString("LEADERBOARD") / 2;
            Color titleColor = new Color(255, 255, 255) * TransitionAlpha;
            float titleScale = 1.5f;

            titlePosition.Y -= transitionOffset * 100;

            spriteBatch.DrawString(font, "LEADERBOARD", titlePosition, titleColor, 0,
                                   titleOrigin, titleScale, SpriteEffects.None, 0);


            int EntryPos = 1;
            Vector2 position = new Vector2(110, 200);


            foreach (highscoreEntry entry in Highscore.entries)
            {
                String timeString = String.Format("{0:00}:{1:00}.{2:00}", entry.ts.Minutes, entry.ts.Seconds, entry.ts.Milliseconds / 10);

                spriteBatch.DrawString(font, EntryPos + ". " + entry.name, position, Color.White);
                spriteBatch.DrawString(font, "reached Level ", new Vector2(300, position.Y), Color.White);
                spriteBatch.DrawString(font, " " + entry.level, new Vector2(480, position.Y), Color.Violet);
                spriteBatch.DrawString(font, "in ", new Vector2(520, position.Y), Color.White);
                spriteBatch.DrawString(font, timeString, new Vector2(560, position.Y), Color.SeaGreen);

                EntryPos += 1;
                position.Y += 30;
            }




            spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}
