using Microsoft.Xna.Framework;
using System;

namespace Tetris
{
	class BlockI : TetrisBlock
	{
		public BlockI() : base()
		{
			origin = new Point(2, 4);

			blockShape = new bool[blockWidth, blockHeight]
			{
				{ false, false, false, false },
				{ true, true, true, true },
				{ false, false, false, false },
				{ false, false, false, false }
 			};
			FillTiles();
		}
    }
}
