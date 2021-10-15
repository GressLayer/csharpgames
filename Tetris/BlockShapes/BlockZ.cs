using Microsoft.Xna.Framework;
using System;

namespace Tetris
{
	public class BlockZ : TetrisBlock
	{
		public BlockZ() : base()
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
