using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tetris
{
    // A class for representing the Tetris playing grid.
    class TetrisGrid : GameObject
    {
        
        Texture2D empty; // The sprite of a single empty cell in the grid.

        Vector2 position;  // The position at which this TetrisGrid should be drawn.
        // The number of grid elements in the x- and y-directions
        public int Width { get { return 12; } }
        public int Height { get { return 20; } }

        // Creates a new TetrisGrid.
        public TetrisGrid()
        {
            /*empty = ExtendedGame.ContentManager.Load<Texture2D>("empty");*/
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

