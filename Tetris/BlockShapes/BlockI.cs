using Microsoft.Xna.Framework;
using System;

namespace Tetris
{
	public class BlockI : TetrisBlock
	{
		public BlockI()
		{
		}

		protected virtual void RotateRight()
		{ }

		protected virtual void RotateLeft()
		{ }
	}
}
