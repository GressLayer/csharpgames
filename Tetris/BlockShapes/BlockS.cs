using Microsoft.Xna.Framework;
using System;

namespace Tetris
{
	class BlockS : TetrisBlock
	{
		public BlockS() : base()
		{
			blockShape = new bool[blockWidth, blockHeight]
			{
				{ false, true, false, false },
				{ false, true, true, false },
				{ false, false, true, false },
				{ false, false, false, false }
			};
			FillTiles();
		}

	}
}
