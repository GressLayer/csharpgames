using Microsoft.Xna.Framework;
using System;

namespace Tetris
{
	class BlockI : TetrisBlock
	{
		public BlockI() : base()
		{
			blockShape = new bool[blockWidth, blockHeight]
			{
				{ false, true, false, false },
				{ false, true, false, false },
				{ false, true, false, false },
				{ false, true, false, false }
			};
			FillTiles();
		}
	}
}
