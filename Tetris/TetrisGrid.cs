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

        TetrisBlock currentBlock, nextBlock;
        static BlockObject heldBlock, blockBuffer;

        // bool blockHeld;
        public static int score, level, blocksUsed, holdsUsed;

        public bool isLeft { get; private set; } 
        public bool isRight { get; private set; }

        public TetrisGrid(int width, int height, int cellSize, Vector2 offset)
        {
            Width = width;
            Height = height;
            this.CellSize = cellSize;
            LocalPosition = offset;

            // Level starts out at 1.
            level = 1;



            // The block currently being held.
            heldBlock = new BlockObject();
            heldBlock.Parent = this;

            // Block buffer: used to swap the values of the current and held block when pressing the hold key.
            blockBuffer = new BlockObject();
            blockBuffer.Parent = this;
            
            ResetGrid();
            StartBlock();
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
            if (inputHelper.KeyPressed(Keys.Right) && currentBlock.GridPositionX < 10 - currentBlock.originX && isRight == false)
            {
                currentBlock.GridPositionX += 1;
                OccupyBottomRow();
            }
            if (inputHelper.KeyPressed(Keys.Left) && currentBlock.GridPositionX > -1 && isLeft == false)
            {
                currentBlock.GridPositionX -= 1;
                OccupyBottomRow();
            }
            if (inputHelper.KeyPressed(Keys.Down) && (currentBlock.GridPositionY < 20 - currentBlock.originY))
            {
                //currentBlock.GridPositionY = 20 - currentBlock.originY;
                currentBlock.GridPositionY += 1;
                OccupyBottomRow();
            }

            if (inputHelper.KeyPressed(Keys.Up) && currentBlock.GridPositionY > 0)
            {
                currentBlock.GridPositionY -= 1;
                OccupyBottomRow();
            }

        }

        //This method updates the position of the blocks in the grid and the occupation of the grid tiles. 
        //It is also responsible for setting boundaries on the movement of blocks over the grid.
        public override void Update(GameTime gameTime)
        {
            

            currentBlock.Update(gameTime);

            foreach (Tile tile in grid)
            {
                tile.Update(gameTime);
            }

            currentBlock.GridPosition = new Point(currentBlock.GridPositionX, currentBlock.GridPositionY);
            currentBlock.LocalPosition = new Vector2(currentBlock.GridPositionX * 32, currentBlock.GridPositionY * 32);

            if (currentBlock.GridPositionY == 20 - currentBlock.originY)
                ResetBlock();

            foreach (Tile tile in currentBlock.block)

                if (tile != null && grid[tile.GridPositionX, tile.GridPositionY + 1].IsLocked == true)
                {
                    OccupyRow();
                    ResetBlock();
                }
            CheckRightCollision();
            CheckLeftCollision();
        }

        // Draws the grid on the screen.
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (Tile tile in grid)
            {
                tile.Draw(gameTime, spriteBatch);
            }

            currentBlock.Draw(gameTime, spriteBatch);

            nextBlock.Draw(gameTime, spriteBatch);

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

        public void StartBlock()
        {
            int startBlock = ExtendedGame.Random.Next(7);
            if (startBlock == 0)
                currentBlock = new BlockI();
            else if (startBlock == 1)
                currentBlock = new BlockL();
            else if (startBlock == 2)
                currentBlock = new BlockJ();
            else if (startBlock == 3)
                currentBlock = new BlockO();
            else if (startBlock == 4)
                currentBlock = new BlockS();
            else if (startBlock == 5)
                currentBlock = new BlockZ();
            else
                currentBlock = new BlockT();

            currentBlock.Parent = this;
        }

        //This method is responsible for generating a new random block. It is called when a block reaches the bottom of the screen
        //and is 'locked' in the grid.
        public void ResetBlock()
        {
            if (nextBlock != null)
                currentBlock = nextBlock;
            currentBlock.Parent = this;

            int newBlock = ExtendedGame.Random.Next(7);
            if (newBlock == 0)
                nextBlock = new BlockI();
            else if (newBlock == 1)
                nextBlock = new BlockL();
            else if (newBlock == 2)
                nextBlock = new BlockJ();
            else if (newBlock == 3)
                nextBlock = new BlockO();
            else if (newBlock == 4)
                nextBlock = new BlockS();
            else if (newBlock == 5)
                nextBlock = new BlockZ();
            else
                nextBlock = new BlockT();

            nextBlock.LocalPosition = new Vector2(590, 200);
            OccupyBottomRow();
        }

        //This method is called after every movement of the currentblock and resets the occupation of the tiles in the grid.
        //After that, it recaculates which tiles are occupied.
        //This method does not ''reset' the grid, it is only used to follow blocks in the grid when they fall down and when they have
        //to lock in the grid.
        public void OccupyBottomRow()
        {
            //This loop sets the occupation of all tiles to false.
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    if (grid[x, y].IsLocked == false)
                    {
                        grid[x, y].IsOccupied = false;
                        grid[x, y].currentColor = Color.White;
                    }
                    else
                        continue;
                }
            }
            //This loop recalculates which tiles are occupied.
            foreach (Tile tile in currentBlock.block)
            {
                for (int x = 0; x < Width; x++)
                {
                    for (int y = 0; y < Height; y++)
                    {
                        if (tile != null && tile.GridPosition == new Point(x, y))
                        {
                            grid[x, y].IsOccupied = true;
                            if (currentBlock.GridPositionY == 20 - currentBlock.originY)
                            //ADD A HELPER BOOL THAT CHECKS WHETER THE CURRENTBLOCK IS ORIGINY DISTANCE FROM A LOCKED BLOCK)*/
                            {
                                grid[x, y].currentColor = currentBlock.blockColor;
                                grid[x, y].IsLocked = true;
                            }
                        }
                    }
                }

            }
            //DIT MAG WAARSCHIJNLIJK VERWIJDERD WORDEN, MAAR MOET NOG EVEN EXPERIMENTEREN.
        }

        public void OccupyRow()
        {
            foreach (Tile tile in currentBlock.block)
            {
                if (tile != null && grid[tile.GridPositionX, tile.GridPositionY + 1].IsLocked == true)
                    for (int x = 0; x < Width; x++)
                    {
                        for (int y = 0; y < Height; y++)
                        {
                            if (grid[x, y].IsOccupied == true && grid[x, y].IsLocked == false)
                            {
                                grid[x, y].currentColor = currentBlock.blockColor;
                                grid[x, y].IsLocked = true;
                            }
                        }
                    }
            }
        }

        public void CheckLeftCollision()
        {
            foreach (Tile tile in currentBlock.block)
                if (tile != null && tile.GridPositionX - 1 > 0 &&
                     grid[tile.GridPositionX - 1, tile.GridPositionY].IsLocked == true)
                {
                    isLeft = true;
                    break;
                }
                else 
                isLeft = false;
        }

        public void CheckRightCollision()
        {
            foreach (Tile tile in currentBlock.block)
                if (tile != null && tile.GridPositionX + 1 < Width - 1 &&
                    grid[tile.GridPositionX + 1, tile.GridPositionY].IsLocked == true)
                {
                    isRight = true;
                    break;
                }
                else                   
                    isRight = false;
        }
    }
}
