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
            //currentBlock = new BlockI ();
            //currentBlock.Parent = this;

            // The block currently being held.
            heldBlock = new BlockObject();
            heldBlock.Parent = this;

            // Block buffer: used to swap the values of the current and held block when pressing the hold key.
            blockBuffer = new BlockObject();
            blockBuffer.Parent = this;

            ResetGrid();
            ResetBlock();
        }

        //Handles player input of objects in the grid
        public override void HandleInput(InputHelper inputHelper)
        {
            currentBlock.HandleInput(inputHelper);

            //Handles rotation of a block in the grid
            if (inputHelper.KeyPressed(Keys.A))
            {
                currentBlock.RotateLeft();
            }
            if (inputHelper.KeyPressed(Keys.D))
            {
                currentBlock.RotateRight();
            }

            //Handles movement of the block over the grid
            if (currentBlock.GridPositionX < 10 - currentBlock.originX)
                if (inputHelper.KeyPressed(Keys.Right))
                    currentBlock.GridPositionX += 1;
            if (currentBlock.GridPositionX > -1)
                if (inputHelper.KeyPressed(Keys.Left))
                    currentBlock.GridPositionX -= 1;
            if (inputHelper.KeyPressed(Keys.Down))
                currentBlock.GridPositionY = 20 - currentBlock.originY;

        }

        public override void Update(GameTime gameTime)
        {

            foreach (Tile tile in grid)
            {
                tile.Update(gameTime);
            }

            currentBlock.Update(gameTime);

            currentBlock.GridPosition = new Point(currentBlock.GridPositionX, currentBlock.GridPositionY);
            currentBlock.LocalPosition = new Vector2(currentBlock.GridPositionX * 32, currentBlock.GridPositionY * 32);

            if (currentBlock.GridPositionX <= -1)
                currentBlock.GridPositionX = -1;
            if (currentBlock.GridPositionX >= 10 - currentBlock.originX)
                currentBlock.GridPositionX = 10 - currentBlock.originX;
            if (currentBlock.GridPositionY >= 20 - currentBlock.originY)
                currentBlock.GridPositionY = 20 - currentBlock.originY;

            if (currentBlock.GridPositionY == 20 - currentBlock.originY)
                ResetBlock();
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

        //Sets a new empty grid
        public void ResetGrid()
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

        public void ResetBlock()
        {
            int newBlock = ExtendedGame.Random.Next(7);

            if (newBlock == 0)
                currentBlock = new BlockI();
            else if (newBlock == 1)
                currentBlock = new BlockL();
            else if (newBlock == 2)
                currentBlock = new BlockJ();
            else if (newBlock == 3)
                currentBlock = new BlockO();
            else if (newBlock == 4)
                currentBlock = new BlockS();
            else if (newBlock == 5)
                currentBlock = new BlockZ();
            else
                currentBlock = new BlockT();
            currentBlock.Parent = this;

        }
    }
}
