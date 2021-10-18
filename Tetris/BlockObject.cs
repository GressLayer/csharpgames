using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

using Tetris;

namespace Tetris
{
    class BlockObject : SpriteGameObject
    {
        public static int BlockType { get; set; }
        float level = 1.0f;

        public static Vector2 rect;

        float angle;

        bool isTilted;

        static string shape()
        {
            // BlockType = ExtendedGame.Random.Next(7);
            if (BlockType == 0) return "sprites/blockL";
            else if (BlockType == 1) return "sprites/blockJ";
            else if (BlockType == 2) return "sprites/blockS";
            else if (BlockType == 3) return "sprites/blockZ";
            else if (BlockType == 4) return "sprites/blockI";
            else if (BlockType == 5) return "sprites/blockO";
            else if (BlockType == 6) return "sprites/blockT";
            else return "sprites/blockL";
        }

        public BlockObject(int blockType) : base(shape())
        {
            blockType = BlockType;

            origin = new Vector2(sprite.Width / 2, sprite.Height / 2);

            LocalPosition = new Vector2(128 + origin.X, 32 + origin.Y) ;
            angle = 0f;

            velocity = new Vector2(0, 100f + (0.15f * level)) /** T*/;

            isTilted = false;
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            if (GameWorld.gameState == State.Playing) {
                if (inputHelper.KeyPressed(Keys.Left))
                    LocalPosition = LocalPosition - MovementX();

                if (inputHelper.KeyPressed(Keys.Right))
                    LocalPosition = LocalPosition + MovementX();

                if (inputHelper.KeyDown(Keys.Down))
                    LocalPosition = LocalPosition + MovementY();
                    
                if (inputHelper.KeyPressed(Keys.D))
                {
                    angle = angle + (float)Math.PI / -2f;
                    isTilted = !isTilted;
                    localPosition.X = localPosition.X + 16;
                }

                if (inputHelper.KeyPressed(Keys.A))
                {
                    angle = angle + (float)Math.PI / +2f;
                    isTilted = !isTilted;
                    localPosition.X = localPosition.X - 16;
                }
            }
        }

        public override void Update(GameTime gameTime)
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
                TetrisGrid.NextBlock();
                Reset();

            //LocalPosition += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
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

        public override void Reset()
        {
            LocalPosition = new Vector2(256, 0);
            int lastBlock = BlockType;
            BlockType = ExtendedGame.Random.Next(7);
            angle = 0f;
            shape();
            
        }

        public Vector2 MovementX()
        {
            return new Vector2(32, 0);
        }
        public Vector2 MovementY()
        {
            return new Vector2(0, 32);
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
    }
}