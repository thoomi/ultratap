#region File Description
//-----------------------------------------------------------------------------
// GameplayScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using GameStateManagement;
using System.Diagnostics;
#endregion

namespace Ultratap
{
    /// <summary>
    /// This is where all the interesting stuff happens
    /// This class is the base for all levels
    /// </summary>
    class GameplayScreen : GameScreen
    {
        #region Fields

        ContentManager content;
        public SpriteFont gameFont;

        float pauseAlpha;
        int currentLevel;
        Vector2 currentTabPosition;

        Stopwatch stopwatch;
        TimeSpan timespan;

        InputAction pauseAction; 

        // Create the main Parts of the Game: (Player, Aim, Level)
        public player Player;
        public Color[] playerTextureData;
        Vector2 customPlayerPosition;

        aim Aim;
        Color[] aimTextureData;
        Vector2 customAimPosition;

        level Level;
        Color[] levelTextureData;
        String customlevelfilePath;

        #endregion // Fields

        #region Initialization


        /// <summary>
        /// Constructor.
        /// </summary>
        public GameplayScreen(Vector2 playerPos, Vector2 aimPos, String levelfilePath, int levelID)
        {
            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);

            pauseAction = new InputAction(
                new Buttons[] { Buttons.Start, Buttons.Back },
                new Keys[] { Keys.Escape },
                true);

            
            // These variabels a indiviually set by each level
            customPlayerPosition = playerPos;
            customAimPosition = aimPos;
            customlevelfilePath = levelfilePath;
            currentLevel = levelID;

            currentTabPosition = new Vector2(playerPos.X, playerPos.Y);

            // Only enable the Tap gesture
            EnabledGestures = GestureType.Tap;

        }


        /// <summary>
        /// Load graphics content for the game and do initializing
        /// </summary>
        public override void Activate(bool instancePreserved)
        {
            if (!instancePreserved)
            {
                if (content == null)
                    content = new ContentManager(ScreenManager.Game.Services, "Content");

                stopwatch = new Stopwatch();

                // Load font
                gameFont = content.Load<SpriteFont>("fonts/gameFont");

                // Create the main game components
                Player = new player(content, customPlayerPosition);
                Aim = new aim(content, customAimPosition);
                Level = new level(content, customlevelfilePath);

                // Load custom level stuff
                customActivate(content);

                // Extract Pixel collision data
                playerTextureData = new Color[Player.Width * Player.Height];
                Player.Texture.GetData(playerTextureData);

                levelTextureData = new Color[Level.Width * Level.Height];
                Level.Texture.GetData(levelTextureData);

                aimTextureData = new Color[Aim.Width * Aim.Height];
                Aim.Texture.GetData(aimTextureData);

                // once the load has finished, we use ResetElapsedTime to tell the game's
                // timing mechanism that we have just finished a very long frame, and that
                // it should not try to catch up.
                ScreenManager.Game.ResetElapsedTime();
            }
        }

        // Override this method to load custom game content
        protected virtual void customActivate(ContentManager content) {   }


        public override void Deactivate()
        {


            base.Deactivate();
        }

        /// <summary>
        /// Unload graphics content used by the game.
        /// </summary>
        public override void Unload()
        {
            content.Unload();
        }

        #endregion // Initialization end

        #region Update and Draw


        /// <summary>
        /// Updates the state of the game. This method checks the GameScreen.IsActive
        /// property, so the game will stop updating when the pause menu is active,
        /// or if you tab away to a different application.
        /// </summary>
        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, false);

            // Gradually fade in or out depending on whether we are covered by the pause screen.
            if (coveredByOtherScreen)
                pauseAlpha = Math.Min(pauseAlpha + 1f / 32, 1);
            else
                pauseAlpha = Math.Max(pauseAlpha - 1f / 32, 0);

            if (IsActive)
            {
                // Update elapsed play time
                timespan = stopwatch.Elapsed;

                UpdatePlayer(gameTime);
                UpdateCollisions(gameTime);
                CustomLevelUpdate(gameTime);
            }
        }


        #region UpdateHelpers

        // Override this function to implement some other cool game components
        protected virtual void CustomLevelUpdate(GameTime gameTime) { }


        // Update method for the player
        Vector2 notNormalizedDirection;

        void UpdatePlayer(GameTime gameTime)
        {
            // Calculate the direction vector in which the player should move
            notNormalizedDirection = currentTabPosition - Player.Position;

            // Calculate the distance
            if (notNormalizedDirection.Length() > 1)
            {
                Player.Position += Player.movingDirection * (float)gameTime.ElapsedGameTime.TotalSeconds * notNormalizedDirection.Length() * 7;
            }
            
            // Make sure that the player does not go out of bounds
            Player.Position.X = MathHelper.Clamp(Player.Position.X, 0, ScreenManager.GraphicsDevice.Viewport.Width - Player.Width);
            Player.Position.Y = MathHelper.Clamp(Player.Position.Y, 41, ScreenManager.GraphicsDevice.Viewport.Height - 65);
        }

        
        // Update Collisions (this elapsedTime is needed for a smooth aim collision)
        double elapsedTime = 0;

        void UpdateCollisions(GameTime gameTime)
        { 
            // The players an aims rectangle
            Rectangle rectPlayer = new Rectangle((int)Player.Position.X,(int)Player.Position.Y, Player.Width, Player.Height);
            Rectangle rectAim = new Rectangle((int)Aim.Position.X, (int)Aim.Position.Y, Aim.Width, Aim.Height);

            if (IntersectPixels(rectPlayer, playerTextureData, rectAim, aimTextureData))
            {
                elapsedTime += gameTime.ElapsedGameTime.TotalSeconds;

                if (elapsedTime >= 0.1)
                { 
                    Player.hasWon = true;

                    // Stop Stopwatch if the Player hits the aim
                    stopwatch.Stop();
                }
            }
            
            // Check for Pixel Collision with the level texture (per Pixel collison)
            Rectangle rectLevel = new Rectangle(0, 0, Level.Width, Level.Height);

            if(IntersectPixels(rectPlayer, playerTextureData, rectLevel, levelTextureData))
            {
                Player.isActive = false;
                Player.Health -= 1;
            }
        }

        /// <summary>
        /// Lets the game respond to player input. Unlike the Update method,
        /// this will only be called when the gameplay screen is active.
        /// </summary>
        public override void HandleInput(GameTime gameTime, InputState input)
        {
            if (input == null)
                throw new ArgumentNullException("input");

            // Look up inputs for the active player profile.
            int playerIndex = (int)ControllingPlayer.Value;

            PlayerIndex player;
            if (pauseAction.Evaluate(input, ControllingPlayer, out player))
            {
                ScreenManager.AddScreen(new PhonePauseScreen(), ControllingPlayer);
            }


            // The player lost a life or the game
            if (Player.isActive == false)
            {
                if (Player.Health > 0)
                {
                    // The player lost a life
                    ScreenManager.AddScreen(new LostLifeMenuScreen(), ControllingPlayer);
                    Player.Reset();
                    currentTabPosition = Player.initPosition;

                    // Reset the stopwatch
                    stopwatch.Reset();
                }
                if (Player.Health == 0)
                {
                    // The player is gameover
                    ScreenManager.AddScreen(new GameOverMenuScreen(), ControllingPlayer);
                    Player.Initialize();
                    currentTabPosition = Player.initPosition;
                }
            }

            // The Player has won the Game
            if(Player.hasWon == true)
            {
                // Stop the stopwatch and give the elapsed time to the winnerScreen
                stopwatch.Stop();

                timespan = stopwatch.Elapsed;

                ScreenManager.AddScreen(new winnerMenuScreen(Player.countTaps, Player.Health, currentLevel, timespan), ControllingPlayer);
            }
            

            if (Player.isActive == true)
            {

                // Loops through the inputs in queue 
                // In this case it only looks for taps
                foreach (GestureSample gesture in input.Gestures)
                {
                    if (gesture.GestureType == GestureType.Tap)
                    {

                        // Start stopwatch only if its the first tap
                        if (Player.countTaps == 0)
                        {
                            stopwatch.Start();
                        }


                        // Set the new tap position and calculate direction
                        currentTabPosition = gesture.Position;

                        // Subtract the players half-width and half-height ( for precisness reasons )
                        currentTabPosition.X -= Player.Width/2;
                        currentTabPosition.Y -= Player.Height/2;

                        //Recalculate the currentTap if it is out of range
                        if (currentTabPosition.X < 0)
                        {
                            currentTabPosition.X = 0;
                        } 
                        if (currentTabPosition.X > 775)
                        {
                            currentTabPosition.X = 775;
                        }
                        if (currentTabPosition.Y < 41)
                        {
                            currentTabPosition.Y = 41;
                        }
                        if (currentTabPosition.Y > 415)
                        {
                            currentTabPosition.Y = 415;
                        }

                        // Calculate the moving direction and normalize the vector to length 1
                        Player.movingDirection = currentTabPosition - Player.Position;
                        Player.movingDirection.Normalize();

                        // Count Tap
                        Player.countTaps += 1;
                    }
                }
            }
        }

        

        #endregion // UpdateHelpers

        /// <summary>
        /// Draws the gameplay screen.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            // This game has a grey background
            ScreenManager.GraphicsDevice.Clear(ClearOptions.Target, Color.White, 0, 0);

            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

            spriteBatch.Begin();
                
                Level.Draw(spriteBatch);

                // Draw Custom level components 
                customLevelDraw(spriteBatch);
                
                Aim.Draw(spriteBatch);
                Player.Draw(spriteBatch);

                
                // Draw the play time
                // Format and display the TimeSpan value.
                string elapsedTime = String.Format("{0:00}:{1:00}.{2:00}", timespan.Minutes, timespan.Seconds, timespan.Milliseconds / 10);

                spriteBatch.DrawString(gameFont, elapsedTime, new Vector2(ScreenManager.GraphicsDevice.Viewport.Width - 460, ScreenManager.GraphicsDevice.Viewport.Y), Color.White);
                
                // Draw the player health
                spriteBatch.DrawString(gameFont, "Lifes: " + Player.Health, new Vector2(ScreenManager.GraphicsDevice.Viewport.Width - 140, ScreenManager.GraphicsDevice.Viewport.Y), Color.White);
                //// Draw the used taps
                //spriteBatch.DrawString(gameFont, "Tabs: " + Player.countTaps, new Vector2(ScreenManager.GraphicsDevice.Viewport.Width - 140, ScreenManager.GraphicsDevice.Viewport.Y), Color.White);
                
            spriteBatch.End();

            // If the game is transitioning on or off, fade it out to black.
            if (TransitionPosition > 0 || pauseAlpha > 0)
            {
                float alpha = MathHelper.Lerp(1f - TransitionAlpha, 1f, pauseAlpha / 2);

                ScreenManager.FadeBackBufferToBlack(alpha);
            }
        }

        // Override this function to draw the other cool custom level components
        protected virtual void customLevelDraw(SpriteBatch spritebatch) { }

        #endregion // Update and Draw

        #region Collision Methods
        /// <summary>
        /// Determines if there is overlap of the non-transparent pixels
        /// between two sprites.
        /// </summary>
        /// <param name="rectangleA">Bounding rectangle of the first sprite</param>
        /// <param name="dataA">Pixel data of the first sprite</param>
        /// <param name="rectangleB">Bouding rectangle of the second sprite</param>
        /// <param name="dataB">Pixel data of the second sprite</param>
        /// <returns>True if non-transparent pixels overlap; false otherwise</returns>
        public static bool IntersectPixels(Rectangle rectangleA, Color[] dataA,
                                    Rectangle rectangleB, Color[] dataB)
        {
            // Find the bounds of the rectangle intersection
            int top = Math.Max(rectangleA.Top, rectangleB.Top);
            int bottom = Math.Min(rectangleA.Bottom, rectangleB.Bottom);
            int left = Math.Max(rectangleA.Left, rectangleB.Left);
            int right = Math.Min(rectangleA.Right, rectangleB.Right);

            // Check every point within the intersection bounds
            for (int y = top; y < bottom; y++)
            {
                for (int x = left; x < right; x++)
                {
                    // Get the color of both pixels at this point
                    Color colorA = dataA[(x - rectangleA.Left) +
                                         (y - rectangleA.Top) * rectangleA.Width];
                    Color colorB = dataB[(x - rectangleB.Left) +
                                         (y - rectangleB.Top) * rectangleB.Width];

                    // If both pixels are not completely transparent,
                    if (colorA.A != 0 && colorB.A != 0)
                    {
                        // then an intersection has been found
                        return true;
                    }
                }
            }

            // No intersection found
            return false;
        }


        /// <summary>
        /// Determines if there is overlap of the non-transparent pixels between two
        /// sprites.
        /// </summary>
        /// <param name="transformA">World transform of the first sprite.</param>
        /// <param name="widthA">Width of the first sprite's texture.</param>
        /// <param name="heightA">Height of the first sprite's texture.</param>
        /// <param name="dataA">Pixel color data of the first sprite.</param>
        /// <param name="transformB">World transform of the second sprite.</param>
        /// <param name="widthB">Width of the second sprite's texture.</param>
        /// <param name="heightB">Height of the second sprite's texture.</param>
        /// <param name="dataB">Pixel color data of the second sprite.</param>
        /// <returns>True if non-transparent pixels overlap; false otherwise</returns>
        public static bool IntersectPixels(
                            Matrix transformA, int widthA, int heightA, Color[] dataA,
                            Matrix transformB, int widthB, int heightB, Color[] dataB)
        {
            // Calculate a matrix which transforms from A's local space into
            // world space and then into B's local space
            Matrix transformAToB = transformA * Matrix.Invert(transformB);

            // When a point moves in A's local space, it moves in B's local space with a
            // fixed direction and distance proportional to the movement in A.
            // This algorithm steps through A one pixel at a time along A's X and Y axes
            // Calculate the analogous steps in B:
            Vector2 stepX = Vector2.TransformNormal(Vector2.UnitX, transformAToB);
            Vector2 stepY = Vector2.TransformNormal(Vector2.UnitY, transformAToB);

            // Calculate the top left corner of A in B's local space
            // This variable will be reused to keep track of the start of each row
            Vector2 yPosInB = Vector2.Transform(Vector2.Zero, transformAToB);

            // For each row of pixels in A
            for (int yA = 0; yA < heightA; yA++)
            {
                // Start at the beginning of the row
                Vector2 posInB = yPosInB;

                // For each pixel in this row
                for (int xA = 0; xA < widthA; xA++)
                {
                    // Round to the nearest pixel
                    int xB = (int)Math.Round(posInB.X);
                    int yB = (int)Math.Round(posInB.Y);

                    // If the pixel lies within the bounds of B
                    if (0 <= xB && xB < widthB &&
                        0 <= yB && yB < heightB)
                    {
                        // Get the colors of the overlapping pixels
                        Color colorA = dataA[xA + yA * widthA];
                        Color colorB = dataB[xB + yB * widthB];

                        // If both pixels are not completely transparent,
                        if (colorA.A != 0 && colorB.A != 0)
                        {
                            // then an intersection has been found
                            return true;
                        }
                    }

                    // Move to the next pixel in the row
                    posInB += stepX;
                }

                // Move to the next row
                yPosInB += stepY;
            }

            // No intersection found
            return false;
        }


        /// <summary>
        /// Calculates an axis aligned rectangle which fully contains an arbitrarily
        /// transformed axis aligned rectangle.
        /// </summary>
        /// <param name="rectangle">Original bounding rectangle.</param>
        /// <param name="transform">World transform of the rectangle.</param>
        /// <returns>A new rectangle which contains the trasnformed rectangle.</returns>
        public static Rectangle CalculateBoundingRectangle(Rectangle rectangle,
                                                           Matrix transform)
        {
            // Get all four corners in local space
            Vector2 leftTop = new Vector2(rectangle.Left, rectangle.Top);
            Vector2 rightTop = new Vector2(rectangle.Right, rectangle.Top);
            Vector2 leftBottom = new Vector2(rectangle.Left, rectangle.Bottom);
            Vector2 rightBottom = new Vector2(rectangle.Right, rectangle.Bottom);

            // Transform all four corners into work space
            Vector2.Transform(ref leftTop, ref transform, out leftTop);
            Vector2.Transform(ref rightTop, ref transform, out rightTop);
            Vector2.Transform(ref leftBottom, ref transform, out leftBottom);
            Vector2.Transform(ref rightBottom, ref transform, out rightBottom);

            // Find the minimum and maximum extents of the rectangle in world space
            Vector2 min = Vector2.Min(Vector2.Min(leftTop, rightTop),
                                      Vector2.Min(leftBottom, rightBottom));
            Vector2 max = Vector2.Max(Vector2.Max(leftTop, rightTop),
                                      Vector2.Max(leftBottom, rightBottom));

            // Return that as a rectangle
            return new Rectangle((int)min.X, (int)min.Y,
                                 (int)(max.X - min.X), (int)(max.Y - min.Y));
        }
        #endregion // Collision methods
    }
}
