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

        static BlockObject currentBlock, nextBlock, heldBlock, blockBuffer;

        bool blockHeld;
        public static int score, level, blocksUsed, holdsUsed;

        int bottomRow = 19;

        // Creates a new TetrisGrid.
        public TetrisGrid(int width, int height, int cellSize, Vector2 offset)
        {
            Width = width;
            Height = height;
            this.CellSize = cellSize;
            LocalPosition = offset;

            currentBlock = new BlockObject(ExtendedGame.Random.Next(7));
            currentBlock.Parent = this;

            // The block currently being held.
            heldBlock = new BlockObject(4);
            heldBlock.Parent = this;

            // Block buffer: used to swap the values of the current and held block when pressing the hold key.
            blockBuffer = new BlockObject(4);
            blockBuffer.Parent = this;

            Reset();
            AddBlock();

        }

        public override void HandleInput(InputHelper inputHelper)
        {
            currentBlock.HandleInput(inputHelper);

            if (inputHelper.KeyPressed(Keys.B))
                currentBlock.Reset();

            // Advance the "block queue": first block gets replaced by the next block.
            if (inputHelper.KeyPressed(Keys.N))
            {
                NextBlock();
            }
            foreach (Tile tile in grid)
                tile.HandleInput(inputHelper);

            if (GameWorld.gameState == State.Playing)
            {

                if (inputHelper.KeyPressed(Keys.LeftShift) || inputHelper.KeyPressed(Keys.RightShift))
                {
                    if (blockHeld)
                    {
                        blockBuffer = heldBlock;
                        heldBlock = currentBlock;
                        currentBlock = blockBuffer;
                        heldBlock.LocalPosition = new Vector2(160, 32);

                        // Only tracks the amount of holds used after a hold has been created.
                        holdsUsed++;
                    }
                    else
                    {
                        heldBlock = currentBlock;
                        blockHeld = true;
                        heldBlock.LocalPosition = new Vector2(160, 32);
                        NextBlock();
                    }
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            currentBlock.Update(gameTime);

            foreach (Tile tile in grid)
                tile.Update(gameTime);

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    if (grid[x, y].BoundingBox.Intersects(currentBlock.BoundingBox))
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

            currentBlock.Draw(gameTime, spriteBatch);

            nextBlock.DrawNext(gameTime, spriteBatch);
            heldBlock.DrawHeld(gameTime, spriteBatch);
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

        public static void NextBlock()
        {
            currentBlock = nextBlock;
            AddBlock();
            score++;
            blocksUsed++;
        }

        static void AddBlock()
        {
            nextBlock = new BlockObject(ExtendedGame.Random.Next(7));
        }
    }
}

