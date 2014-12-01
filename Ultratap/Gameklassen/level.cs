using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Ultratap
{
    class level
    {
        // Image representing the Level
        public Texture2D Texture;

        // Constructor
        public level(ContentManager content, String filepath)
        {
            Texture = content.Load<Texture2D>(filepath);
        }

        // Get the width of the Level
        public int Width
        {
            get { return Texture.Width; }
        }

        // Get the height of the Level
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
            spriteBatch.Draw(Texture, new Vector2(0, 0), Color.White);
        }
    }
}
