using Microsoft.Xna.Framework;
using System;

namespace Tetris
{
	class BlockT : TetrisBlock
	{
		public BlockT() : base()
		{
			blockShape = new bool[blockWidth, blockHeight]
			{
				{ false, true, false, false },
				{ false, true, true, false },
				{ false, true, false, false },
				{ false, false, false, false }
			};
		}
	}
}
