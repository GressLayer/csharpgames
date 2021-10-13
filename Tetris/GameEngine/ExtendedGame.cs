using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Tetris
{
    class ExtendedGame : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        protected InputHelper inputHelper;
        protected GameWorld gameWorld1;

        protected static List<GameObject> gameWorld;

        /// <summary>
        /// A static reference to the ContentManager object, used for loading assets.
        /// </summary>
        public static ContentManager ContentManager { get; private set; }
        
        /// <summary>
        /// A static reference to the width and height of the screen.
        /// </summary>
        public static Point ScreenSize { get; private set; }


        public ExtendedGame()
        {
            graphics = new GraphicsDeviceManager(this);
            // set the directory where game assets are locate
            Content.RootDirectory = "Content";

            inputHelper = new InputHelper();
            ScreenSize = new Point(1024, 768);

            graphics.PreferredBackBufferWidth = ScreenSize.X;
            graphics.PreferredBackBufferHeight = ScreenSize.Y;
            graphics.ApplyChanges();

        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            ContentManager = Content;

            gameWorld = new List<GameObject>();

            /*Fullscreen = false;*/
        }

        protected override void Update(GameTime gameTime)
        {
            HandleInput();
            base.Update(gameTime);
        }

        protected void HandleInput()
        {
            inputHelper.Update();

            if (inputHelper.KeyPressed(Keys.Escape))
                Exit();
            /*if (inputHelper.KeyPressed(Keys.R))
                FullScreen = !FullScreen;*/

            gameWorld1.HandleInput(inputHelper);

        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            base.Draw(gameTime);
            spriteBatch.Begin();
            gameWorld1.Draw(gameTime, spriteBatch);
            spriteBatch.End();
        }

        public static List<GameObject> GameWorld
        {
            get { return gameWorld; }
        }
    }
}
