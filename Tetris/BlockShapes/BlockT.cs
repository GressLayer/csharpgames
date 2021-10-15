using Microsoft.Xna.Framework;
using System;

namespace Tetris
{
	public class BlockT : TetrisBlock
	{
		public BlockT() : base()
		{
			blockShape = new bool[blockWidth, blockHeight];
		}

		protected virtual void RotateRight()
		{ }

		protected virtual void RotateLeft()
		{ }
	}
}
