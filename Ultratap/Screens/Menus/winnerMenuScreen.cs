#region File Description
//-----------------------------------------------------------------------------
// winnerMenuScreen.cs
// This Screen shows the Score after winning a level
//-----------------------------------------------------------------------------
#endregion


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
    class winnerMenuScreen : GameScreen
    {

        ContentManager content;

        TextureButton nextLevelButton;

        int levelID;
        int levelscore;
        int health;
        int leveltaps;
        TimeSpan ts;
        String newTimeString;
        String totalTimeString;

        savegame SaveGame;
        highscore Highscore;


        InputAction menuCancel;

        /// <summary>
        /// Creates the winnerMenuScreen
        /// </summary>
        public winnerMenuScreen(int usedTaps, int lifes, int levelID, TimeSpan timespan)
        {

            this.levelID = levelID;

            health = lifes;
            leveltaps = usedTaps;

            ts = new TimeSpan(timespan.Days, timespan.Hours, timespan.Minutes, timespan.Seconds, timespan.Milliseconds);

            newTimeString = String.Format("{0:00}:{1:00}.{2:00}", ts.Minutes, ts.Seconds, ts.Milliseconds / 10);

            //// Calculate score
            //levelscore = 2000  - 100 * leveltaps;
                        
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


                nextLevelButton = new TextureButton(content.Load<Texture2D>("nextlevel"), new Vector2(480, 210));
                nextLevelButton.Tapped += new EventHandler<EventArgs>(nextLevelButton_Tapped);
            
                // Load the SaveGame and store the new Data
                SaveGame = new savegame();
                SaveGame.LoadGameState();

                SaveGame.Score += levelscore;
                SaveGame.LevelID = this.levelID;
                SaveGame.Taps += leveltaps;
                SaveGame.ts += ts;

                SaveGame.SaveGameState();

                totalTimeString = String.Format("{0:00}:{1:00}.{2:00}", SaveGame.ts.Minutes, SaveGame.ts.Seconds, SaveGame.ts.Milliseconds / 10);
                

            base.Activate(instancePreserved);
        }
        
        // The buttons tap event handler
        void nextLevelButton_Tapped(object sender, EventArgs e)
        {
            OnCancel();
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {

            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        /// <summary>
        /// The Method which is call if the user hits the next button
        /// </summary>
        public void OnCancel() 
        {
            // Switch through the levels

            switch (levelID)
            { 
                case 1:
                    LoadingScreen.Load(ScreenManager, false, PlayerIndex.One, new BackgroundScreen(), new level2());
                    break;
                case 2:
                    LoadingScreen.Load(ScreenManager, false, PlayerIndex.One, new BackgroundScreen(), new level3());
                    break;
                case 3:
                    LoadingScreen.Load(ScreenManager, false, PlayerIndex.One, new BackgroundScreen(), new level4());
                    break;
                case 4:
                    LoadingScreen.Load(ScreenManager, false, PlayerIndex.One, new BackgroundScreen(), new level5());
                    break;
                case 5:
                    LoadingScreen.Load(ScreenManager, false, PlayerIndex.One, new BackgroundScreen(), new level6());
                    break;
                case 6:
                    LoadingScreen.Load(ScreenManager, false, PlayerIndex.One, new BackgroundScreen(), new level7());
                    break;
                case 7:
                    LoadingScreen.Load(ScreenManager, false, PlayerIndex.One, new BackgroundScreen(), new level8());
                    break;
                case 8:
                    LoadingScreen.Load(ScreenManager, false, PlayerIndex.One, new BackgroundScreen(), new level9());
                    break;
                case 9:
                    LoadingScreen.Load(ScreenManager, false, PlayerIndex.One, new BackgroundScreen(), new level10());
                    break;
                case 10:
                    LoadingScreen.Load(ScreenManager, false, PlayerIndex.One, new BackgroundScreen(), new level11());
                    break;
                //case 11:
                //    LoadingScreen.Load(ScreenManager, false, PlayerIndex.One, new BackgroundScreen(), new level12());
                //    break;
                case 11:
                    LoadingScreen.Load(ScreenManager, false, PlayerIndex.One, new BackgroundScreen(), new lastLevel());
                    break;

                default:
                    Highscore = new highscore();
                    if (Highscore.isInHighscore(SaveGame.ts, SaveGame.LevelID))
                    {
                        Highscore.addHighscoreEntry(SaveGame.LevelID, SaveGame.ts);
                    }

                    SaveGame.ResetGameState();

                    LoadingScreen.Load(ScreenManager, false, null, new BackgroundScreen(), new PhoneMainMenuScreen());
                    break;
            }
        }

        public override void HandleInput(GameTime gameTime, InputState input)
        {
            // Test for the menuCancel action
            PlayerIndex player;
            if (menuCancel.Evaluate(input, ControllingPlayer, out player))
            {
                OnCancel();
            }

            // Read in our gestures
            foreach (GestureSample gesture in input.Gestures)
            {
                // If we have a tap
                if (gesture.GestureType == GestureType.Tap)
                {

                    nextLevelButton.HandleTap(gesture.Position);
                    
                }
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

            Vector2 titlePosition = new Vector2(graphics.Viewport.Width / 2, 80);
            Vector2 titleOrigin = font.MeasureString("You completed Level " + levelID + " !") / 2;
            Color titleColor = new Color(255, 255, 255);
            float titleScale = 1.5f;

            titlePosition.Y -= transitionOffset * 100;

            spriteBatch.DrawString(font, "You completed Level " + levelID + " !", titlePosition, titleColor, 0,
                                   titleOrigin, titleScale, SpriteEffects.None, 0);

            //spriteBatch.DrawString(font, "Level Completion:     2000" , new Vector2(200, 150), Color.Green);
            //spriteBatch.DrawString(font, "Taps:                  -" + leveltaps + " x 100", new Vector2(200, 200), Color.Red);

            spriteBatch.DrawString(font, "Level Time: " + newTimeString, new Vector2(200, 200), Color.Azure);
            spriteBatch.DrawString(font, "Total Time: " + totalTimeString, new Vector2(200, 250), Color.SteelBlue);

            nextLevelButton.Draw(this);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
