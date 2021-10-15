using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Tetris
{
    public class TetrisBlock
    {
        bool[,] blockShape;
        const int blockSize = 4;

        public TetrisBlock()
        {
            blockShape = new bool[blockSize, blockSize];
            for (int x = 0; x < blockSize; x++)
            {
                for (int y = 0; y < blockSize; y++)
                {
                    blockShape[x, y] = false;
                }
            }
        }

        protected virtual void RotateRight()
        { }

        protected virtual void RotateLeft()
        { }
    }
}
