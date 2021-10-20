using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

using Tetris;

namespace Tetris
{
    abstract class TetrisBlock : GameObject
    {
        protected bool[,] blockShape;
        protected const int blockWidth = 4;
        protected const int blockHeight = 4;

        protected Tile[,] block;

        public TetrisBlock()
        {
        }

        protected void FillTiles()
        {
            block = new Tile[blockWidth, blockHeight];

            for (int x = 0; x < blockWidth; x++)
            {
                for (int y = 0; y < blockHeight; y++)
                {
                    if (blockShape[x, y])
                    {
                        block[x, y] = new Tile();
                        block[x, y].IsOccupied = true;
                        block[x, y].LocalPosition = new Vector2(y * 32, x * 32);
                        block[x, y].Parent = this;
                    }
                }
            }
        }

        public void RotateRight()
        {
            bool[,] oldState = blockShape;
            Tile[,] oldBlock = block;
            for (int x = 0; x < blockWidth; x++)
            {
                for (int y = 0; y < blockHeight; y++)
                {
                    blockShape[x, y] = oldState[3 - y, x];
                    block[x, y] = oldBlock[3 - y, x];
                }
            }
        }

        public void RotateLeft()
        {
            bool[,] oldState = blockShape;
            Tile[,] oldBlock = block;
            for (int x = 0; x < blockWidth; x++)
            {
                for (int y = 0; y < blockHeight; y++)
                {
                    blockShape[x, y] = oldState[y, 3 - x];
                    block[x, y] = oldBlock[y, 3 - x];
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            foreach (Tile tile in block)
            {
                if (tile != null)
                    tile.Update(gameTime);
            }
        }

        public override void Draw (GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (Tile tile in block)
            {
                if (tile != null)
                    tile.Draw(gameTime, spriteBatch);
            }
        }
    }
}
