using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Ultratap
{
    class level12 : GameplayScreen
    {

        // Constructor ( Sets the initial player position, the initial aim position, and the levels texture)
        public level12()
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

            base.customLevelDraw(spritebatch);
        }

    }
}
