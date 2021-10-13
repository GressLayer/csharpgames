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

        // The random-number generator of the game.
        public static Random Random { get { return random; } }
        static Random random;

        // The main font of the game.
        SpriteFont font;

        // The current game state.
        State gameState;

        // The main grid of the game.
        TetrisGrid grid;

        SpriteGameObject menu, menu2, menubar, menubarS, menubar2S, hud;
        public GameWorld()
        {
            random = new Random();
            gameState = State.Welcome;

            SpriteGameObject menu = new SpriteGameObject("sprites/menu");
            SpriteGameObject menu2 = new SpriteGameObject("sprites/menu2");
            SpriteGameObject menubar = new SpriteGameObject("sprites/menubar");
            SpriteGameObject menubarS = new SpriteGameObject("sprites/menubarS");
            SpriteGameObject menubar2S = new SpriteGameObject("sprites/menubar2S");
            SpriteGameObject hud = new SpriteGameObject("sprites/hud");
            SpriteFont font = ExtendedGame.ContentManager.Load<SpriteFont>("Font");
            grid = new TetrisGrid();
        }

        public void HandleInput(InputHelper inputHelper)
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
            switch (gameState) 
            {
                case (State.Welcome):
                    menu.Draw(gameTime, spriteBatch);
                    menubar.Draw(gameTime, spriteBatch);
                    break;
                case (State.Controls):
                    menu.Draw(gameTime, spriteBatch);
                    menubarS.Draw(gameTime, spriteBatch);
                    spriteBatch.DrawString(font, "CONTROLS", new Vector2(750, 112), Color.White, 0f, Vector2.Zero, 2.0f, SpriteEffects.None, 0f);
                    break;
                case (State.Playing):
                    menu2.Draw(gameTime, spriteBatch);
                    hud.Draw(gameTime, spriteBatch);
                    grid.Draw(gameTime, spriteBatch);
                    break;
                case (State.GameOver):
                    menu2.Draw(gameTime, spriteBatch);
                    menubar2S.Draw(gameTime, spriteBatch);
                    spriteBatch.DrawString(font, "GAME OVER", new Vector2(750, 112), Color.White, 0f, Vector2.Zero, 2.0f, SpriteEffects.None, 0f);
                    break;
            }
        }

        public void Reset()
        {
        }

    }
}
