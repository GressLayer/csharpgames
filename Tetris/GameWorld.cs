using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
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

        Texture2D menu, menu2, menubar, menubarS, menubar2S, hud;
        Song welcome, controls, playing, gameover;

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
            gameState = State.Welcome;

            MediaPlayer.IsRepeating = true;

            menu = TetrisGame.ContentManager.Load<Texture2D>("sprites/menu");
            menu2 = TetrisGame.ContentManager.Load<Texture2D>("sprites/menu2");
            menubar = TetrisGame.ContentManager.Load<Texture2D>("sprites/menubar");
            menubarS = TetrisGame.ContentManager.Load<Texture2D>("sprites/menubarS");
            menubar2S = TetrisGame.ContentManager.Load<Texture2D>("sprites/menubar2S");
            hud = TetrisGame.ContentManager.Load<Texture2D>("sprites/hud");

            font = TetrisGame.ContentManager.Load<SpriteFont>("Font");

            welcome = TetrisGame.ContentManager.Load<Song>("music/welcome");
            controls = TetrisGame.ContentManager.Load<Song>("music/controls");
            playing = TetrisGame.ContentManager.Load<Song>("music/playing");
            gameover = TetrisGame.ContentManager.Load<Song>("music/gameover");

            grid = new TetrisGrid();

            MediaPlayer.Play(controls);
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
                    spriteBatch.Draw(menubar, Vector2.Zero, Color.White);
                    break;
                case (State.Controls):
                    spriteBatch.Draw(menu, Vector2.Zero, Color.PaleGoldenrod);
                    spriteBatch.Draw(menubarS, Vector2.Zero, Color.PaleGoldenrod);
                    spriteBatch.DrawString(font, "CONTROLS", new Vector2(750, 112), Color.White, 0f, Vector2.Zero, 2.0f, SpriteEffects.None, 0f);
                    break;
                case (State.Playing):
                    spriteBatch.Draw(menu2, Vector2.Zero, Color.White);
                    spriteBatch.Draw(hud, Vector2.Zero, Color.White);
                    grid.Draw(gameTime, spriteBatch);
                    break;
                case (State.GameOver):
                    spriteBatch.Draw(menu2, Vector2.Zero, Color.DarkOrange);
                    spriteBatch.Draw(menubar2S, Vector2.Zero, Color.DarkOrange);
                    spriteBatch.DrawString(font, "GAME OVER", new Vector2(750, 112), Color.White, 0f, Vector2.Zero, 2.0f, SpriteEffects.None, 0f);
                    break;
            }

            spriteBatch.End();
        }

        public void Reset()
        {
        }

    }
}
