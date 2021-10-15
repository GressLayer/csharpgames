using Microsoft.Xna.Framework;
using System;

namespace Tetris
{
	public class BlockL : TetrisBlock
	{
		public BlockL() : base()
		{
			blockShape = new bool[blockWidth, blockHeight]
			{
				{ false, true, false, false },
				{ false, true, false, false },
				{ false, true, true, false },
				{ false, false, false, false }
			};
		}

	}
}