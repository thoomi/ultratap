using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Ultratap
{
    class level8 : GameplayScreen
    {

        // Constructor ( Sets the initial player position, the initial aim position, and the levels texture)
        public level8()
            : base(new Vector2(50, 380), new Vector2(680, 370), "level/level8", 8)
        {
        }

        #region Fields
        Texture2D rotor;
        Color[] rotorTextureData;
        Vector2 rotorOrigin;
        const float BlockRotateSpeed = 0.6f;
        Block rotorBlock1;
        Block rotorBlock2;

        Texture2D hammer;
        Color[] hammerTextureData;
        Vector2 hammerPosition;
        int hammerDirection = 1;
        


        #endregion



        protected override void customActivate(ContentManager content)
        {
            rotor = content.Load<Texture2D>("level/rotor300x300");
            hammer = content.Load<Texture2D>("level/rect200x500");

            // Initial hammer Position
            hammerPosition = new Vector2(300, -399);

            // Calculate the rotor origion
            rotorOrigin = new Vector2(rotor.Width / 2, rotor.Height / 2);

            rotorBlock1 = new Block();
            rotorBlock1.Position = new Vector2(150, 190);
            rotorBlock1.Rotation = 0.5f * MathHelper.TwoPi;

            rotorBlock2 = new Block();
            rotorBlock2.Position = new Vector2(650, 190);
            rotorBlock2.Rotation = 0.5f * MathHelper.TwoPi;
            
            // Extract collision data
            rotorTextureData = new Color[rotor.Width * rotor.Height];
            rotor.GetData(rotorTextureData);

            hammerTextureData = new Color[hammer.Width * hammer.Height];
            hammer.GetData(hammerTextureData);


            base.customActivate(content);
        }


        protected override void CustomLevelUpdate(GameTime gameTime)
        {
            // Update the person's transform and rectangle
            Matrix personTransform = Matrix.CreateTranslation(new Vector3(Player.Position, 0.0f));
            Rectangle personRectangle = new Rectangle((int)Player.Position.X, (int)Player.Position.Y, Player.Width, Player.Height);

            // Update the rotors rotation angle
            rotorBlock1.Rotation -= BlockRotateSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            rotorBlock2.Rotation += 0.8f * (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Update the hammers position
            if (hammerPosition.Y <= -400 || hammerPosition.Y >= -250)
            {
                hammerDirection *= -1;
            }
            hammerPosition.Y += 100.0f * (float)gameTime.ElapsedGameTime.TotalSeconds * hammerDirection;

            // Update the rotors transform-matrix
            Matrix block1Transform = Matrix.CreateTranslation(new Vector3(-rotorOrigin, 0.0f)) * Matrix.CreateRotationZ(rotorBlock1.Rotation) * Matrix.CreateTranslation(new Vector3(rotorBlock1.Position, 0.0f));
            Matrix block2Transform = Matrix.CreateTranslation(new Vector3(-rotorOrigin, 0.0f)) * Matrix.CreateRotationZ(rotorBlock2.Rotation) * Matrix.CreateTranslation(new Vector3(rotorBlock2.Position, 0.0f));

            // Update the rotors rectangles
            Rectangle block1Rectangle = CalculateBoundingRectangle(new Rectangle(0, 0, rotor.Width, rotor.Height), block1Transform);
            Rectangle block2Rectangle = CalculateBoundingRectangle(new Rectangle(0, 0, rotor.Width, rotor.Height), block2Transform);


            // The per-pixel check is expensive, so check the bounding rectangles
            // first to prevent testing pixels when collisions are impossible.
            if (personRectangle.Intersects(block1Rectangle))
            {
                // Check collision with person
                if (IntersectPixels(personTransform, Player.Width,
                                    Player.Height, playerTextureData,
                                    block1Transform, rotor.Width,
                                    rotor.Height, rotorTextureData))
                {
                    Player.isActive = false;
                    Player.Health -= 1;
                }
            }
            
            // The per-pixel check is expensive, so check the bounding rectangles
            // first to prevent testing pixels when collisions are impossible.
            if (personRectangle.Intersects(block2Rectangle))
            {
                // Check collision with person
                if (IntersectPixels(personTransform, Player.Width,
                                    Player.Height, playerTextureData,
                                    block2Transform, rotor.Width,
                                    rotor.Height, rotorTextureData))
                {
                    Player.isActive = false;
                    Player.Health -= 1;
                }
            }

            // Hammer Pixel Collision
            Rectangle rectObstacle = new Rectangle((int)hammerPosition.X, (int)hammerPosition.Y, hammer.Width, hammer.Height);

            if (IntersectPixels(rectObstacle, hammerTextureData, personRectangle, playerTextureData))
            {
                Player.isActive = false;
                Player.Health -= 1;
            }


            base.CustomLevelUpdate(gameTime);
        }

        protected override void customLevelDraw(SpriteBatch spritebatch)
        {

            spritebatch.Draw(rotor, rotorBlock1.Position, null, Color.White, rotorBlock1.Rotation, rotorOrigin, 1.0f, SpriteEffects.None, 0.0f);
            spritebatch.Draw(rotor, rotorBlock2.Position, null, Color.White, rotorBlock2.Rotation, rotorOrigin, 1.0f, SpriteEffects.None, 0.0f);

            spritebatch.Draw(hammer, hammerPosition, Color.White);

            base.customLevelDraw(spritebatch);
        }

    }
}
