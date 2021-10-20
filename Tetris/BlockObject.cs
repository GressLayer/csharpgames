using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

using Tetris;

namespace Tetris
{
    class BlockObject : SpriteGameObject
    {
        public static int BlockType { get; set; } = ExtendedGame.Random.Next(7);

        float angle;

        bool isTilted;

        public static string shape()
        {
            //BlockType = ExtendedGame.Random.Next(7);
            if (BlockType == 0) return "sprites/blockL";
            else if (BlockType == 1) return "sprites/blockJ";
            else if (BlockType == 2) return "sprites/blockS";
            else if (BlockType == 3) return "sprites/blockZ";
            else if (BlockType == 4) return "sprites/blockI";
            else if (BlockType == 5) return "sprites/blockO";
            else if (BlockType == 6) return "sprites/blockT";
            else return "sprites/blockL";
        }

        public BlockObject() : base(shape())
        {
            BlockType = ExtendedGame.Random.Next(7);

            /* Sets the origin of the block.
            /* Blocks with an origin that is NOT a multiple of 32 would be placed halfway inside a grid space.
             * These get a default origin value.
             */
            if (sprite.Width % 64 != 0)
                origin = new Vector2(32, sprite.Height / 2);
            else if (sprite.Height % 64 != 0)
                origin = new Vector2(sprite.Width / 2, 32);
            else
                origin = new Vector2(sprite.Width / 2, sprite.Height / 2);

            LocalPosition = new Vector2(origin.X, origin.Y);
            angle = 0f;

            isTilted = false;
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            // These inputs should ONLY be possible during the Playing state: this enclosing if-statement makes sure of it.
            if (GameWorld.gameState == State.Playing) 
            {
                // Left and right movement. As simple as it gets.
                if (inputHelper.KeyPressed(Keys.Left))
                    LocalPosition += new Vector2(-32, 0);
                if (inputHelper.KeyPressed(Keys.Right))
                    LocalPosition += new Vector2(32, 0);

                /* Further elaboration:
                 *   The combination of KeyPressed and KeyUp (instead of KeyDown) is used for a reason.
                 *   Using KeyDown constantly multiplies velocity.Y by 3, causing exponential speed growth.
                 *   The button needs to be held down anyway to not activate the KeyUp if-statement, so KeyPressed works.
                 *   
                 *   KeyUp is also the initial state, so it still allows for velocity to get its initial value.
                 */
                if (inputHelper.KeyPressed(Keys.Down))
                    velocity *= 3;
                if (inputHelper.KeyUp(Keys.Down))
                    velocity = 100 + (15 * TetrisGrid.level);

                // Right rotation.
                if (inputHelper.KeyPressed(Keys.D))
                {
                    angle = angle + (float)Math.PI / -2f;
                    isTilted = !isTilted;
                }
                
                // Left rotation.
                if (inputHelper.KeyPressed(Keys.A))
                {
                    angle = angle + (float)Math.PI / +2f;
                    isTilted = !isTilted;
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (GameWorld.gameState == State.Playing)
            {
                base.Update(gameTime);
                if (LocalPosition.X <= 0 + origin.X && isTilted == false)
                    localPosition.X = 0 + origin.X;

                if (LocalPosition.X <= 0 + origin.Y && isTilted == true)
                    localPosition.X = 0 + origin.Y;

                if (LocalPosition.X >= 256 + origin.X && isTilted == false)
                    localPosition.X = 256 + origin.X;

                if (LocalPosition.X >= 320 - origin.Y && isTilted == true)
                    localPosition.X = 320 - origin.Y;

                if (BoundingBox.Y >= 640)
                {
                    TetrisGrid.NextBlock();
                }

                localPosition.Y += velocity * (int)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }

        public override void Reset()
        {
            angle = 0f;
        }

        public override Rectangle BoundingBox
        {
            get
            {
                boundingBox = sprite.Bounds;
                boundingBox.Offset(GlobalPosition - origin);
                Parent = GameWorld.grid;
                return boundingBox;            
            }
        }

        // Draws the current block on the grid.
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, GlobalPosition, null, Color.White, angle, origin, 1.0f, SpriteEffects.None, 0);
        }
        // Draws the incoming block in the HUD.
        public void DrawNext(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, new Vector2(660, 250), null, Color.White, angle, origin, 1.0f, SpriteEffects.None, 0);
        }
        // Draws the currently held Block in the HUD.
        public void DrawHeld(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, new Vector2(916, 250), null, Color.White, angle, origin, 1.0f, SpriteEffects.None, 0);
        }
    }
}