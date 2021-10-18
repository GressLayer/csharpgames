using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Tetris
{
    class BlockObject : SpriteGameObject
    {

        public int BlockType { get; set; }

        Rectangle spriteRectangle;

        float angle;

        public BlockObject() : base("sprites/LBlock")
        {
            BlockType = ExtendedGame.Random.Next(7);

            LocalPosition = new Vector2(128, -64);
            angle = 0f;

            velocity = new Vector2(0, 128);
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            if (inputHelper.KeyPressed(Keys.Left))
                LocalPosition = LocalPosition - new Vector2(32, 0);
            if (inputHelper.KeyPressed(Keys.Right))
                LocalPosition = LocalPosition + new Vector2(32,0);
            if(inputHelper.KeyPressed(Keys.F))
                angle = angle + (float)Math.PI / 2f;

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (LocalPosition.X <= 0)
                localPosition.X = 0;

            if (LocalPosition.X >= 256 && BlockType != 4)
                localPosition.X = 256;

            if (LocalPosition.X >= 288)
                localPosition.X = 288;
            
            if (LocalPosition.Y >= 608)
                Reset();


        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            int index = BlockType;
            spriteRectangle = new Rectangle(index * sprite.Height / 2, 0, sprite.Height / 2, sprite.Height);
            spriteBatch.Draw(sprite, GlobalPosition, null, Color.White, angle, origin, 1.0f, SpriteEffects.None, 0);
        }

        public override void Reset()


        public Vector2 Movement()
        {
            LocalPosition = new Vector2(128, -64);
            int lastBlock;
            lastBlock = BlockType;
            BlockType = ExtendedGame.Random.Next(7);
            if (BlockType == lastBlock)
            Reset();
        }

    }
}