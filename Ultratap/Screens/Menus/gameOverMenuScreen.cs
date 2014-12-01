using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;

namespace Ultratap
{
    /// <summary>
    /// The You Lost a Life Screen
    /// </summary>
    class GameOverMenuScreen : PhoneMenuScreen
    {
        public GameOverMenuScreen()
            : base("GAME OVER")
        {
            // Create the  Try Again Button

            Button tryagainButton = new Button("Try Again");
            tryagainButton.Tapped += tryagainButton_Tapped;
            MenuButtons.Add(tryagainButton);

            Button highscoreButton = new Button("Highscore");
            highscoreButton.Tapped += new EventHandler<EventArgs>(highscoreButton_Tapped);
            MenuButtons.Add(highscoreButton);

        }

        protected override void OnCancel()
        {
            LoadingScreen.Load(ScreenManager, false, PlayerIndex.One, new BackgroundScreen(), new level1());
            base.OnCancel();
        }

        void highscoreButton_Tapped(object sender, EventArgs e)
        {
            LoadingScreen.Load(ScreenManager, false, PlayerIndex.One, new BackgroundScreen(), new HighscoreScreen());
        }

        /// <summary>
        /// The "Lost Life" button handler just calls the OnCancel method so that 
        /// pressing the "Lost Life" button is the same as pressing the hardware back button.
        /// </summary>
        void tryagainButton_Tapped(object sender, EventArgs e)
        {
            LoadingScreen.Load(ScreenManager, false, PlayerIndex.One, new BackgroundScreen(), new level1());
        }

        savegame GameState;
        highscore Highscore;
        

        protected override void customMenuActivate(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            // Load GameState
            GameState = new savegame();
            GameState.LoadGameState();

            // Load Highscore
            Highscore = new highscore();
            
            if (Highscore.isInHighscore(GameState.ts, GameState.LevelID))
            {
                Highscore.addHighscoreEntry(GameState.LevelID, GameState.ts);
            }

            GameState.ResetGameState();


            base.customMenuActivate(content);
        }
    }
}

