using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Ultratap
{
    class level10 : GameplayScreen
    {

        // Constructor ( Sets the initial player position, the initial aim position, and the levels texture)
        public level10()
            : base(new Vector2(150, 230), new Vector2(700, 220), "level/basic_level", 10)
        {
        }


        Texture2D rescueRing;
        Color[] rescueRingTextureData;
        Vector2 rescueRingOrigin;
        const float RotateSpeed = 0.4f;
        Block rescueBlock;

        Texture2D circle;
        Vector2 circlePosition;
        Color[] circleTextureData;
        Vector2 circleOrigion;
        float circleScaleSpeed = 0.3f;
        float currentScale = 1.0f;

        Texture2D zickzack;
        Color[] zickzackTextureData;
        Vector2 zickzackPosition;


        protected override void customActivate(ContentManager content)
        {

            rescueRing = content.Load<Texture2D>("level/rescueRing");
            rescueRingOrigin = new Vector2(rescueRing.Width / 2, rescueRing.Height / 2);
            rescueBlock = new Block();
            rescueBlock.Position = new Vector2(400, 240);
            rescueBlock.Rotation = 0.5f * MathHelper.TwoPi;

            circle = content.Load<Texture2D>("level/circle280");
            circlePosition = new Vector2(400, 240);
            circleOrigion = new Vector2(circle.Width / 2, circle.Height / 2);

            zickzack = content.Load<Texture2D>("level/zickzack800x400");
            zickzackPosition = new Vector2(-775, 40);

        
            // Extract collision data
            rescueRingTextureData = new Color[rescueRing.Width * rescueRing.Height];
            rescueRing.GetData(rescueRingTextureData);

            circleTextureData = new Color[circle.Width * circle.Height];
            circle.GetData(circleTextureData);

            zickzackTextureData = new Color[zickzack.Width * zickzack.Height];
            zickzack.GetData(zickzackTextureData);

            base.customActivate(content);
        }


        protected override void CustomLevelUpdate(GameTime gameTime)
        {


            rescueBlock.Rotation += RotateSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            currentScale += circleScaleSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (currentScale > 1.0f)
            {
                // if the block has grown to 150%,
                // reverse the scaling process
                currentScale = 1.0f;
                circleScaleSpeed *= -1.0f;
            }
            else if (currentScale < 0.2f)
            {
                // if the block has shrunk to 50%,
                // reverste the scaling process
                currentScale = 0.2f;
                circleScaleSpeed *= -1.0f;

            }


            zickzackPosition.X += 10.0f * (float)gameTime.ElapsedGameTime.TotalSeconds;


            UpdateCollisions();

            base.CustomLevelUpdate(gameTime);
        }


        private void UpdateCollisions()
        {
            // Update the person's transform and rectangle
            Matrix personTransform = Matrix.CreateTranslation(new Vector3(Player.Position, 0.0f));
            Rectangle personRectangle = new Rectangle((int)Player.Position.X, (int)Player.Position.Y, Player.Width, Player.Height);

            // Update the rescueRing transform and rectangle
            Matrix blockTransform = Matrix.CreateTranslation(new Vector3(-rescueRingOrigin, 0.0f)) * Matrix.CreateRotationZ(rescueBlock.Rotation) * Matrix.CreateTranslation(new Vector3(rescueBlock.Position, 0.0f));
            Rectangle blockRectangle = CalculateBoundingRectangle(new Rectangle(0, 0, rescueRing.Width, rescueRing.Height), blockTransform);

            // The per-pixel check is expensive, so check the bounding rectangles
            // first to prevent testing pixels when collisions are impossible.
            if (personRectangle.Intersects(blockRectangle))
            {
                // Check collision with person
                if (IntersectPixels(personTransform, Player.Width,
                                    Player.Height, playerTextureData,
                                    blockTransform, rescueRing.Width,
                                    rescueRing.Height, rescueRingTextureData))
                {
                    Player.isActive = false;
                    Player.Health -= 1;
                    zickzackPosition.X = -775;
                    return;
                }
            }


            // Update the circle transform and rectangle
            Matrix circleTransform = Matrix.CreateTranslation(new Vector3(-circleOrigion, 0.0f)) * Matrix.CreateScale(currentScale) * Matrix.CreateTranslation(new Vector3(circlePosition, 0.0f));
            Rectangle circleRect = CalculateBoundingRectangle(new Rectangle(0, 0, circle.Width, circle.Height), circleTransform);

            // The per-pixel check is expensive, so check the bounding rectangles
            // first to prevent testing pixels when collisions are impossible.
            if (personRectangle.Intersects(circleRect))
            {
                // Check collision with person
                if (IntersectPixels(personTransform, Player.Width,
                                    Player.Height, playerTextureData,
                                    circleTransform, circle.Width,
                                    circle.Height, circleTextureData))
                {
                    Player.isActive = false;
                    Player.Health -= 1;
                    zickzackPosition.X = -775;
                    return;
                }
            }


            // Check for collisions with the zickzack
            Rectangle zickzackRect = new Rectangle((int)zickzackPosition.X, (int)zickzackPosition.Y, zickzack.Width, zickzack.Height);

            if (IntersectPixels(zickzackRect, zickzackTextureData, personRectangle, playerTextureData))
            {
                Player.isActive = false;
                Player.Health -= 1;
                zickzackPosition.X = -775;
                return;
            }

        }


        protected override void customLevelDraw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(rescueRing, rescueBlock.Position, null, Color.White, rescueBlock.Rotation, rescueRingOrigin, 1.0f, SpriteEffects.None, 0.0f);
            spritebatch.Draw(circle, circlePosition, null, Color.White, 0.0f, circleOrigion, currentScale, SpriteEffects.None, 0.0f);
            spritebatch.Draw(zickzack, zickzackPosition, Color.White);

            base.customLevelDraw(spritebatch);
        }

    }
}
