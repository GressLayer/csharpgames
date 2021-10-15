using Microsoft.Xna.Framework;
using System;

namespace Tetris
{
	public class BlockZ : TetrisBlock
	{
		public BlockZ() : base()
		{
			blockShape = new bool[blockWidth, blockHeight]
			{
				{ false, false, true, false },
				{ false, true, true, false },
				{ false, true, false, false },
				{ false, false, false, false }
			};
		}

	}
}
