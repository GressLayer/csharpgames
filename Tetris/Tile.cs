using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

using Tetris;

namespace Tetris
{

    class Tile : SpriteGameObject
    {
        public int colorNumber { get; set; }
        public bool IsOccupied { get; set; }

        Rectangle spriteRectangle;

        public Color currentColor { get; set; }

        int index;

        public Tile() : base("sprites/Tetromino")
        {
            colorNumber = BlockObject.BlockType + 1;
            
            if (IsOccupied == false)
                index = 0;
            if (IsOccupied == true)
                index = 1;
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            if (inputHelper.KeyPressed(Keys.H))
                IsOccupied = !IsOccupied;
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
                if (IsOccupied)
                {
                    switch (colorNumber)
                    {
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
                    currentColor = Color.White;

                spriteRectangle = new Rectangle(index * sprite.Height, 0, sprite.Height, sprite.Height);
                spriteBatch.Draw(sprite, GlobalPosition, spriteRectangle, currentColor, 0, origin, 1.0f, SpriteEffects.None, 0);
            }
        }

        public void SetTile()
        {
            IsOccupied = true;
        }
    }
}
