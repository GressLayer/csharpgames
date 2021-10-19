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
        int bottomRow = 19;

        Tile[,] grid;
        int Width, Height; 
        public int CellSize { get; private set; }

        static BlockObject currentBlock, nextBlock, heldBlock, blockBuffer;

        bool blockHeld;
        public static int score, level, blocksUsed, holdsUsed;

        // Creates a new TetrisGrid.
        public TetrisGrid(int width, int height, int cellSize, Vector2 offset)
        {
            Width = width;
            Height = height;
            this.CellSize = cellSize;
            LocalPosition = offset;

            level = 1;

            currentBlock = new BlockObject(ExtendedGame.Random.Next(7));
            currentBlock.Parent = this;

            // The block currently being held.
            heldBlock = new BlockObject(4);
            heldBlock.Parent = this;

            // Block buffer: used to swap the values of the current and held block when pressing the hold key.
            blockBuffer = new BlockObject(3);
            blockBuffer.Parent = this;

            Reset();
            AddBlock();

        }

        public override void HandleInput(InputHelper inputHelper)
        {
            currentBlock.HandleInput(inputHelper);

            if (inputHelper.KeyPressed(Keys.B))
                currentBlock.Reset();

            foreach (Tile tile in grid)
                tile.HandleInput(inputHelper);

            if (GameWorld.gameState == State.Playing)
            {
                // Advance the "block queue": first block gets replaced by the next block.
                // Developer input: comment out when finished.
                if (inputHelper.KeyPressed(Keys.N))
                {
                    NextBlock();
                }

                /* Hold button.
                 * Held block is saved in a buffer variable to be able to "swap" heldBlock and currentBlock's values.
                 */
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
                    else if (blockHeld == false)
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
                    {
                        if (grid[x, 19].BoundingBox.Intersects(currentBlock.BoundingBox))
                        {
                            grid[x, y].IsOccupied = true;
                            if (BlockObject.BlockType == 0)
                                grid[x, y].colorNumber = 0;
                            if (BlockObject.BlockType == 1)
                                grid[x, y].colorNumber = 1;
                            if (BlockObject.BlockType == 2)
                                grid[x, y].colorNumber = 2;
                            if (BlockObject.BlockType == 3)
                                grid[x, y].colorNumber = 3;
                            if (BlockObject.BlockType == 4)
                                grid[x, y].colorNumber = 4;
                            if (BlockObject.BlockType == 5)
                                grid[x, y].colorNumber = 5;
                            if (BlockObject.BlockType == 6)
                                grid[x, y].colorNumber = 6;
                        }
                    }
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

            if (blockHeld)
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
                    grid[x, y] = new Tile(false);
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
            nextBlock = new BlockObject(BlockObject.BlockType);
        }
    }
}

