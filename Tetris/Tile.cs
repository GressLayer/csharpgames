using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Tetris
{
    class Tile : SpriteGameObject
    {
        public int ColorNumber { get; private set; }
        Color currentColor;
        public Tile(int color): base("sprites/empty")
        {
            ColorNumber = color;
            if (ColorNumber == 1)
                currentColor = Color.LimeGreen;
            else if (ColorNumber == 2)
                currentColor = Color.Blue;
            else if (ColorNumber == 3)
                currentColor = Color.Red;
            else if (ColorNumber == 4)
                currentColor = Color.Purple;
            else if (ColorNumber == 5)
                currentColor = Color.Orange;
            else
                currentColor = Color.White;



        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            {
                spriteBatch.Draw(sprite, LocalPosition, null, currentColor, 0, origin, 1.0f, SpriteEffects.None, 0);
            }
        }

    }

}
