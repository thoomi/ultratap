#region File Description
//-----------------------------------------------------------------------------
// PhoneMainMenuScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

using System;
using GameStateManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;


namespace Ultratap
{
    class PhoneMainMenuScreen : PhoneMenuScreen
    {


        public PhoneMainMenuScreen()
            : base("Menu")
        {

            // Create a button to start the game
            Button playButton = new Button("PLAY");
            playButton.Tapped += playButton_Tapped;
            MenuButtons.Add(playButton);

            // Create a button to the highscore
            Button highscoreButton = new Button("Leaderboard");
            highscoreButton.Tapped += new EventHandler<EventArgs>(highscoreButton_Tapped);
            MenuButtons.Add(highscoreButton);

            // Create two buttons to toggle sound effects and music. This sample just shows one way
            // of making and using these buttons; it doesn't actually have sound effects or music
            BooleanButton musicButton = new BooleanButton("Music", true);
            musicButton.Tapped += musicButton_Tapped;
            MenuButtons.Add(musicButton);

            Button creditButton = new Button("Credits");
            creditButton.Tapped += new EventHandler<EventArgs>(creditButton_Tapped);
            MenuButtons.Add(creditButton);

        }

        void creditButton_Tapped(object sender, EventArgs e)
        {
            LoadingScreen.Load(ScreenManager, true, null, new BackgroundScreen(), new CreditsScreen());
        }

        void highscoreButton_Tapped(object sender, EventArgs e)
        {
            LoadingScreen.Load(ScreenManager, true, null, new BackgroundScreen() ,new HighscoreScreen());
        }

        protected override void customMenuActivate(ContentManager content)
        {
            
            base.customMenuActivate(content);
        }


        void playButton_Tapped(object sender, EventArgs e)
        {
            // When the "Play" button is tapped, we load the GameplayScreen
            LoadingScreen.Load(ScreenManager, true, PlayerIndex.One, new level1());
        }

        void musicButton_Tapped(object sender, EventArgs e)
        {
            // If the music is playing, stop it.
            if (ScreenManager.musicManager.IsGameMusicPlaying)
            {
                ScreenManager.musicManager.Stop();
            }
                
            // Otherwise start playing our song.
            else
            {
                ScreenManager.musicManager.Play(ScreenManager.gameMusic);
            }

        }

        protected override void OnCancel()
        {
            ScreenManager.Game.Exit();
            base.OnCancel();
        }


        
    }
}
