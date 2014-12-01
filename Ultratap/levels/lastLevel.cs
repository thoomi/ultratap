using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Ultratap
{
    class lastLevel : GameplayScreen
    {

        // Constructor ( Sets the initial player position, the initial aim position, and the levels texture)
        public lastLevel()
            : base(new Vector2(50, 230), new Vector2(700, 220), "level/basic_level", 12)
        {
        }



        protected override void customActivate(ContentManager content)
        {

            base.customActivate(content);
        }


        protected override void CustomLevelUpdate(GameTime gameTime)
        {

            


            UpdateCollisions();

            base.CustomLevelUpdate(gameTime);
        }


        private void UpdateCollisions()
        {
        }


        protected override void customLevelDraw(SpriteBatch spritebatch)
        {
           
            SpriteFont textFont = gameFont;

            spritebatch.DrawString(gameFont, "Congratulation! You reached the highest level!", new Vector2(50, 100), Color.Black);
            spritebatch.DrawString(gameFont, "I am sure there will be more soon!", new Vector2(50, 140), Color.Black);
            spritebatch.DrawString(gameFont, "Please give me feedback to ", new Vector2(50, 300), Color.Black);
            spritebatch.DrawString(gameFont, "improve the game :)", new Vector2(50, 340), Color.Black);
            spritebatch.DrawString(gameFont, "Greetings, Thomi ", new Vector2(50, 380), Color.DarkOliveGreen);

            base.customLevelDraw(spritebatch);
        }

    }
}
