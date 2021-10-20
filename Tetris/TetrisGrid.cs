using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

using Tetris;

namespace Tetris
{
    // A class for representing the Tetris playing grid.
    class TetrisGrid : GameObject
    {

        Tile[,] grid;
        int Width, Height;
        public int CellSize { get; private set; }

        TetrisBlock currentBlock;
        static BlockObject nextBlock, heldBlock, blockBuffer;

        // bool blockHeld;
        public static int score, level, blocksUsed, holdsUsed;

        public TetrisGrid(int width, int height, int cellSize, Vector2 offset)
        {
            Width = width;
            Height = height;
            this.CellSize = cellSize;
            LocalPosition = offset;

            // Level starts out at 1.
            level = 1;

            // The block currently being dropped and controlled.
            currentBlock = new BlockI();
            currentBlock.Parent = this;

            // The block currently being held.
            heldBlock = new BlockObject();
            heldBlock.Parent = this;

            // Block buffer: used to swap the values of the current and held block when pressing the hold key.
            blockBuffer = new BlockObject();
            blockBuffer.Parent = this;

            Reset();
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            currentBlock.HandleInput(inputHelper);

            if (inputHelper.KeyPressed(Keys.A))
            {
                currentBlock.RotateLeft();
            }
            if (inputHelper.KeyPressed(Keys.D))
            {
                currentBlock.RotateRight();
            }
        }

        public override void Reset()
        {
            grid = new Tile[Width, Height];

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    grid[x, y] = new Tile();
                    grid[x, y].LocalPosition = LocalPosition + new Vector2(x * CellSize, y * CellSize);
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            currentBlock.Update(gameTime);

            foreach (Tile tile in grid)
            {
                tile.Update(gameTime);
            } 
        }

        // Draws the grid on the screen.
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (Tile tile in grid)
            {
                    tile.Draw(gameTime, spriteBatch);
            }

            currentBlock.Draw(gameTime, spriteBatch);

            //nextBlock.DrawNext(gameTime, spriteBatch);

            //if (blockHeld)
            //heldBlock.DrawHeld(gameTime, spriteBatch);
        }
    }
}
