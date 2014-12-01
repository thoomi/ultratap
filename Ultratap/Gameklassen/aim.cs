using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Ultratap
{
    class aim
    {
        // Texture representing the Aim
        public Texture2D Texture;

        // The Position
        public Vector2 Position;

        // Constructor
        public aim(ContentManager content, Vector2 position)
        {
            Texture = content.Load<Texture2D>("gameElements/aim");
            this.Position = position;
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
