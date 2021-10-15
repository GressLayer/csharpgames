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
        // An enum for the different game states that the game can have.
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

        // 
        SpriteGameObject menu, menu2, menubar, menubarS, menubar2S, hud;

        Song welcome, controls, playing, gameover;

        public int GridWidth { get; private set; }
        public int GridHeight { get; private set; }
        public int CellSize { get; private set; }
        public Vector2 GridOffset { get; private set; }


        public GameWorld()
        {
            random = new Random();
            gameState = State.Welcome;

            MediaPlayer.IsRepeating = true;

            welcome = ExtendedGame.ContentManager.Load<Song>("music/welcome");
            controls = ExtendedGame.ContentManager.Load<Song>("music/controls");
            playing = ExtendedGame.ContentManager.Load<Song>("music/playing");
            gameover = ExtendedGame.ContentManager.Load<Song>("music/gameover");

            menu = new SpriteGameObject("sprites/menu");
            menu2 = new SpriteGameObject("sprites/menu2");
            menubar = new SpriteGameObject("sprites/menubar");
            menubarS = new SpriteGameObject("sprites/menubarS");
            menubar2S = new SpriteGameObject("sprites/menubar2S");
            hud = new SpriteGameObject("sprites/hud");

            font = ExtendedGame.ContentManager.Load<SpriteFont>("Font");

            GridWidth = 10; 
            GridHeight = 20; 
            CellSize = 32;
            GridOffset = new Vector2(96, 64);

            grid = new TetrisGrid(GridWidth, GridHeight, CellSize, GridOffset);

            MediaPlayer.Play(welcome);
        }

        public void HandleInput(InputHelper inputHelper)
        {
            // Developer quick-switch to test game states: comment out when no longer needed
            if (inputHelper.KeyPressed(Keys.D1))
                gameState = State.Welcome;
            if (inputHelper.KeyPressed(Keys.D2))
                gameState = State.Controls;
            if (inputHelper.KeyPressed(Keys.D3))
                gameState = State.Playing;
            if (inputHelper.KeyPressed(Keys.D4))
                gameState = State.GameOver;

            // Holds input options per game state

            switch (gameState)
            {
                case (State.Welcome):
                    if (inputHelper.KeyPressed(Keys.Space))
                    {
                        gameState = State.Controls;
                        MediaPlayer.Play(controls);
                    }
                    break;
                case (State.Controls):
                    if (inputHelper.KeyPressed(Keys.Back))
                    {
                        gameState = State.Welcome;
                        MediaPlayer.Play(welcome);
                    }
                    if (inputHelper.KeyPressed(Keys.Space))
                    {
                        gameState = State.Playing;
                        MediaPlayer.Play(playing);
                    }
                    break;
                case (State.Playing):
                    if (inputHelper.KeyPressed(Keys.R))
                    {
                        gameState = State.GameOver;
                        MediaPlayer.IsRepeating = false;
                        MediaPlayer.Play(gameover);
                    }
                    break;
                case (State.GameOver):
                    if (inputHelper.KeyPressed(Keys.Space))
                    {
                        gameState = State.Welcome;
                        MediaPlayer.IsRepeating = true;
                        MediaPlayer.Play(welcome);
                    }
                    break;
            }

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
                    spriteBatch.DrawString(font, "PRESS SPACE TO START", new Vector2(280, 480), Color.White, 0f, Vector2.Zero, 2.0f, SpriteEffects.None, 0f);
                    spriteBatch.DrawString(font, "Wassim Chammat    Corne van Vliet\n2981351                              6790836", new Vector2(6, 731), Color.White, 0f, Vector2.Zero, 0.8f, SpriteEffects.None, 0f);
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
