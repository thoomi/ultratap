
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Ultratap
{
    class level5 : GameplayScreen
    {

        // Constructor ( Sets the initial player position, the initial aim position, and the levels texture)
        public level5()
            : base(new Vector2(70, 230), new Vector2(700, 220), "level/basic_level", 5)
        {
        }


        Texture2D obstacle1;
        Vector2 obstacle1Position;
        Vector2 obstacle2Position;
        int obstacleDirection = -1;

        Color[] obstacleData;

        protected override void customActivate(ContentManager content)
        {
            obstacle1 = content.Load<Texture2D>("level/level3_obstacle");

            obstacle1Position = new Vector2(150, 0);
            obstacle2Position = new Vector2(450, -200);

            obstacleData = new Color[obstacle1.Width * obstacle1.Height];
            obstacle1.GetData(obstacleData);


            base.customActivate(content);
        }

        protected override void CustomLevelUpdate(GameTime gameTime)
        {

            UpdateCollisions();

            // Moving obstacles
            if (obstacle1Position.Y <= -150 || obstacle1Position.Y >= 1)
            {
                obstacleDirection *= -1;
            }
            
            obstacle1Position.Y += 25.0f * (float)gameTime.ElapsedGameTime.TotalSeconds * obstacleDirection;

            obstacle2Position.Y -= 25.0f * (float)gameTime.ElapsedGameTime.TotalSeconds * obstacleDirection;


            base.CustomLevelUpdate(gameTime);
        }

        private void UpdateCollisions()
        {
            // Get the bounding rectangles
            Rectangle rectPlayer = new Rectangle((int)Player.Position.X,(int)Player.Position.Y, Player.Width, Player.Height);
            Rectangle rectObstacle1 = new Rectangle((int)obstacle1Position.X, (int)obstacle1Position.Y, obstacle1.Width, obstacle1.Height);
            Rectangle rectObstacle2 = new Rectangle((int)obstacle2Position.X, (int)obstacle2Position.Y, obstacle1.Width, obstacle1.Height);

            if(IntersectPixels(rectObstacle1, obstacleData, rectPlayer, playerTextureData))
            {
                Player.isActive = false;
                Player.Health -= 1;
            }
            if(IntersectPixels(rectObstacle2, obstacleData, rectPlayer, playerTextureData))
            {
                Player.isActive = false;
                Player.Health -= 1;
            }

        }


        protected override void customLevelDraw(SpriteBatch spritebatch)
        {

            spritebatch.Draw(obstacle1, obstacle1Position, Color.White);
            spritebatch.Draw(obstacle1, obstacle2Position, Color.White);

            base.customLevelDraw(spritebatch);
        }

    }
}
