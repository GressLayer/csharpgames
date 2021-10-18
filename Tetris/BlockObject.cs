using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Tetris
{
    class BlockObject : SpriteGameObject
    {

        public static int BlockType { get; set; }
        float level = 0f;
        
        Rectangle spriteRectangle;
        public static Vector2 rect;
        
        float angle;

        static string shape()
        {
            if (BlockType == 1) return "sprites/blockL";
            if (BlockType == 2) return "sprites/blockJ";
            if (BlockType == 3) return "sprites/blockS";
            if (BlockType == 4) return "sprites/blockZ";
            if (BlockType == 5) return "sprites/blockI";
            if (BlockType == 6) return "sprites/blockO";
            if (BlockType == 7) return "sprites/blockT";
            else return "sprites/blockL";
        }

        public BlockObject() : base(shape())
        {
            BlockType = ExtendedGame.Random.Next(7);
            spriteRectangle = new Rectangle(sprite.Width, 0, sprite.Width, sprite.Height);

            LocalPosition = new Vector2(160, 32);
            angle = 0f;

            rect = new Vector2(32, 32);
            velocity = new Vector2(0, 100f + (0.15f * level));
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            if (inputHelper.KeyPressed(Keys.Left))
                LocalPosition = LocalPosition - new Vector2(32, 0);

            if (inputHelper.KeyPressed(Keys.Right))
                LocalPosition = LocalPosition + new Vector2(32, 0);

            if (inputHelper.KeyDown(Keys.Down))
                LocalPosition = LocalPosition + MovementY();
                
            if(inputHelper.KeyPressed(Keys.F))
                angle = angle + (float)Math.PI / -2f;

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (LocalPosition.X <= 0)
                localPosition.X = 0;

            if (LocalPosition.X >= 256)
                localPosition.X = 256;

            if (LocalPosition.Y >= 640)
                Reset();
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, GlobalPosition, null, Color.White, angle, rect, 1.0f, SpriteEffects.None, 0);
        }

        public void DrawNext(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, new Vector2(660, 216), null, Color.White, angle, rect, 1.0f, SpriteEffects.None, 0);
        }

        public void DrawHeld(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, new Vector2(916, 216), null, Color.White, angle, rect, 1.0f, SpriteEffects.None, 0);
        }

        public override void Reset()
        {
            LocalPosition = new Vector2(160, 32);
            int lastBlock = BlockType;
            BlockType = ExtendedGame.Random.Next(7);
            
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