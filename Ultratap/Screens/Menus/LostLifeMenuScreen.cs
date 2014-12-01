#region File Description
//-----------------------------------------------------------------------------
// LostLifeScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion
using System;

namespace Ultratap
{
    /// <summary>
    /// The You Lost a Life Screen
    /// </summary>
    class LostLifeMenuScreen : PhoneMenuScreen
    {
        public LostLifeMenuScreen()
            : base("You Lost a Life!")
        {
            // Create the  "Lost Life Button"

            Button tryagainButton = new Button("Try Again!");
            tryagainButton.Tapped += tryagainButton_Tapped;
            MenuButtons.Add(tryagainButton);

        }

        /// <summary>
        /// The "Lost Life" button handler just calls the OnCancel method so that 
        /// pressing the "Lost Life" button is the same as pressing the hardware back button.
        /// </summary>
        void tryagainButton_Tapped(object sender, EventArgs e)
        {
            OnCancel();
        }


        protected override void OnCancel()
        {
            ExitScreen();
            base.OnCancel();
        }
    }
}
