using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

using Tetris;

namespace Tetris
{
    abstract class TetrisBlock : GameObject
    {
        protected bool[,] blockShape;
        protected const int blockWidth = 4;
        protected const int blockHeight = 4;
        public Vector2 origin { get; protected set; }
        public float originX { get { return origin.X; } }
        public float originY { get { return origin.Y; } }

        protected Tile[,] block;

        public TetrisBlock()
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
                        block[x, y].LocalPosition = new Vector2(x * 32, y * 32);
                        block[x, y].Parent = this;
                    }
                }
            }
        }

        //Ik word helemaal gek van die rotation, ik krijg het maar niet aan de praat.
        public virtual void RotateRight()
        {
            bool[,] oldState = blockShape;
            Tile[,] oldBlock = block;
            for (int x = 0; x < blockWidth; x++)
            {
                for (int y = 0; y < blockHeight; y++)
                {
                    blockShape[x, y] = oldState[y, 3 - x];
                    if (block[x, y] != null)
                    {
                        block[x, y].LocalPosition = new Vector2(3 - y * 32, x * 32);
                    }
                    //if (block[x, y] != null)

                    //block[x, y].LocalPosition = new Vector2((3 - y) * 32, x * 32);

                }
            }
            for (int x = 0; x < blockWidth; x++)
            {
                for (int y = 0; y < blockHeight; y++)
                {
                    /*if (block[x, y] != null)
                        block[x, y].LocalPosition = new Vector2(x * 32, y * 32);*/
                }
            }
        }

        public virtual void RotateLeft()
        {
            bool[,] oldState = blockShape;
            Tile[,] oldBlock = block;
            for (int x = 0; x < blockWidth; x++)
            {
                for (int y = 0; y < blockHeight; y++)
                {
                    blockShape[x, y] = oldState[y, 3 - x];

                    if (block[x, y] != null)
                        block[x, y].LocalPosition = new Vector2(y * 32, (3 - x) * 32);
                }
            }
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
    }
}
