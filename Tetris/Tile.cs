using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Tetris
{
    class Tile : SpriteGameObject
    {
        public int ColorNumber { get; private set; }

        public bool IsOccupied { get; set; }

        Rectangle spriteRectangle;


        int index;

        public Tile() : base("sprites/Tetromino")
        {

            if (IsOccupied == false)
                index = 0;
            if (IsOccupied == true)
                index = 1;

            /*if (IsOccupied)
            {
                switch (colorNumber)
                {
                    case (0): currentColor = Color.White; break;
                    case (1): currentColor = Color.Orange; break;
                    case (2): currentColor = Color.Blue; break;
                    case (3): currentColor = Color.Red; break;
                    case (4): currentColor = Color.Lime; break;
                    case (5): currentColor = Color.Cyan; break;
                    case (6): currentColor = Color.Yellow; break;
                    case (7): currentColor = Color.Magenta; break;
                }
            }
            else
                currentColor = Color.White;*/
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            if (inputHelper.KeyPressed(Keys.H))
                IsOccupied = true;
        }
        public override void Update(GameTime gameTime)
        {

            if (IsOccupied == false)
                index = 0;
            if (IsOccupied == true)
                index = 1;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            {
                spriteRectangle = new Rectangle(index * sprite.Height, 0, sprite.Height, sprite.Height);
                spriteBatch.Draw(sprite, GlobalPosition, spriteRectangle, Color.White, 0, origin, 1.0f, SpriteEffects.None, 0);
            }
        }

        public void SetTile()
        {
            IsOccupied = true;
        }
    }
}
