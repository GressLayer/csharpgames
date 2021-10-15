using Microsoft.Xna.Framework;
using System;

namespace Tetris
{
	public class BlockT : TetrisBlock
	{
		public BlockT()
		{
		}

		protected virtual void RotateRight()
		{ }

		protected virtual void RotateLeft()
		{ }
	}
}
