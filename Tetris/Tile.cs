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

        public bool IsLocked { get; set; }

        Rectangle spriteRectangle;

        public Color currentColor { get; set; }

        int index;

        public Point rotationGrid { get; set; }
        public int rotationGridX { get; set; }
        public int rotationGridY { get; set; }

        public Tile() : base("sprites/Tetromino")
        {   
            if (IsOccupied == false)
                index = 0;
            if (IsOccupied == true)
                index = 1;
            rotationGrid = Point.Zero;
        }

        public override void HandleInput(InputHelper inputHelper)
        {
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
                spriteBatch.Draw(sprite, GlobalPosition, spriteRectangle, currentColor, 0, origin, 1.0f, SpriteEffects.None, 0);
            }
        }

        public void SetTile()
        {
            IsOccupied = true;
        }
    }
}
