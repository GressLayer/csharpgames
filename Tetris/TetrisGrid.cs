using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
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

        public static Song levelup, rowclear;

        /* currentBlock is the block currently being controlled.
         * nextBlock is the incoming block, controlled after currentBlock has been locked into the grid.
         * heldBlock is the block being held for later use, which can swap with currentBlock through the use of a third object, blockBuffer.
         */
        TetrisBlock currentBlock, nextBlock, heldBlock, blockBuffer;

        public static int score, level, blocksUsed, holdsUsed;

        public int timestep = 0;

        public bool isLeft { get; private set; } 
        public bool isRight { get; private set; }

        public TetrisGrid(int width, int height, int cellSize, Vector2 offset)
        {
            Width = width;
            Height = height;
            this.CellSize = cellSize;
            LocalPosition = offset;

            score = 0;
            blocksUsed = 0;
            holdsUsed = 0;

            // Level starts out at 1.
            level = 1;

            levelup = ExtendedGame.ContentManager.Load<Song>("music/levelup");
            rowclear = ExtendedGame.ContentManager.Load<Song>("music/rowclear");

            if (GameWorld.gameState == State.Playing)
                MediaPlayer.IsRepeating = false;

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

            if (inputHelper.KeyPressed(Keys.RightShift) || inputHelper.KeyPressed(Keys.LeftShift))
            {
                HoldBlock();
            }

        }

        //This method updates the position of the blocks in the grid and the occupation of the grid tiles. 
        //It is also responsible for setting boundaries on the movement of blocks over the grid.
        public override void Update(GameTime gameTime)
        {
            if (nextBlock != null)
                nextBlock.Update(gameTime);

            currentBlock.Update(gameTime);

            foreach (Tile tile in grid)
            {
                tile.Update(gameTime);
            }

            currentBlock.GridPosition = new Point(currentBlock.GridPositionX, currentBlock.GridPositionY);
            currentBlock.LocalPosition = new Vector2(currentBlock.GridPositionX * 32, currentBlock.GridPositionY * 32 + gameTime.ElapsedGameTime.Seconds);

            if (currentBlock.GridPositionY == 20 - currentBlock.originY)
            {
                ResetBlock();
                CheckFullRow();
            }

            foreach (Tile tile in currentBlock.block)

                if (tile != null && tile.GridPositionY + 1 < Height && grid[tile.GridPositionX, tile.GridPositionY + 1].IsLocked == true)
                {
                    tile.GridPositionX = currentBlock.GridPositionX;
                    tile.GridPositionY = currentBlock.GridPositionY;

                    OccupyRow();
                    CheckFullRow();
                    ResetBlock();
                }

            CheckRightCollision();
            CheckLeftCollision();

            if (GameWorld.gameState == State.Playing)
            {
                for (int x = 0; x < 10; x++)
                    if (grid[x, 0].IsLocked == true)
                        GameWorld.EndGame(true);
            }

            // 
            for (timestep = 0; timestep <= 10; timestep += (int)gameTime.ElapsedGameTime.TotalMilliseconds)
            {
                if (timestep == 10)
                {
                    currentBlock.GridPositionY += 1;
                    timestep = 0;
                }
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

            if (nextBlock != null)
                nextBlock.Draw(gameTime, spriteBatch);

            if (heldBlock != null)
                heldBlock.Draw(gameTime, spriteBatch);

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

        public void NextBlock()
        {
            if (nextBlock != null)
                currentBlock = nextBlock;
            currentBlock.Parent = this;

            int newBlock = ExtendedGame.Random.Next(7);

            switch (newBlock)
            {
                case (0): nextBlock = new BlockL(); break;
                case (1): nextBlock = new BlockJ(); break;
                case (2): nextBlock = new BlockS(); break;
                case (3): nextBlock = new BlockZ(); break;
                case (4): nextBlock = new BlockI(); break;
                case (5): nextBlock = new BlockO(); break;
                case (6): nextBlock = new BlockT(); break;
            }
            if (newBlock >= 7)
                NextBlock();

            nextBlock.LocalPosition = new Vector2(590, 192);
        }

        //This method is responsible for generating a new random block. It is called when a block reaches the bottom of the screen
        //and is 'locked' in the grid.
        public void ResetBlock()
        {
            if (GameWorld.gameState == State.Playing)
            {
                NextBlock();
                
                OccupyBottomRow();
                score++;
            }
        }

        public void HoldBlock()
        {
            if (GameWorld.gameState == State.Playing)
            {
                if (heldBlock != null)
                {
                    blockBuffer = heldBlock;
                    heldBlock = currentBlock;
                    currentBlock = blockBuffer;
                }
                else
                {
                    heldBlock = currentBlock;
                    ResetBlock();
                }
                currentBlock.GridPositionX = 0;
                currentBlock.GridPositionY = 0;
                currentBlock.Parent = this;

                heldBlock.LocalPosition = new Vector2(760, 128);
                holdsUsed++;
            }
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
        }

        public void OccupyRow()
        {
            foreach (Tile tile in currentBlock.block)
            {
                if (tile != null && tile.GridPositionY + 1 < Height && grid[tile.GridPositionX, tile.GridPositionY + 1].IsLocked == true )
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

        public void CheckFullRow()
        {
            int combo = 0;
            for (int y = 0; y < Height; y++)
                if (grid[0, y].IsLocked&& grid[1,y].IsLocked && grid[2, y].IsLocked && grid[3, y].IsLocked && grid[4, y].IsLocked &&
                    grid[5, y].IsLocked && grid[6, y].IsLocked && grid[7, y].IsLocked && grid[8, y].IsLocked && grid[9, y].IsLocked)
                {
                    combo++;
                    MoveRowDown(y);
                    MediaPlayer.Play(rowclear);
                }
            if (combo == 1)
                score += 100;
            if (combo == 2)
                score += 250;
            if (combo == 3)
                score += 800;
            if (combo >= 4)
                score += 5000;

            if (score % 20000 == 0)
            {
                level += score / 20000;
                MediaPlayer.Play(levelup);
            }
        }

        public void MoveRowDown(int row)
        {
            for (int x = 0; x < Width; x++)
            {
                grid[x, row].IsLocked = false;
                grid[x, row].IsOccupied = false;
            }

            for (int x = 0; x < Width; x++)
            {
                for (int y = row; y > 0; y--)
                {
                    grid[x, y].IsLocked = grid[x, y - 1].IsLocked;
                    grid[x, y].IsOccupied = grid[x, y - 1].IsOccupied;
                    grid[x, y].currentColor = grid[x, y - 1].currentColor;
                }
            }

            for (int x = 0; x < Width; x++)
            {
                grid[x, 0].IsOccupied = false;
                grid[x, 0].IsLocked = false;

            }
        }
    }
}
