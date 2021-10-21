using Microsoft.Xna.Framework;
using System;

namespace Tetris
{
	class BlockT : TetrisBlock
	{
		public BlockT() : base()
		{
			origin = new Vector2(96, 96);
			blockShape = new bool[blockWidth, blockHeight]
			{
				{ false, false, false, false },
				{ true, true, true, false },
				{ false, true, false, false },
				{ false, false, false, false }
			};
			FillTiles();
		}
	}
}
