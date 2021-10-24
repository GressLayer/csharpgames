using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Input;
using System;

using Tetris;

namespace Tetris
{
    // A class for representing the game world.
    // This contains the grid, the falling block, and everything else that the player can see/do.

    // An enum for the different game states that the game can have.
    public enum State
    {
        Welcome,
        Controls,
        Playing,
        GameOver
    }

    class GameWorld
    {
        // The current game state.
        public static State gameState;

        public static bool over;

        // Used to draw blank spaces in a more space-efficient way.
        string blank = "            ";

        // The main font of the game.
        SpriteFont font;

        // The main grid of the game.
        public static TetrisGrid grid { get; private set; }

        // All the sprites used for menus and HUDs.
        Texture2D menubar;
        SpriteGameObject menu, menu2, menubarS, menubar2S, hud;

        // All music in the game.
        Song welcome, controls, playing;
        static Song gameover;

        public int GridWidth { get; private set; } = 10;
        public int GridHeight { get; private set; } = 20;
        public int CellSize { get; private set; } = 32;
        public Vector2 GridOffset { get; private set; }


        public GameWorld()
        {
            // Game starts out in the Welcome game state, with looping music.
            gameState = State.Welcome;
            MediaPlayer.IsRepeating = true;

            // Loads all the music.
            welcome = ExtendedGame.ContentManager.Load<Song>("music/welcome");
            controls = ExtendedGame.ContentManager.Load<Song>("music/controls");
            playing = ExtendedGame.ContentManager.Load<Song>("music/playing");
            gameover = ExtendedGame.ContentManager.Load<Song>("music/gameover");

            // Loads all the menu and HUD sprites.
            menu = new SpriteGameObject("sprites/menu");
            menubar = ExtendedGame.ContentManager.Load<Texture2D>("sprites/menubar");
            menu2 = new SpriteGameObject("sprites/menu2");
            menubarS = new SpriteGameObject("sprites/menubarS");
            menubar2S = new SpriteGameObject("sprites/menubar2S");
            hud = new SpriteGameObject("sprites/hud");

            // Loads the game font.
            font = ExtendedGame.ContentManager.Load<SpriteFont>("Font");

            // Sets the size of the grid (height, width, cell size and where the grid is placed on the screen).
            // GridWidth = 10; 
            // GridHeight = 20; 
            // CellSize = 32;
            GridOffset = new Vector2(96, 64);

            // Creates a grid object.
            grid = new TetrisGrid(GridWidth, GridHeight, CellSize, GridOffset);

            // Starts playing the music for the welcome screen.
            MediaPlayer.Play(welcome);
        }

        public void HandleInput(InputHelper inputHelper)
        {
            // Holds input options per game state.
            grid.HandleInput(inputHelper);
            switch (gameState)
            {
                case (State.Welcome):
                    MediaPlayer.IsRepeating = true;
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
                    }
                    break;
            }

        }

        public void Update(GameTime gameTime)
        {
            if (gameState == State.Playing)
            grid.Update(gameTime);
        }

        public static void EndGame(bool over)
        {
            if (over)
            {
                gameState = State.GameOver;
                MediaPlayer.IsRepeating = false;
                MediaPlayer.Play(gameover);

                grid.ResetGrid();
            }

            over = false;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            /* One beefy switch.
             * Based on gameState's value, all the necessary menu/HUD sprites and strings are drawn.
             */
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
                    spriteBatch.DrawString(font, "1 ROW" + blank + "       100 PTS\n2 ROWS" + blank + "    250 PTS\n3 ROWS" + blank + "    800 PTS\n\n4 ROWS" + blank + "      TETRIS\n" + blank + blank + "      5000 PTS\n\n 20000 PTS TO LEVEL UP", new Vector2(728, 384), Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);

                    break;
                case (State.Playing):
                    menu2.Draw(gameTime, spriteBatch);
                    hud.Draw(gameTime, spriteBatch);
                    grid.Draw(gameTime, spriteBatch);
                    spriteBatch.DrawString(font, "" + TetrisGrid.score, new Vector2(582, 60), Color.White, 0f, Vector2.Zero, 2.0f, SpriteEffects.None, 0f);
                    spriteBatch.DrawString(font, "" + TetrisGrid.level, new Vector2(864, 60), Color.White, 0f, Vector2.Zero, 2.0f, SpriteEffects.None, 0f);
                    spriteBatch.DrawString(font, "" + TetrisGrid.blocksUsed, new Vector2(582, 340), Color.White, 0f, Vector2.Zero, 2.0f, SpriteEffects.None, 0f);
                    spriteBatch.DrawString(font, "" + TetrisGrid.holdsUsed, new Vector2(840, 340), Color.White, 0f, Vector2.Zero, 2.0f, SpriteEffects.None, 0f);

                    break;
                case (State.GameOver):
                    menu2.Draw(gameTime, spriteBatch);
                    menubar2S.Draw(gameTime, spriteBatch);
                    spriteBatch.DrawString(font, "GAME OVER", new Vector2(750, 112), Color.White, 0f, Vector2.Zero, 2.0f, SpriteEffects.None, 0f);
                    spriteBatch.DrawString(font, "Ended with a score of:", new Vector2(360, 300), Color.White, 0f, Vector2.Zero, 1.6f, SpriteEffects.None, 0f);
                    spriteBatch.DrawString(font, "" + TetrisGrid.score, new Vector2(360, 340), Color.White, 0f, Vector2.Zero, 2.0f, SpriteEffects.None, 0f);
                    spriteBatch.DrawString(font, "You have used " + TetrisGrid.blocksUsed + " blocks and held " + TetrisGrid.holdsUsed + ".", new Vector2(224, 440), Color.White, 0f, Vector2.Zero, 1.8f, SpriteEffects.None, 0f);
                    spriteBatch.DrawString(font, "PRESS SPACE TO TRY AGAIN", new Vector2(240, 660), Color.White, 0f, Vector2.Zero, 2.0f, SpriteEffects.None, 0f);
                    break;
            }

            // Student names and numbers.
            spriteBatch.DrawString(font, "Wassim Chammat    Corne van Vliet\n2981351                              6790836", new Vector2(6, 731), Color.White, 0f, Vector2.Zero, 0.8f, SpriteEffects.None, 0f);
        }

        public void Reset()
        {
        }
    }
}
