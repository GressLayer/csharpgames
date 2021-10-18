using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Tetris
{
    // A class for representing the Tetris playing grid.
    class TetrisGrid : GameObject
    {

        Tile[,] grid;
        int Width, Height, cellSize;

        BlockObject testBlock, nextBlock, heldBlock, drawnBlock;

        // Creates a new TetrisGrid.
        public TetrisGrid(int width, int height, int cellSize, Vector2 offset)
        {
            Width = width;
            Height = height;
            this.cellSize = cellSize;
            LocalPosition = offset;

            Reset();

            testBlock = new BlockObject();
            testBlock.Parent = this;

            AddBlock();

        }

        public override void HandleInput(InputHelper inputHelper)
        {
            testBlock.HandleInput(inputHelper);

            // Advance the "block queue": first block gets replaced by the next block.
            if (inputHelper.KeyPressed(Keys.N))
            {
                NextBlock();
            }
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
                    grid[x, y] = new Tile(0);
                    grid[x, y].LocalPosition = LocalPosition + new Vector2(x * cellSize, y * cellSize);
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

