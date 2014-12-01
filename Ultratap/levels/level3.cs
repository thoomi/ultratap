using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Ultratap
{
    class level3 : GameplayScreen
    {

        // Constructor ( Sets the initial player position, the initial aim position, and the levels texture)
        public level3()
            : base(new Vector2(70, 230), new Vector2(700, 220), "level/basic_level", 3)
        {
        }

        Texture2D obstacle;
        Vector2 obstacle1Position;
        Vector2 obstacle2Position;
        Vector2 obstacle3Position;

        int obstacleDirection = 1;

        Color[] obstacleData;



        protected override void customActivate(ContentManager content)
        {
            obstacle = content.Load<Texture2D>("level/rect100x250");

            obstacleData = new Color[obstacle.Width * obstacle.Height];
            obstacle.GetData(obstacleData);

            // Initial position
            obstacle1Position = new Vector2(200, 40);
            obstacle2Position = new Vector2(350, 190);
            obstacle3Position = new Vector2(500, 40);

            base.customActivate(content);
        }


        protected override void CustomLevelUpdate(GameTime gameTime)
        {

            UpdateCollisions();

            if (obstacle1Position.Y <= 39 || obstacle1Position.Y >= 190)
            {
                obstacleDirection *= -1;
            }

            obstacle1Position.Y += 150.0f * (float)gameTime.ElapsedGameTime.TotalSeconds * obstacleDirection;
            obstacle2Position.Y -= 150.0f * (float)gameTime.ElapsedGameTime.TotalSeconds * obstacleDirection;
            obstacle3Position.Y += 150.0f * (float)gameTime.ElapsedGameTime.TotalSeconds * obstacleDirection;

            base.CustomLevelUpdate(gameTime);
        }


        private void UpdateCollisions()
        {
            // Get the bounding rectangles
            Rectangle rectPlayer = new Rectangle((int)Player.Position.X, (int)Player.Position.Y, Player.Width, Player.Height);
            Rectangle rectObstacle1 = new Rectangle((int)obstacle1Position.X, (int)obstacle1Position.Y, obstacle.Width, obstacle.Height);
            Rectangle rectObstacle2 = new Rectangle((int)obstacle2Position.X, (int)obstacle2Position.Y, obstacle.Width, obstacle.Height);
            Rectangle rectObstacle3 = new Rectangle((int)obstacle3Position.X, (int)obstacle3Position.Y, obstacle.Width, obstacle.Height);

            if (IntersectPixels(rectObstacle1, obstacleData, rectPlayer, playerTextureData))
            {
                Player.isActive = false;
                Player.Health -= 1;
            }
            if (IntersectPixels(rectObstacle2, obstacleData, rectPlayer, playerTextureData))
            {
                Player.isActive = false;
                Player.Health -= 1;
            }
            if (IntersectPixels(rectObstacle3, obstacleData, rectPlayer, playerTextureData))
            {
                Player.isActive = false;
                Player.Health -= 1;
            }

        }


        protected override void customLevelDraw(SpriteBatch spritebatch)
        {

            spritebatch.Draw(obstacle, obstacle1Position, Color.White);
            spritebatch.Draw(obstacle, obstacle2Position, Color.White);
            spritebatch.Draw(obstacle, obstacle3Position, Color.White);

            base.customLevelDraw(spritebatch);
        }

    }
}
