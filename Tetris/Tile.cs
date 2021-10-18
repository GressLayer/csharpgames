using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Tetris
{
    class Tile : SpriteGameObject
    {
        public int colorNumber { get; private set; }
        Color currentColor;

        bool isOccupied = false;
        static string textureBase = "sprites/empty";

        public Tile(int color) : base(textureBase)
        {
            if (isOccupied)
                textureBase = "sprites/tetromino";
            else
                textureBase = "sprites/empty";

            colorNumber = color;

            if (isOccupied)
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
