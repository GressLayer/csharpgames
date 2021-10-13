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

        // stores the width and height of the game world.
        Point worldSize;
        // stores the width and height of the window in pixels.
        Point windowSize;
        //Scales the game world based on screensize.
        Matrix spriteScale;

        // A static reference to the ContentManager object, used for loading assets.
        public static ContentManager ContentManager { get; private set; }

        public ExtendedGame()
        {
            graphics = new GraphicsDeviceManager(this);
            // set the directory where game assets are locate
            Content.RootDirectory = "Content";

            inputHelper = new InputHelper();

        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            ContentManager = Content;
            worldSize = new Point(1024, 768);
            windowSize = new Point(1024, 768);
            FullScreen = false;
            
            
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
            if (inputHelper.KeyPressed(Keys.F11))
                FullScreen = !FullScreen;

            gameWorld1.HandleInput(inputHelper);

        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            base.Draw(gameTime);
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, spriteScale);
            gameWorld1.Draw(gameTime, spriteBatch);
            spriteBatch.End();
        }

        void ApplyResolutionSettings(bool fullScreen)
        {
            graphics.IsFullScreen = fullScreen;

            Point screenSize;
            if (fullScreen)
            {
                screenSize = new Point(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width,
                    GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);
            }
            else
                screenSize = windowSize;
            
            graphics.PreferredBackBufferWidth = screenSize.X;
            graphics.PreferredBackBufferHeight = screenSize.Y;

            graphics.IsFullScreen = fullScreen;
            graphics.ApplyChanges();

            GraphicsDevice.Viewport = CalculateViewport(screenSize);
            spriteScale = Matrix.CreateScale((float)GraphicsDevice.Viewport.Width / worldSize.X,
                (float)GraphicsDevice.Viewport.Height / worldSize.Y, 1);
        }

        Viewport CalculateViewport(Point windowSize)
        {
            Viewport viewport = new Viewport();

            float gameAspectRatio = (float)worldSize.X / worldSize.Y;
            float windowAspectRatio = (float)windowSize.X / windowSize.Y;

            if (windowAspectRatio > gameAspectRatio)
            {
                viewport.Width = (int)(windowSize.Y * gameAspectRatio);
                viewport.Height = windowSize.Y;
            }
            else
            {
                viewport.Width = windowSize.X;
                viewport.Height = (int)(windowSize.X / gameAspectRatio);
            }

            viewport.X = (windowSize.X - viewport.Width) / 2;
            viewport.Y = (windowSize.Y - viewport.Height) / 2;

            return viewport;
        }
        
        // A boolean property that sets full screen or disables it.
        public bool FullScreen
        {
            get { return graphics.IsFullScreen; }
            protected set { ApplyResolutionSettings(value); }
        }
    }


}
