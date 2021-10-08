using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tetris
{
    // A class for representing the Tetris playing grid.
    class TetrisGrid
    {
        // The sprite of a single empty cell in the grid.
        Texture2D empty;

        // The position at which this TetrisGrid should be drawn.
        Vector2 position;

        // The number of grid elements in the x-direction.
        public int Width { get { return 10; } }

        // The number of grid elements in the y-direction.
        public int Height { get { return 20; } }

        // Creates a new TetrisGrid.
        public TetrisGrid()
        {
            empty = TetrisGame.ContentManager.Load<Texture2D>("sprites/empty");
            position = Vector2.Zero;
            Clear();
        }

        // Draws the grid on the screen.
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
        }

        // Clears the grid.
        public void Clear()
        {
        }
    }
}

