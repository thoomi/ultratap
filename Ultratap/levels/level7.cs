using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Ultratap
{
    class level7 : GameplayScreen
    {

        // Constructor ( Sets the initial player position, the initial aim position, and the levels texture)
        public level7()
            : base(new Vector2(50, 240), new Vector2(750, 240), "level/level7", 7)
        {
        }


        Texture2D rotor;
        Color[] rotorTextureData;
        Vector2 rotorOrigin;
        const float BlockRotateSpeed = 0.6f;

        Block rotorBlock;



        protected override void customActivate(ContentManager content)
        {

            rotor = content.Load<Texture2D>("level/rotor");

            // Calculate the block origin
            rotorOrigin = new Vector2(rotor.Width / 2, rotor.Height / 2);
            rotorBlock = new Block();
            rotorBlock.Position = new Vector2(400, 240);
            rotorBlock.Rotation = 0.5f * MathHelper.TwoPi;

            // Extract collision data
            rotorTextureData = new Color[rotor.Width * rotor.Height];
            rotor.GetData(rotorTextureData);

            base.customActivate(content);
        }


        protected override void CustomLevelUpdate(GameTime gameTime)
        {
            // Update the person's transform
            Matrix personTransform = Matrix.CreateTranslation(new Vector3(Player.Position, 0.0f));
            // Get the bounding rectangle of the person
            Rectangle personRectangle = new Rectangle((int)Player.Position.X, (int)Player.Position.Y, Player.Width, Player.Height);

            rotorBlock.Rotation += BlockRotateSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            Matrix blockTransform = Matrix.CreateTranslation(new Vector3(-rotorOrigin, 0.0f)) * Matrix.CreateRotationZ(rotorBlock.Rotation) * Matrix.CreateTranslation(new Vector3(rotorBlock.Position, 0.0f));

            // Calculate the bounding rectangle of this block in world space
            Rectangle blockRectangle = CalculateBoundingRectangle(
                     new Rectangle(0, 0, rotor.Width, rotor.Height),
                     blockTransform);

            // The per-pixel check is expensive, so check the bounding rectangles
            // first to prevent testing pixels when collisions are impossible.
            if (personRectangle.Intersects(blockRectangle))
            {
                // Check collision with person
                if (IntersectPixels(personTransform, Player.Width,
                                    Player.Height, playerTextureData,
                                    blockTransform, rotor.Width,
                                    rotor.Height, rotorTextureData))
                {
                    Player.isActive = false;
                    Player.Health -= 1;
                }
            }


            base.CustomLevelUpdate(gameTime);
        }


        protected override void customLevelDraw(SpriteBatch spritebatch)
        {

            spritebatch.Draw(rotor, rotorBlock.Position, null, Color.White, rotorBlock.Rotation, rotorOrigin, 1.0f, SpriteEffects.None, 0.0f);

            base.customLevelDraw(spritebatch);
        }

    }
}

