using Microsoft.Xna.Framework;
using System;

namespace Tetris
{
	public class BlockL : TetrisBlock
	{
		public BlockL()
		{
		}
		protected virtual void RotateRight()
		{ }

		protected virtual void RotateLeft()
		{ }
	}
}