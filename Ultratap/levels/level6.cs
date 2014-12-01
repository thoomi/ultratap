using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Ultratap
{
    class level6 : GameplayScreen
    {

        // Constructor ( Sets the initial player position, the initial aim position, and the levels texture)
        public level6()
            : base(new Vector2(70, 230), new Vector2(710, 220), "level/basic_level", 6)
        {
        }

        #region Fields
        Texture2D obstacleUP;
        Texture2D obstacleDOWN;

        Color[] obsUpData;
        Color[] obsDownData;

        Vector2 obs1upPosition;
        Vector2 obs1downPosition;

        Vector2 obs2upPosition;
        Vector2 obs2downPosition;

        Vector2 obs3upPosition;
        Vector2 obs3downPosition;

        Vector2 obs4upPosition;
        Vector2 obs4downPosition;

        #endregion



        protected override void customActivate(ContentManager content)
        {
            obstacleUP = content.Load<Texture2D>("level/level5_obs_up");
            obstacleDOWN = content.Load<Texture2D>("level/level5_obs_down");

            obsUpData = new Color[obstacleUP.Width * obstacleUP.Height];
            obstacleUP.GetData(obsUpData);

            obsDownData = new Color[obstacleDOWN.Width * obstacleDOWN.Height];
            obstacleDOWN.GetData(obsDownData);

            // Initial Positions of the moving game parts
            obs1upPosition = new Vector2(150, 260);
            obs1downPosition = new Vector2(150, -80);

            obs2upPosition = new Vector2(270, 290);
            obs2downPosition = new Vector2(270, -110);
            
            obs3upPosition = new Vector2(430, 290);
            obs3downPosition = new Vector2(430, -110);

            obs4upPosition = new Vector2(550, 240);
            obs4downPosition = new Vector2(550, -60);


            base.customActivate(content);
        }


        int obs1direction = -1;
        int obs2direction = -1;
        int obs3direction = -1;
        int obs4direction = -1;

        protected override void CustomLevelUpdate(GameTime gameTime)
        {

            UpdateCollisions();
            
            // Move the obstacles
            if (obs1upPosition.Y <= 240 || obs1upPosition.Y >= 300)
            {
                obs1direction *= -1;
            }
            obs1upPosition.Y += 25.0f * (float)gameTime.ElapsedGameTime.TotalSeconds * obs1direction;
            obs1downPosition.Y -= 25.0f * (float)gameTime.ElapsedGameTime.TotalSeconds * obs1direction;


            if (obs2upPosition.Y <= 240 || obs2upPosition.Y >= 300)
            {
                obs2direction *= -1;
            }
            obs2upPosition.Y += 25.0f * (float)gameTime.ElapsedGameTime.TotalSeconds * obs2direction;
            obs2downPosition.Y -= 25.0f * (float)gameTime.ElapsedGameTime.TotalSeconds * obs2direction;


            if (obs3upPosition.Y <= 240 || obs3upPosition.Y >= 300)
            {
                obs3direction *= -1;
            }
            obs3upPosition.Y += 25.0f * (float)gameTime.ElapsedGameTime.TotalSeconds * obs3direction;
            obs3downPosition.Y -= 25.0f * (float)gameTime.ElapsedGameTime.TotalSeconds * obs3direction;


            if (obs4upPosition.Y <= 240 || obs4upPosition.Y >= 300)
            {
                obs4direction *= -1;
            }
            obs4upPosition.Y += 25.0f * (float)gameTime.ElapsedGameTime.TotalSeconds * obs4direction;
            obs4downPosition.Y -= 25.0f * (float)gameTime.ElapsedGameTime.TotalSeconds * obs4direction;

            base.CustomLevelUpdate(gameTime);
        }


        private void UpdateCollisions()
        {
            // Bounding rectangles
            Rectangle rectPlayer = new Rectangle((int)Player.Position.X, (int)Player.Position.Y, Player.Width, Player.Height);

            Rectangle rectObs1Down = new Rectangle((int)obs1downPosition.X, (int)obs1downPosition.Y, obstacleDOWN.Width, obstacleDOWN.Height);
            Rectangle rectObs1Up = new Rectangle((int)obs1upPosition.X, (int)obs1upPosition.Y, obstacleUP.Width, obstacleUP.Height);

            Rectangle rectObs2Down = new Rectangle((int)obs2downPosition.X, (int)obs2downPosition.Y, obstacleDOWN.Width, obstacleDOWN.Height);
            Rectangle rectObs2Up = new Rectangle((int)obs2upPosition.X, (int)obs2upPosition.Y, obstacleUP.Width, obstacleUP.Height);

            Rectangle rectObs3Down = new Rectangle((int)obs3downPosition.X, (int)obs3downPosition.Y, obstacleDOWN.Width, obstacleDOWN.Height);
            Rectangle rectObs3Up = new Rectangle((int)obs3upPosition.X, (int)obs3upPosition.Y, obstacleUP.Width, obstacleUP.Height);

            Rectangle rectObs4Down = new Rectangle((int)obs4downPosition.X, (int)obs4downPosition.Y, obstacleDOWN.Width, obstacleDOWN.Height);
            Rectangle rectObs4Up = new Rectangle((int)obs4upPosition.X, (int)obs4upPosition.Y, obstacleUP.Width, obstacleUP.Height);

            if (IntersectPixels(rectObs1Down, obsDownData, rectPlayer, playerTextureData))
            {
                Player.isActive = false;
                Player.Health -= 1;
            }
            if (IntersectPixels(rectObs1Up, obsUpData, rectPlayer, playerTextureData))
            {
                Player.isActive = false;
                Player.Health -= 1;
            }


            if (IntersectPixels(rectObs2Down, obsDownData, rectPlayer, playerTextureData))
            {
                Player.isActive = false;
                Player.Health -= 1;
            }
            if (IntersectPixels(rectObs2Up, obsUpData, rectPlayer, playerTextureData))
            {
                Player.isActive = false;
                Player.Health -= 1;
            } 
            
            
            if (IntersectPixels(rectObs3Down, obsDownData, rectPlayer, playerTextureData))
            {
                Player.isActive = false;
                Player.Health -= 1;
            }
            if (IntersectPixels(rectObs3Up, obsUpData, rectPlayer, playerTextureData))
            {
                Player.isActive = false;
                Player.Health -= 1;
            }


            if (IntersectPixels(rectObs4Down, obsDownData, rectPlayer, playerTextureData))
            {
                Player.isActive = false;
                Player.Health -= 1;
            }
            if (IntersectPixels(rectObs4Up, obsUpData, rectPlayer, playerTextureData))
            {
                Player.isActive = false;
                Player.Health -= 1;
            }




        }


        protected override void customLevelDraw(SpriteBatch spritebatch)
        {

            spritebatch.Draw(obstacleUP, obs1upPosition, Color.White);
            spritebatch.Draw(obstacleDOWN, obs1downPosition, Color.White);

            spritebatch.Draw(obstacleUP, obs2upPosition, Color.White);
            spritebatch.Draw(obstacleDOWN, obs2downPosition, Color.White);

            spritebatch.Draw(obstacleUP, obs3upPosition, Color.White);
            spritebatch.Draw(obstacleDOWN, obs3downPosition, Color.White);

            spritebatch.Draw(obstacleUP, obs4upPosition, Color.White);
            spritebatch.Draw(obstacleDOWN, obs4downPosition, Color.White);


            base.customLevelDraw(spritebatch);
        }

    }
}
