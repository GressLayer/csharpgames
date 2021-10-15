using Microsoft.Xna.Framework;
using System;

namespace Tetris
{
	public class BlockO : TetrisBlock
	{
		public BlockO()
		{
		}

		protected virtual void RotateRight()
		{ }

		protected virtual void RotateLeft()
		{ }
	}
}
