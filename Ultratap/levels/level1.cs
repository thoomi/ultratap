using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Ultratap
{
    class level1 : GameplayScreen
    {

        // Constructor ( Sets the initial player position, the initial aim position, and the levels texture)
        public level1()
            : base(new Vector2(70, 230), new Vector2(720, 220), "level/basic_level", 1)
        { 
        }

        Texture2D obstacle;
        Vector2 obstaclePosition;
        int obstacleDirection = 1;

        Color[] obstacleData;

        protected override void customActivate(ContentManager content)
        {

            obstacle = content.Load<Texture2D>("level/rect100x250");

            obstacleData = new Color[obstacle.Width * obstacle.Height];
            obstacle.GetData(obstacleData);

            // Initial position
            obstaclePosition = new Vector2(350, 50);

            base.customActivate(content);
        }


        protected override void CustomLevelUpdate(GameTime gameTime)
        {

            UpdateCollisions();


            if (obstaclePosition.Y <= 39 || obstaclePosition.Y >= 190)
            {
                obstacleDirection *= -1;
            }

            obstaclePosition.Y += 150.0f * (float)gameTime.ElapsedGameTime.TotalSeconds * obstacleDirection;

            base.CustomLevelUpdate(gameTime);
        }

        private void UpdateCollisions()
        {
            // Get the bounding rectangles
            Rectangle rectPlayer = new Rectangle((int)Player.Position.X, (int)Player.Position.Y, Player.Width, Player.Height);
            Rectangle rectObstacle = new Rectangle((int)obstaclePosition.X, (int)obstaclePosition.Y, obstacle.Width, obstacle.Height);

            if (IntersectPixels(rectObstacle, obstacleData, rectPlayer, playerTextureData))
            {
                Player.isActive = false;
                Player.Health -= 1;
            }
        }


        protected override void customLevelDraw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(obstacle, obstaclePosition, Color.White);

            base.customLevelDraw(spritebatch);
        }

    }
}
