using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Ultratap
{
    class level4 : GameplayScreen
    {

        // Constructor ( Sets the initial player position, the initial aim position, and the levels texture)
        public level4()
            : base(new Vector2(90, 150), new Vector2(650, 300), "level/level4", 4)
        {
        }

        Texture2D raute;
        Color[] rauteData;
        
        Vector2 raute1Position;
        Vector2 raute2Position;
        Vector2 raute3Position;

        Vector2 raute4Position;
        Vector2 raute5Position;
        Vector2 raute6Position;


        protected override void customActivate(ContentManager content)
        {
            raute = content.Load<Texture2D>("level/raute");
            rauteData = new Color[raute.Width * raute.Height];
            raute.GetData(rauteData);

            // Initial positions
            // Horizontal rautes
            raute1Position = new Vector2(800, 50);
            raute2Position = new Vector2(800, 225);
            raute3Position = new Vector2(800, 405);

            // Vertical rautes
            raute4Position = new Vector2(195, 0);
            raute5Position = new Vector2(390, 0);
            raute6Position = new Vector2(580, 0);

            base.customActivate(content);
        }

        bool flag = true;

        protected override void CustomLevelUpdate(GameTime gameTime)
        {

            UpdateCollisions();


            if (raute1Position.X > 0 && flag == true)
            {
                raute1Position.X -= 350.0f * (float)gameTime.ElapsedGameTime.TotalSeconds;
                raute2Position.X -= 350.0f * (float)gameTime.ElapsedGameTime.TotalSeconds;
                raute3Position.X -= 350.0f * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (raute4Position.Y < 480 && flag == false)
            {
                raute4Position.Y += 350.0f * (float)gameTime.ElapsedGameTime.TotalSeconds;
                raute5Position.Y += 350.0f * (float)gameTime.ElapsedGameTime.TotalSeconds;
                raute6Position.Y += 350.0f * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (raute1Position.X < 0)
            {
                flag = false;
                raute1Position.X = 800;
                raute2Position.X = 800;
                raute3Position.X = 800;
            }
            if (raute4Position.Y > 480)
            {
                flag = true;
                raute4Position.Y = 0;
                raute5Position.Y = 0;
                raute6Position.Y = 0;
            }

            


            base.CustomLevelUpdate(gameTime);
        }


        private void UpdateCollisions()
        {
            // Get the bounding rectangles
            Rectangle rectPlayer = new Rectangle((int)Player.Position.X, (int)Player.Position.Y, Player.Width, Player.Height);
            Rectangle rectRaute1 = new Rectangle((int)raute1Position.X, (int)raute1Position.Y, raute.Width, raute.Height);
            Rectangle rectRaute2 = new Rectangle((int)raute2Position.X, (int)raute2Position.Y, raute.Width, raute.Height);
            Rectangle rectRaute3 = new Rectangle((int)raute3Position.X, (int)raute3Position.Y, raute.Width, raute.Height);
            Rectangle rectRaute4 = new Rectangle((int)raute4Position.X, (int)raute4Position.Y, raute.Width, raute.Height);
            Rectangle rectRaute5 = new Rectangle((int)raute5Position.X, (int)raute5Position.Y, raute.Width, raute.Height);
            Rectangle rectRaute6 = new Rectangle((int)raute6Position.X, (int)raute6Position.Y, raute.Width, raute.Height);


            if (IntersectPixels(rectRaute1, rauteData, rectPlayer, playerTextureData))
            {
                Player.isActive = false;
                Player.Health -= 1;
            }
            if (IntersectPixels(rectRaute2, rauteData, rectPlayer, playerTextureData))
            {
                Player.isActive = false;
                Player.Health -= 1;
            }
            if (IntersectPixels(rectRaute3, rauteData, rectPlayer, playerTextureData))
            {
                Player.isActive = false;
                Player.Health -= 1;
            }
            if (IntersectPixels(rectRaute4, rauteData, rectPlayer, playerTextureData))
            {
                Player.isActive = false;
                Player.Health -= 1;
            }
            if (IntersectPixels(rectRaute5, rauteData, rectPlayer, playerTextureData))
            {
                Player.isActive = false;
                Player.Health -= 1;
            }
            if (IntersectPixels(rectRaute6, rauteData, rectPlayer, playerTextureData))
            {
                Player.isActive = false;
                Player.Health -= 1;
            }

        }


        protected override void customLevelDraw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(raute, raute1Position, Color.White);
            spritebatch.Draw(raute, raute2Position, Color.White);
            spritebatch.Draw(raute, raute3Position, Color.White);
            spritebatch.Draw(raute, raute4Position, Color.White);
            spritebatch.Draw(raute, raute5Position, Color.White);
            spritebatch.Draw(raute, raute6Position, Color.White);

            base.customLevelDraw(spritebatch);
        }

    }
}

