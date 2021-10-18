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

        float angle = 0f;

        bool firstTurn() 
        {
            if (angle == 0f)
                return true;
            else if (angle == (float)Math.PI * -1f)
                return false;
            else
                return false;
        }
        bool secondTurn()
        {
            if (angle == (float)Math.PI * -1f)
                return true;
            else if (angle == 0f)
                return false;
            else
                return false;
        }

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

            LocalPosition = new Vector2(256, 0);

            origin = new Vector2(sprite.Width / 2, sprite.Height / 2);

            rect = new Vector2(32, 32);
            velocity = new Vector2(0, 100f + (0.15f * level));
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

                if (inputHelper.KeyPressed(Keys.Space))
                {
                    if (firstTurn())
                    {
                        localPosition.X -= 32;
                    }
                    else if (secondTurn())
                    {
                        localPosition.X += 32;
                    }
                    angle = angle + (float)Math.PI / -2f;
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (LocalPosition.X <= 0)
                localPosition.X = 0;

            if (LocalPosition.X >= 352)
                localPosition.X = 352;

            if (LocalPosition.Y >= 640)
            {
                TetrisGrid.NextBlock();
                Reset();
            }
        }

        // Draws the current block on the grid.
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, GlobalPosition, null, Color.White, angle, rect, 1.0f, SpriteEffects.None, 0);
        }

        // Draws the incoming block in the HUD.
        public void DrawNext(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, new Vector2(660, 216), null, Color.White, angle, rect, 1.0f, SpriteEffects.None, 0);
        }

        // Draws the currently held Block in the HUD.
        public void DrawHeld(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, new Vector2(916, 216), null, Color.White, angle, rect, 1.0f, SpriteEffects.None, 0);
        }

        public override void Reset()
        {
            LocalPosition = new Vector2(256, 0);
            int lastBlock = BlockType;
            BlockType = ExtendedGame.Random.Next(7);
            angle = 0f;
        }

        public Vector2 MovementX()
        {
            return new Vector2(32, 0);
        }
        public Vector2 MovementY()
        {
            return new Vector2(0, 32);
        }
    }
}