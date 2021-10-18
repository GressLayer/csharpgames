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

        int score;
        string blank = "            ";

        // The main font of the game.
        SpriteFont font;

        // The current game state.
        State gameState;

        // The main grid of the game.
        public static TetrisGrid grid { get; private set; }

        Texture2D menubar;
        SpriteGameObject menu, menu2, menubarS, menubar2S, hud;

        Song welcome, controls, playing, gameover;

        public int GridWidth { get; private set; }
        public int GridHeight { get; private set; }
        public int CellSize { get; private set; }
        public Vector2 GridOffset { get; private set; }


        public GameWorld()
        {
            score = 0;
            gameState = State.Welcome;

            MediaPlayer.IsRepeating = true;

            menubar = ExtendedGame.ContentManager.Load<Texture2D>("sprites/menubar");

            welcome = ExtendedGame.ContentManager.Load<Song>("music/welcome");
            controls = ExtendedGame.ContentManager.Load<Song>("music/controls");
            playing = ExtendedGame.ContentManager.Load<Song>("music/playing");
            gameover = ExtendedGame.ContentManager.Load<Song>("music/gameover");

            menu = new SpriteGameObject("sprites/menu");
            menu2 = new SpriteGameObject("sprites/menu2");
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

            /* if (inputHelper.KeyPressed(Keys.D1))
                gameState = State.Welcome;
            if (inputHelper.KeyPressed(Keys.D2))
                gameState = State.Controls;
            if (inputHelper.KeyPressed(Keys.D3))
                gameState = State.Playing;
            if (inputHelper.KeyPressed(Keys.D4))
                gameState = State.GameOver; */

            // Holds input options per game state

            grid.HandleInput(inputHelper);
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
            grid.Update(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            switch (gameState) 
            {
                case (State.Welcome):
                    menu.Draw(gameTime, spriteBatch);
                    spriteBatch.Draw(menubar, new Vector2(0, 96), Color.White);
                    spriteBatch.DrawString(font, "PRESS SPACE TO START", new Vector2(280, 480), Color.White, 0f, Vector2.Zero, 2.0f, SpriteEffects.None, 0f);
                    break;
                case (State.Controls):
                    menu.Draw(gameTime, spriteBatch);
                    menubarS.Draw(gameTime, spriteBatch);
                    spriteBatch.DrawString(font, "CONTROLS", new Vector2(750, 112), Color.White, 0f, Vector2.Zero, 2.0f, SpriteEffects.None, 0f);
                    spriteBatch.DrawString(font, "PRESS SPACE TO PLAY", new Vector2(292, 660), Color.White, 0f, Vector2.Zero, 2.0f, SpriteEffects.None, 0f);
                    spriteBatch.DrawString(font, "or press BACKSPACE to return", new Vector2(384, 710), Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);

                    spriteBatch.DrawString(font, "MOVEMENT", new Vector2(64, 82), Color.White, 0f, Vector2.Zero, 1.6f, SpriteEffects.None, 0f);
                    spriteBatch.DrawString(font, "LEFT/RIGHT to move the Tetromino.\nSPACE to rotate.", new Vector2(64, 122), Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
                    spriteBatch.DrawString(font, "DROP", new Vector2(64, 202), Color.White, 0f, Vector2.Zero, 1.6f, SpriteEffects.None, 0f);
                    spriteBatch.DrawString(font, "The Tetromino falls on its own: press DOWN to drop faster!\nPress UP for a \"Hard Drop\", to instantly place the block!\nPressing SHIFT lets you \"hold\" a block, to store it for later use.", new Vector2(64, 242), Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);

                    spriteBatch.DrawString(font, "THE BASICS OF TETRIS", new Vector2(64, 358), Color.White, 0f, Vector2.Zero, 1.6f, SpriteEffects.None, 0f);
                    spriteBatch.DrawString(font, "- Align the blocks to fill a whole row.\n  Clear rows to empty the grid and score points!", new Vector2(64, 398), Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
                    spriteBatch.DrawString(font, "- Clearing multiple rows at once means more points.\n  Go for a TETRIS!", new Vector2(64, 448), Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
                    spriteBatch.DrawString(font, "- Try playing for as long as possible.\n  Hitting the top of the grid equals GAME OVER.", new Vector2(64, 498), Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);

                    spriteBatch.DrawString(font, "Note: F11 toggles between fullscreen and windowed mode.", new Vector2(64, 588), Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);

                    spriteBatch.DrawString(font, "SCORE CHART", new Vector2(728, 312), Color.White, 0f, Vector2.Zero, 1.6f, SpriteEffects.None, 0f);
                    spriteBatch.DrawString(font, "1 PT PER BLOCK", new Vector2(728, 352), Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
                    spriteBatch.DrawString(font, "1 ROW" + blank + "       100 PTS\n2 ROWS" + blank + "    250 PTS\n3 ROWS" + blank + "    800 PTS\n\n4 ROWS" + blank + "      TETRIS\n" + blank + blank + "    10000 PTS", new Vector2(728, 384), Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);

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
                    spriteBatch.DrawString(font, "Ended with a score of:", new Vector2(360, 400), Color.White, 0f, Vector2.Zero, 1.6f, SpriteEffects.None, 0f);
                    spriteBatch.DrawString(font, "" + score, new Vector2(360, 440), Color.White, 0f, Vector2.Zero, 2.0f, SpriteEffects.None, 0f);
                    spriteBatch.DrawString(font, "PRESS SPACE TO TRY AGAIN", new Vector2(240, 660), Color.White, 0f, Vector2.Zero, 2.0f, SpriteEffects.None, 0f);
                    break;
            }

            spriteBatch.DrawString(font, "Wassim Chammat    Corne van Vliet\n2981351                              6790836", new Vector2(6, 731), Color.White, 0f, Vector2.Zero, 0.8f, SpriteEffects.None, 0f);
        }

        public void Reset()
        {
        }

    }
}
