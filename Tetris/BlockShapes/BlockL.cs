using Microsoft.Xna.Framework;
using System;

namespace Tetris
{
	public class BlockL : TetrisBlock
	{
		public BlockL() : base()
		{
            blockShape = new bool[blockWidth, blockHeight];
            for (int x = 0; x < blockWidth; x++)
            {
                for (int y = 0; y < blockHeight; y++)
                {
                    blockShape[x, y] = false;
                }
            }
        }

	}
}