using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Ultratap
{
    class level11 : GameplayScreen
    {

        // Constructor ( Sets the initial player position, the initial aim position, and the levels texture)
        public level11()
            : base(new Vector2(87.5f, 127.5f), new Vector2(275, 115), "level/level11", 11)
        {
        }

        Texture2D circle;
        Color[] circleTextureData;
        Vector2 circleOrigin;

        const float circle1RotateSpeed = 1.0f;
        Block circle1Block;

        const float circle2RotateSpeed = 1.0f;
        Block circle2Block;

        const float circle3RotateSpeed = 0.9f;
        Block circle3Block;

        const float circle4RotateSpeed = 0.9f;
        Block circle4Block;

        const float circle5RotateSpeed = 0.9f;
        Block circle5Block;


        protected override void customActivate(ContentManager content)
        {
            circle = content.Load<Texture2D>("level/circle200");
            circleTextureData = new Color[circle.Width * circle.Height];
            circle.GetData(circleTextureData);

            circleOrigin = new Vector2(circle.Width / 2, circle.Height / 2);

            circle1Block = new Block();
            circle1Block.Position = new Vector2(100, 340);
            circle1Block.Rotation =  0.25f * MathHelper.Pi;

            circle2Block = new Block();
            circle2Block.Position = new Vector2(300, 340);
            circle2Block.Rotation = 1.25f * MathHelper.Pi;

            circle3Block = new Block();
            circle3Block.Position = new Vector2(700, 340);
            circle3Block.Rotation = 0.8f * MathHelper.TwoPi;

            circle4Block = new Block();
            circle4Block.Position = new Vector2(700, 140);
            circle4Block.Rotation = 0.8f * MathHelper.Pi;

            circle5Block = new Block();
            circle5Block.Position = new Vector2(500, 140);
            circle5Block.Rotation = -0.2f;

            base.customActivate(content);
        }


        protected override void CustomLevelUpdate(GameTime gameTime)
        {

            circle1Block.Rotation += circle1RotateSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            circle2Block.Rotation += circle2RotateSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            circle3Block.Rotation += circle3RotateSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            circle4Block.Rotation += circle4RotateSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            circle5Block.Rotation += circle5RotateSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;



            UpdateCollisions();

            base.CustomLevelUpdate(gameTime);
        }


        private void UpdateCollisions()
        {
            // Update the person's transform and rectangle
            Matrix personTransform = Matrix.CreateTranslation(new Vector3(Player.Position, 0.0f));
            Rectangle personRectangle = new Rectangle((int)Player.Position.X, (int)Player.Position.Y, Player.Width, Player.Height);

            // Update the rescueRing transform and rectangle
            Matrix circle1Transform = Matrix.CreateTranslation(new Vector3(-circleOrigin, 0.0f)) * Matrix.CreateRotationZ(circle1Block.Rotation) * Matrix.CreateTranslation(new Vector3(circle1Block.Position, 0.0f));
            Rectangle circle1Rectangle = CalculateBoundingRectangle(new Rectangle(0, 0, circle.Width, circle.Height), circle1Transform);

            // The per-pixel check is expensive, so check the bounding rectangles
            // first to prevent testing pixels when collisions are impossible.
            if (personRectangle.Intersects(circle1Rectangle))
            {
                // Check collision with person
                if (IntersectPixels(personTransform, Player.Width,
                                    Player.Height, playerTextureData,
                                    circle1Transform, circle.Width,
                                    circle.Height, circleTextureData))
                {
                    Player.isActive = false;
                    Player.Health -= 1;
                    return;
                }
            }

            // Update the rescueRing transform and rectangle
            Matrix circle2Transform = Matrix.CreateTranslation(new Vector3(-circleOrigin, 0.0f)) * Matrix.CreateRotationZ(circle2Block.Rotation) * Matrix.CreateTranslation(new Vector3(circle2Block.Position, 0.0f));
            Rectangle circle2Rectangle = CalculateBoundingRectangle(new Rectangle(0, 0, circle.Width, circle.Height), circle2Transform);

            // The per-pixel check is expensive, so check the bounding rectangles
            // first to prevent testing pixels when collisions are impossible.
            if (personRectangle.Intersects(circle2Rectangle))
            {
                // Check collision with person
                if (IntersectPixels(personTransform, Player.Width,
                                    Player.Height, playerTextureData,
                                    circle2Transform, circle.Width,
                                    circle.Height, circleTextureData))
                {
                    Player.isActive = false;
                    Player.Health -= 1;
                    return;
                }
            }



            // Update the rescueRing transform and rectangle
            Matrix circle3Transform = Matrix.CreateTranslation(new Vector3(-circleOrigin, 0.0f)) * Matrix.CreateRotationZ(circle3Block.Rotation) * Matrix.CreateTranslation(new Vector3(circle3Block.Position, 0.0f));
            Rectangle circle3Rectangle = CalculateBoundingRectangle(new Rectangle(0, 0, circle.Width, circle.Height), circle3Transform);

            // The per-pixel check is expensive, so check the bounding rectangles
            // first to prevent testing pixels when collisions are impossible.
            if (personRectangle.Intersects(circle3Rectangle))
            {
                // Check collision with person
                if (IntersectPixels(personTransform, Player.Width,
                                    Player.Height, playerTextureData,
                                    circle3Transform, circle.Width,
                                    circle.Height, circleTextureData))
                {
                    Player.isActive = false;
                    Player.Health -= 1;
                    return;
                }
            }


            // Update the rescueRing transform and rectangle
            Matrix circle4Transform = Matrix.CreateTranslation(new Vector3(-circleOrigin, 0.0f)) * Matrix.CreateRotationZ(circle4Block.Rotation) * Matrix.CreateTranslation(new Vector3(circle4Block.Position, 0.0f));
            Rectangle circle4Rectangle = CalculateBoundingRectangle(new Rectangle(0, 0, circle.Width, circle.Height), circle4Transform);

            // The per-pixel check is expensive, so check the bounding rectangles
            // first to prevent testing pixels when collisions are impossible.
            if (personRectangle.Intersects(circle4Rectangle))
            {
                // Check collision with person
                if (IntersectPixels(personTransform, Player.Width,
                                    Player.Height, playerTextureData,
                                    circle4Transform, circle.Width,
                                    circle.Height, circleTextureData))
                {
                    Player.isActive = false;
                    Player.Health -= 1;
                    return;
                }
            }



            // Update the rescueRing transform and rectangle
            Matrix circle5Transform = Matrix.CreateTranslation(new Vector3(-circleOrigin, 0.0f)) * Matrix.CreateRotationZ(circle5Block.Rotation) * Matrix.CreateTranslation(new Vector3(circle5Block.Position, 0.0f));
            Rectangle circle5Rectangle = CalculateBoundingRectangle(new Rectangle(0, 0, circle.Width, circle.Height), circle5Transform);

            // The per-pixel check is expensive, so check the bounding rectangles
            // first to prevent testing pixels when collisions are impossible.
            if (personRectangle.Intersects(circle5Rectangle))
            {
                // Check collision with person
                if (IntersectPixels(personTransform, Player.Width,
                                    Player.Height, playerTextureData,
                                    circle5Transform, circle.Width,
                                    circle.Height, circleTextureData))
                {
                    Player.isActive = false;
                    Player.Health -= 1;
                    return;
                }
            }

        }


        protected override void customLevelDraw(SpriteBatch spritebatch)
        {

            spritebatch.Draw(circle, circle1Block.Position, null, Color.White, circle1Block.Rotation, circleOrigin, 1.0f, SpriteEffects.None, 0.0f);
            spritebatch.Draw(circle, circle2Block.Position, null, Color.White, circle2Block.Rotation, circleOrigin, 1.0f, SpriteEffects.None, 0.0f);
            spritebatch.Draw(circle, circle3Block.Position, null, Color.White, circle3Block.Rotation, circleOrigin, 1.0f, SpriteEffects.None, 0.0f);
            spritebatch.Draw(circle, circle4Block.Position, null, Color.White, circle4Block.Rotation, circleOrigin, 1.0f, SpriteEffects.None, 0.0f);
            spritebatch.Draw(circle, circle5Block.Position, null, Color.White, circle5Block.Rotation, circleOrigin, 1.0f, SpriteEffects.None, 0.0f);


            base.customLevelDraw(spritebatch);
        }

    }
}
