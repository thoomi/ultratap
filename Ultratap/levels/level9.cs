using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Ultratap
{
    class level9 : GameplayScreen
    {

        // Constructor ( Sets the initial player position, the initial aim position, and the levels texture )
        public level9()
            : base(new Vector2(115, 407), new Vector2(650, 380), "level/level9", 9)
        {
        }

        // The three center mover
        Texture2D center1;
        int centerDirection = 1;
        Color[] centerTextureData;

        Vector2 center1Position;
        Texture2D center2;
        Vector2 center2Position;
        Texture2D center3;
        Vector2 center3Position;

        // The left side
        Texture2D sideBlock;
        Color[] sideBlockTextureData;

        Vector2 leftBlock1Position;
        int leftBlock1Direction = 1;
        Vector2 leftBlock2Position;
        int leftBlock2Direction = 1;
        Vector2 leftBlock3Position;
        int leftBlock3Direction = 1;

        Vector2 rightBlock1Position;
        int rightBlock1Direction = 1;
        Vector2 rightBlock2Position;


        protected override void customActivate(ContentManager content)
        {

            center1 = center2 = center3 = content.Load<Texture2D>("level/rect100x250");
            sideBlock = content.Load<Texture2D>("level/rect250x100");

            center1Position = new Vector2(250, 41);
            center2Position = new Vector2(350, 94);
            center3Position = new Vector2(450, 41);

            leftBlock1Position = new Vector2(10, 100);
            leftBlock2Position = new Vector2(20, 200);
            leftBlock3Position = new Vector2(30, 300);

            rightBlock1Position = new Vector2(550, 150);
            rightBlock2Position = new Vector2(550, 250);


            centerTextureData = new Color[center1.Width * center1.Height];
            center1.GetData(centerTextureData);

            sideBlockTextureData = new Color[sideBlock.Width * sideBlock.Height];
            sideBlock.GetData(sideBlockTextureData);



            base.customActivate(content);
        }


        protected override void CustomLevelUpdate(GameTime gameTime)
        {

            if (center1Position.Y < 40 || center1Position.Y > 95)
            {
                centerDirection *= -1;
            }

            center1Position.Y += 30.0f * (float)gameTime.ElapsedGameTime.TotalSeconds * centerDirection;
            center2Position.Y -= 30.0f * (float)gameTime.ElapsedGameTime.TotalSeconds * centerDirection;
            center3Position.Y += 30.0f * (float)gameTime.ElapsedGameTime.TotalSeconds * centerDirection;


            if (leftBlock1Position.X > 40 || leftBlock1Position.X < 1)
            {
                leftBlock1Direction *= -1;
            }
            if (leftBlock2Position.X > 40 || leftBlock2Position.X < 1)
            {
                leftBlock2Direction *= -1;
            }
            if (leftBlock3Position.X > 40 || leftBlock3Position.X < 1)
            {
                leftBlock3Direction *= -1;
            }


            leftBlock1Position.X += 30.0f * (float)gameTime.ElapsedGameTime.TotalSeconds * leftBlock1Direction;
            leftBlock2Position.X += 30.0f * (float)gameTime.ElapsedGameTime.TotalSeconds * leftBlock2Direction;
            leftBlock3Position.X += 30.0f * (float)gameTime.ElapsedGameTime.TotalSeconds * leftBlock3Direction;


            if (rightBlock1Position.X > 549 || rightBlock1Position.X < 400)
            {
                rightBlock1Direction *= -1;
            }


            rightBlock1Position.X += 40.0f * (float)gameTime.ElapsedGameTime.TotalSeconds * rightBlock1Direction;
            rightBlock2Position.X -= 40.0f * (float)gameTime.ElapsedGameTime.TotalSeconds * rightBlock1Direction;



            UpdateCollisions();

            base.CustomLevelUpdate(gameTime);
        }


        private void UpdateCollisions()
        {
            // Get the bounding rectangles
            Rectangle rectPlayer = new Rectangle((int)Player.Position.X, (int)Player.Position.Y, Player.Width, Player.Height);

            Rectangle center1Rect = new Rectangle((int)center1Position.X, (int)center1Position.Y, center1.Width, center1.Height);
            Rectangle center2Rect = new Rectangle((int)center2Position.X, (int)center2Position.Y, center2.Width, center2.Height);
            Rectangle center3Rect = new Rectangle((int)center3Position.X, (int)center3Position.Y, center3.Width, center3.Height);

            //Bounding draw and collision rectangle of the three center rects
            Rectangle boundingCenterRect = new Rectangle(250, 40, 300, 200);

            if (boundingCenterRect.Intersects(rectPlayer))
            {

                if (IntersectPixels(center1Rect, centerTextureData, rectPlayer, playerTextureData))
                {
                    Player.isActive = false;
                    Player.Health -= 1;
                    return;
                }

                if (IntersectPixels(center2Rect, centerTextureData, rectPlayer, playerTextureData))
                {
                    Player.isActive = false;
                    Player.Health -= 1;
                    return;
                }

                if (IntersectPixels(center3Rect, centerTextureData, rectPlayer, playerTextureData))
                {
                    Player.isActive = false;
                    Player.Health -= 1;
                    return;
                }
            }



            Rectangle left1Rect = new Rectangle((int)leftBlock1Position.X, (int)leftBlock1Position.Y, sideBlock.Width, sideBlock.Height);
            Rectangle left2Rect = new Rectangle((int)leftBlock2Position.X, (int)leftBlock2Position.Y, sideBlock.Width, sideBlock.Height);
            Rectangle left3Rect = new Rectangle((int)leftBlock3Position.X, (int)leftBlock3Position.Y, sideBlock.Width, sideBlock.Height);
            //Bounding draw and collision rectangle of the three center rects
            Rectangle boundingleftRect = new Rectangle(0, 100, 250, 300);

            if (boundingleftRect.Intersects(rectPlayer))
            {

                if (IntersectPixels(left1Rect, sideBlockTextureData, rectPlayer, playerTextureData))
                {
                    Player.isActive = false;
                    Player.Health -= 1;
                    return;
                }

                if (IntersectPixels(left2Rect, sideBlockTextureData, rectPlayer, playerTextureData))
                {
                    Player.isActive = false;
                    Player.Health -= 1;
                    return;
                }

                if (IntersectPixels(left3Rect, sideBlockTextureData, rectPlayer, playerTextureData))
                {
                    Player.isActive = false;
                    Player.Health -= 1;
                    return;
                }
            }



            Rectangle right1Rect = new Rectangle((int)leftBlock1Position.X, (int)leftBlock1Position.Y, sideBlock.Width, sideBlock.Height);
            Rectangle right2Rect = new Rectangle((int)leftBlock2Position.X, (int)leftBlock2Position.Y, sideBlock.Width, sideBlock.Height);
            //Bounding draw and collision rectangle of the three center rects
            Rectangle boundingrightRect = new Rectangle(550, 150, 250, 100);

            if (boundingrightRect.Intersects(rectPlayer))
            {

                if (IntersectPixels(right1Rect, sideBlockTextureData, rectPlayer, playerTextureData))
                {
                    Player.isActive = false;
                    Player.Health -= 1;
                    return;
                }

                if (IntersectPixels(right2Rect, sideBlockTextureData, rectPlayer, playerTextureData))
                {
                    Player.isActive = false;
                    Player.Health -= 1;
                    return;
                }
            }


        }


        protected override void customLevelDraw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(center1, center1Position, Color.White);
            spritebatch.Draw(center2, center2Position, Color.White);
            spritebatch.Draw(center3, center3Position, Color.White);

            spritebatch.Draw(sideBlock, leftBlock1Position, Color.White);
            spritebatch.Draw(sideBlock, leftBlock2Position, Color.White);
            spritebatch.Draw(sideBlock, leftBlock3Position, Color.White);

            spritebatch.Draw(sideBlock, rightBlock1Position, Color.White);
            spritebatch.Draw(sideBlock, rightBlock2Position, Color.White);

            base.customLevelDraw(spritebatch);
        }
    }
}