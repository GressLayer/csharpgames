using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Tetris
{
    // A class for representing the Tetris playing grid.
    class TetrisGrid : GameObject
    {

        Tile[,] grid;
        int Width, Height; 
        public int CellSize { get; private set; }

        BlockObject testBlock, nextBlock, heldBlock, drawnBlock;

        int bottomRow = 19;

        // Creates a new TetrisGrid.
        public TetrisGrid(int width, int height, int cellSize, Vector2 offset)
        {
            Width = width;
            Height = height;
            this.CellSize = cellSize;
            LocalPosition = offset;

            testBlock = new BlockObject();

            Reset();

            testBlock = new BlockObject();
            testBlock.Parent = this;

            AddBlock();

        }

        public override void HandleInput(InputHelper inputHelper)
        {
            testBlock.HandleInput(inputHelper);

            if (inputHelper.KeyPressed(Keys.B))
                testBlock.Reset();

            // Advance the "block queue": first block gets replaced by the next block.
            if (inputHelper.KeyPressed(Keys.N))
            {
                NextBlock();
            }
            foreach (Tile tile in grid)
                tile.HandleInput(inputHelper);
            if (inputHelper.KeyPressed(Keys.LeftShift) || inputHelper.KeyPressed(Keys.RightShift))
            {
                heldBlock = testBlock;
                testBlock = heldBlock;
                nextBlock = testBlock;
                Reset();
                AddBlock();
            }
        }

        public override void Update(GameTime gameTime)
        {
            testBlock.Update(gameTime);

            foreach (Tile tile in grid)
                tile.Update(gameTime);

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    if (grid[x, y].BoundingBox.Intersects(testBlock.BoundingBox))
                        grid[x, y].IsOccupied = true;
                    else
                        grid[x, y].IsOccupied = false;
                }
            }

        }

        // Draws the grid on the screen.
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (Tile tile in grid)
                tile.Draw(gameTime, spriteBatch);
            testBlock.Draw(gameTime, spriteBatch);
            nextBlock.DrawNext(gameTime, spriteBatch);


        }

        // Clears the grid.
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

        void NextBlock()
        {
            testBlock = nextBlock;
            Reset();
            AddBlock();
        }

        void AddBlock()
        {
            nextBlock = new BlockObject();
            nextBlock.Parent = this;
        }
        

    }
}

