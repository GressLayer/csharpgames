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

            //These instructions handle rotation of the blocks, clockwise, as well as counterclockwise.
            if (inputHelper.KeyPressed(Keys.A))
            {
                currentBlock.RotateLeft();
            }
            if (inputHelper.KeyPressed(Keys.D))
            {
                currentBlock.RotateRight();
            }

            //These instructions handle movement of the block over the grid and make sure the grid follows the presence of blocks
            //to allow them to lock in the grid when needed.
            if (inputHelper.KeyPressed(Keys.Right) && currentBlock.GridPositionX < 10 - currentBlock.originX)
            {
                currentBlock.GridPositionX += 1;
                OccupyUpdate();
            }
            if (inputHelper.KeyPressed(Keys.Left) && currentBlock.GridPositionX > -1)
            {
                currentBlock.GridPositionX -= 1;
                OccupyUpdate();
            }
            if (inputHelper.KeyPressed(Keys.Down) && currentBlock.GridPositionY < 20 - currentBlock.originY)
            {
                currentBlock.GridPositionY += 1;
                OccupyUpdate();
            }

            if (inputHelper.KeyPressed(Keys.Up) && currentBlock.GridPositionY > 0)
            {
                currentBlock.GridPositionY -= 1;
                OccupyUpdate();
            }

        }

        //This method updates the position of the blocks in the grid and the occupation of the grid tiles. 
        //It is also responsible for setting boundaries on the movement of blocks over the grid.
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

        //This method resets the grid for a new game. It differs from OccupyUpdate in that this method also resets
        //blocks that are 'locked' inside the grid.
        public void ResetGrid()
        {
            grid = new Tile[Width, Height];

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    grid[x, y] = new Tile();
                    grid[x, y].LocalPosition = LocalPosition + new Vector2(x * CellSize, y * CellSize);
                    grid[x, y].currentColor = Color.White;
                }
            }
        }

        //This method is responsible for generating a new random block. It is called when a block reaches the bottom of the screen
        //and is 'locked' in the grid.
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
            //REMOVE THIS LINE WHEN TESTING WITH LOCKING A BLOCK IS FINISHED!
            currentBlock.GridPositionX = 3;
            currentBlock.Parent = this;
            OccupyUpdate();

        }

       //This method is called after every movement of the currentblock and resets the occupation of the tiles in the grid.
       //After that, it recaculates which tiles are occupied.
       //This method does not ''reset' the grid, it is only used to follow blocks in the grid when they fall down and when they have
       //to lock in the grid.
        public void OccupyUpdate()
        {
            //This loop sets the occupation of all tiles to false.
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                            grid[x, y].IsOccupied = false;
                }
            }
            //This loop recalculates which tiles are occupied.
            foreach (Tile tile in currentBlock.block)
            {
                for (int x = 0; x < Width; x++)
                {
                    for (int y = 0; y < Height; y++)
                    {
                        if (tile != null)
                        {
                            if (tile.GridPosition == new Point(x, y))
                                grid[x, y].IsOccupied = true;
                        }
                    }
                }
            }
        }
    }
}
