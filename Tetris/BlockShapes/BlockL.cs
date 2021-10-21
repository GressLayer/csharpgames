using Microsoft.Xna.Framework;
using System;

namespace Tetris
{
	class BlockL : TetrisBlock
	{
		public BlockL() : base()
		{
			origin = new Point(3, 3);
			blockShape = new bool[blockWidth, blockHeight]
			{
				{ false, false, false, false },
				{ true, true, true, false },
				{ false, false, true, false },
				{ false, false, false, false }
			};
			FillTiles();
		}

	}
}