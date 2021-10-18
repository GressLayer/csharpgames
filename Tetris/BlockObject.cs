using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Tetris
{
    class BlockObject : SpriteGameObject
    {

        public static int BlockType { get; set; }
        
        Rectangle spriteRectangle;
        Vector2 rect;
        
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
            BlockType = 1 + ExtendedGame.Random.Next(6);
            int index = BlockType;
            spriteRectangle = new Rectangle(sprite.Width, sprite.Height, sprite.Width, sprite.Height);

            LocalPosition = new Vector2(192, 64);
            angle = 0f;

            rect = new Vector2(32, 96);
            velocity = new Vector2(0, 128);
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            if (inputHelper.KeyPressed(Keys.Left))
                LocalPosition = LocalPosition - new Vector2(32, 0);

            if (inputHelper.KeyPressed(Keys.Right))
                LocalPosition = LocalPosition + new Vector2(32,0);

            if (inputHelper.KeyDown(Keys.Down))
                LocalPosition = LocalPosition + MovementY();
                
            if(inputHelper.KeyPressed(Keys.F))
                angle = angle + (float)Math.PI / 2f;

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (LocalPosition.X <= 0)
                localPosition.X = 0;

            if (LocalPosition.X >= 256)
                localPosition.X = 256;

            if (LocalPosition.Y >= 640)
                localPosition.Y = 640;

            if (LocalPosition.Y >= 544)
                Reset();

        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            int index = BlockType;
            spriteRectangle = new Rectangle(index * sprite.Height / 2, 0, sprite.Height / 2, sprite.Height);
            spriteBatch.Draw(sprite, GlobalPosition, null, Color.White, angle, origin, 1.0f, SpriteEffects.None, 0);
        }

        public override void Reset()
        {
            spriteBatch.Draw(sprite, new Vector2(624, 312), null, Color.White, angle, rect, 1.0f, SpriteEffects.None, 0);
            LocalPosition = new Vector2(128, -64);
            int lastBlock = BlockType;
            BlockType = 1 + ExtendedGame.Random.Next(6);
            if (lastBlock == BlockType)
                Reset();
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