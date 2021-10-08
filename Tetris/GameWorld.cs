using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
            GameOver
        }

        Texture2D menu, bg;

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

            menu = TetrisGame.ContentManager.Load<Texture2D>("sprites/menu");
            bg = TetrisGame.ContentManager.Load<Texture2D>("sprites/bg");
            font = TetrisGame.ContentManager.Load<SpriteFont>("Font");

            grid = new TetrisGrid();
        }

        public void HandleInput(GameTime gameTime, InputHelper inputHelper)
        {
            // Quick-switch to test game states: comment out when no longer needed
            if (inputHelper.KeyPressed(Keys.D1))
                gameState = State.Welcome;
            if (inputHelper.KeyPressed(Keys.D2))
                gameState = State.Controls;
            if (inputHelper.KeyPressed(Keys.D3))
                gameState = State.Playing;
            if (inputHelper.KeyPressed(Keys.D4))
                gameState = State.GameOver;
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            switch (gameState) 
            {
                case (State.Welcome):
                    spriteBatch.Draw(menu, Vector2.Zero, Color.White);
                    break;
                case (State.Controls):
                    spriteBatch.Draw(menu, Vector2.Zero, Color.Aquamarine);
                    break;
                case (State.Playing):
                    spriteBatch.Draw(bg, Vector2.Zero, Color.White);
                    grid.Draw(gameTime, spriteBatch);
                    break;
                case (State.GameOver):
                    spriteBatch.Draw(menu, Vector2.Zero, Color.PaleGoldenrod);
                    break;
            }

            spriteBatch.End();
        }

        public void Reset()
        {
        }

    }
}
