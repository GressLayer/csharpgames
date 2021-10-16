﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Tetris
{
    // A class for representing the Tetris playing grid.
    class TetrisGrid : GameObject
    {

        Tile[,] grid;
        int Width, Height, cellSize;

        BlockObject testBlock;

        // Creates a new TetrisGrid.
        public TetrisGrid(int width, int height, int cellSize, Vector2 offset)
        {
            Width = width;
            Height = height;
            this.cellSize = cellSize;
            LocalPosition = offset;

            Reset();
            AddBlock();

        }

        public override void HandleInput(InputHelper inputHelper)
        {
            testBlock.HandleInput(inputHelper);
        }

        public override void Update(GameTime gameTime)
        {
            testBlock.Update(gameTime);
        }

        // Draws the grid on the screen.
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (Tile tile in grid)
                tile.Draw(gameTime, spriteBatch);
            testBlock.Draw(gameTime, spriteBatch);
        }

        // Clears the grid.
        public override void Reset()
        {
            grid = new Tile[Width, Height];

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    grid[x, y] = new Tile(0);
                    grid[x, y].LocalPosition = LocalPosition + new Vector2(x * cellSize, y * cellSize);
                }
            }
        }

        void AddBlock()
        {
            testBlock = new BlockObject();
            testBlock.Parent = this;
        }
        

    }
}

