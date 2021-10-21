using Microsoft.Xna.Framework;
using System;

namespace Tetris
{
	class BlockJ : TetrisBlock
	{
		public BlockJ() : base()
		{
			origin = new Point(3, 3);

		blockShape = new bool[blockWidth, blockHeight]
			{
				{ false, false, false, false },
				{ false, false, true, false },
				{ true, true, true, false },
				{ false, false, false, false }
			};
			FillTiles();

		}

	}
}
