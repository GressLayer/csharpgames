using Microsoft.Xna.Framework;
using System;

namespace Tetris
{
	class BlockZ : TetrisBlock
	{
		public BlockZ() : base()
		{
			origin = new Vector2(96, 96);
			blockShape = new bool[blockWidth, blockHeight]
			{
				{ false, false, false, false },
				{ false, true, true, false },
				{ true, true, false, false },
				{ false, false, false, false }
			};
			FillTiles();
		}

	}
}
