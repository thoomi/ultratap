using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;


namespace Ultratap
{
    class player
    {

        // Image representing the Player
        public Texture2D Texture;

        // Position of the Player relative to the upper left side of the screen
        public Vector2 initPosition; // Start position
        public Vector2 Position;

        // Moving direction
        public Vector2 movingDirection;
        public bool isMoving;

        // State of the player
        public bool isActive;
        public bool hasWon;

        // Amount of hit points that player has
        public int Health;

        // The count of the taps
        public int countTaps;

        // Constructor
        public player(ContentManager content, Vector2 position)
        {
            Texture = content.Load<Texture2D>("gameElements/playerball");
            this.Position = this.initPosition = position;
            // Activate the player
            this.isActive = true;
            this.isMoving = false;
            this.hasWon = false;
            this.Health = 2;
            this.countTaps = 0;
        }

        // Get the width of the player
        public int Width
        {
            get { return Texture.Width; }
        }

        // Get the height of the player
        public int Height
        {
            get { return Texture.Height; }
        }


        public void Initialize()
        {
            isActive = true;
            isMoving = false;
            Position = initPosition;
            movingDirection.X = 0;
            movingDirection.Y = 0;
            Health = 2;
            countTaps = 0;
        }

        public void Reset()
        {
            isActive = true;
            isMoving = false;
            Position = initPosition;
            movingDirection.X = 0;
            movingDirection.Y = 0;
            countTaps = 0;
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(Texture, Position, Color.White);
        }


    }
}
