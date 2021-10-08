using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Tetris
{
    // A class for representing the game world.
    // This contains the grid, the falling block, and everything else that the player can see/do.

    class GameWorld
    {
        /// An enum for the different game states that the game can have.
        enum State
        {
            Welcome,
            Controls,
            Playing,
            Paused,
            GameOver
        }

        Texture2D bg;

        // The random-number generator of the game.
        public static Random Random { get { return random; } }
        static Random random;

        // The main font of the game.
        SpriteFont font;

        // The current game state.
        State gameState;

        // The main grid of the game.
        TetrisGrid grid;

        public GameWorld()
        {
            random = new Random();
            gameState = State.Playing;

            bg = TetrisGame.ContentManager.Load<Texture2D>("sprites/bg");
            font = TetrisGame.ContentManager.Load<SpriteFont>("Font");

            grid = new TetrisGrid();
        }

        public void HandleInput(GameTime gameTime, InputHelper inputHelper)
        {
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            grid.Draw(gameTime, spriteBatch);
            spriteBatch.Draw(bg, Vector2.Zero, Color.White);
            spriteBatch.DrawString(font, "Hello!", Vector2.Zero, Color.Blue);
            spriteBatch.End();
        }

        public void Reset()
        {
        }

    }
}
