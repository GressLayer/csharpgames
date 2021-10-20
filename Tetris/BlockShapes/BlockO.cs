using Microsoft.Xna.Framework;
using System;

namespace Tetris
{
	class BlockO : TetrisBlock
	{
		public BlockO() : base()
		{
			blockShape = new bool[blockWidth, blockHeight]
			{
				{ false, false, false, false },
				{ false, true, true, false },
				{ false, true, true, false },
				{ false, false, false, false }
			};

			FillTiles();
		}
	}
}
