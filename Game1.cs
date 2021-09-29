using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using System;

namespace Pong
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        Player1 player1;
        Player2 player2;
        BallFunction ball;
        // Vectors for the Position and Origin of the Bats and the Ball

        
        
        // 2D textures for the Bats and the Ball

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 1600; //Changes to window size
            graphics.PreferredBackBufferHeight = 900;
            graphics.ApplyChanges();

            
            
            // Declaration of the Positions of the Ball and the Bats
   

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            player1 = new Player1(Content);
            player2 = new Player2(Content);
            ball = new BallFunction(Content);
            //Loading the Sprites for the Bats and the Ball
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            //Declaration of the Origin of the Bats and the ball to centre them correctly

            player1.Movement();
            player2.Movement();
            player1.Boundaries();
            player2.Boundaries();
            ball.BallUpdate(gameTime);
            base.Update(gameTime);

            ball.BatPos1 = player1.Bat1Position;
            ball.BatPos2 = player2.Bat2Position;

            //Pressing Up or Down moves Bat2 by 8 pixels per frame

            //If Bat1 touches the upper or bottom border it doesn't move off the screen

            //If Bat2 touches the upper or bottom border it doesn't move off the screen
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            player1.Draw(gameTime, spriteBatch);
            player2.Draw(gameTime, spriteBatch);
            ball.Draw(gameTime, spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
