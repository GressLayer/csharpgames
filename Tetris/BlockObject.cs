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

        public BlockObject() : base ("sprites/blocks")
        {
            BlockType = ExtendedGame.Random.Next(7);
            int index = BlockType;
            spriteRectangle = new Rectangle(index * sprite.Height / 2, 0, sprite.Height / 2, sprite.Height);

           
            LocalPosition = new Vector2(128, -64);
            angle = 0f;
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            if (inputHelper.KeyPressed(Keys.Left))
                LocalPosition = LocalPosition - Movement();
            if (inputHelper.KeyPressed(Keys.Right))
                LocalPosition = LocalPosition + Movement();
            if(inputHelper.KeyPressed(Keys.F))
                angle = angle + 1.57f;
        }

        public override void Update(GameTime gameTime)
        {
            if (LocalPosition.X <= 0)
                localPosition.X = 0;
            if (LocalPosition.X >= 256)
                localPosition.X = 256;

        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, GlobalPosition, spriteRectangle, Color.White, angle, origin, 1.0f, SpriteEffects.None, 0);
        }

        public Vector2 Movement()
        {
            return new Vector2(32, 0);
        }
    }
}
