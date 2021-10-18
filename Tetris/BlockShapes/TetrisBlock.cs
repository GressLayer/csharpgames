using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Tetris
{
    abstract class TetrisBlock : GameObject
    {
        protected bool[,] blockShape;
        protected const int blockWidth = 4;
        protected const int blockHeight = 4;

        protected Tile[,] block;

        public TetrisBlock()
        {
            blockShape = new bool[blockWidth, blockHeight];

        }

        protected void RotateRight()
        {
            bool[,] oldState = blockShape;
            for (int x = 0; x < blockWidth; x++)
            {
                for (int y = 0; y < blockHeight; y++)
                {
                    blockShape[x, y] = oldState[3 - y, x];
                }
            }
        }

        protected void RotateLeft()
        {
            bool[,] oldState = blockShape;
            for(int x = 0; x < blockWidth; x++)
            {
                for (int y = 0; y < blockHeight; y++)
                {
                    blockShape[x, y] = oldState[y, 3 - x];
                }
            }
        }
    }
}
