using Microsoft.Xna.Framework;
using System;

namespace Tetris
{
	class BlockJ : TetrisBlock
	{
		public BlockJ() : base()
		{
			blockShape = new bool[blockWidth, blockHeight]
			{
				{ false, false, true, false },
				{ false, false, true, false },
				{ false, true, true, false },
				{ false, false, false, false }
			};
			FillTiles();
		}

	}
}
