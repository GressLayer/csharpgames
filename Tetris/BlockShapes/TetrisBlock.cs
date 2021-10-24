using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Tetris;

namespace Tetris
{
    abstract class TetrisBlock : GameObject
    { 

        public bool[,] blockShape { get; protected set; }
        protected const int blockWidth = 4;
        protected const int blockHeight = 4;
        public Point origin { get; protected set; }
        public int originX { get { return origin.X; } }
        public int originY { get { return origin.Y; } }

        public Tile[,] block { get; protected set; }

        public Color blockColor { get; protected set; }

        public TetrisBlock() : base()
        {
        }

        protected void FillTiles()
        {
            block = new Tile[blockWidth, blockHeight];

            for (int x = 0; x < blockWidth; x++)
            {
                for (int y = 0; y < blockHeight; y++)
                {
                    if (blockShape[x, y])
                    {
                        block[x, y] = new Tile();
                        block[x, y].IsOccupied = true;
                        block[x, y].GridPositionX = x;
                        block[x, y].GridPositionY = y;
                        block[x, y].LocalPosition = new Vector2(block[x,y].GridPositionX * 32, block[x,y].GridPositionY * 32);
                        block[x, y].Parent = this;
                    }
                }
            }
        }

        //Ik word helemaal gek van die rotation, ik krijg het maar niet aan de praat.
        public virtual void RotateRight()
        {

            foreach (Tile tile in block)
                if (tile != null)
                {
                    tile.GridPosition = new Point(3 - tile.GridPositionY, tile.GridPositionX);
                    tile.LocalPosition = new Vector2(tile.GridPositionX * 32, tile.GridPositionY * 32);
                }
            GameWorld.grid.OccupyRow();
        }

        public virtual void RotateLeft()
        {

            foreach (Tile tile in block)
                if (tile != null)
                {
                    tile.GridPosition = new Point(tile.GridPositionY, 3 - tile.GridPositionX);
                    tile.LocalPosition = new Vector2(tile.GridPositionX * 32, tile.GridPositionY * 32);
                }
            GameWorld.grid.OccupyRow();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (Tile tile in block)
            {
                if (tile != null)                   
                    tile.Update(gameTime);
            }
        }

        public override void Draw (GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (Tile tile in block)
            {
                if (tile != null)
                    tile.Draw(gameTime, spriteBatch);
            }
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            foreach (Tile tile in block)
            {
                if (tile != null && inputHelper.KeyPressed(Keys.Right) && GridPositionX < 10 - originX &&
                    GameWorld.grid.isRight == false)
                    tile.GridPositionX += 1;
                if (tile != null && inputHelper.KeyPressed(Keys.Left) && GridPositionX > -1 &&
                    GameWorld.grid.isLeft == false)
                    tile.GridPositionX -= 1;
                if (tile != null && inputHelper.KeyPressed(Keys.Up) && GridPositionY > 0)
                    tile.GridPositionY -= 1;
                if (tile != null && inputHelper.KeyPressed(Keys.Down) && GridPositionY < 20 - originY)
                {
                    tile.GridPositionY += 1;
                    //tile.GridPositionY += (20 - originY - GridPositionY);
                }
            }
        }
    }
}
