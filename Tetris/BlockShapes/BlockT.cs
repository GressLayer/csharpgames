using Microsoft.Xna.Framework;
using System;

namespace Tetris
{
	class BlockT : TetrisBlock
	{
		public BlockT() : base()
		{
			blockColor = Color.Magenta;
			origin = new Point(3, 3);
			blockShape = new bool[blockWidth, blockHeight]
			{
				{ false, false, false, false },
				{ true, true, true, false },
				{ false, true, false, false },
				{ false, false, false, false }
			};
			FillTiles();
			for (int x = 0; x < blockWidth; x++)
			{
				for (int y = 0; y < blockHeight; y++)
				{
					if (block[x, y] != null)
						block[x, y].currentColor = blockColor;
				}
			}
		}
	}
}
