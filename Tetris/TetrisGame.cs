using System;
using System.Collections.Generic;

namespace Tetris
{
    class TetrisGame : ExtendedGame
    {
        [STAThread]
        static void Main(string[] args)
        {
            TetrisGame game = new TetrisGame();
            game.Run();
        }

        public TetrisGame()
        {
            IsMouseVisible = true;
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            // create and reset the game world
            gameWorld1 = new GameWorld();
            gameWorld1.Reset();
        }

    }
}

