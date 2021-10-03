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

        public static Player1 player1;
        public static Player2 player2;
        Ball ball;
        Hud hud;
        
        
        // 2D textures for the Bats and the Ball

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        // Initialize function: called once at startup.
        protected override void Initialize()
        {
            // Changes the window size
            graphics.PreferredBackBufferWidth = 1600;
            graphics.PreferredBackBufferHeight = 1000;
            graphics.ApplyChanges();
   
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            player1 = new Player1(Content);
            player2 = new Player2(Content);
            ball = new Ball(Content);
            hud = new Hud(Content);
            //Loading the Sprites for the Bats and the Ball
        }

        protected override void Update(GameTime gameTime)
        {

            player1.Movement();
            player2.Movement();
            player1.Boundaries();
            player2.Boundaries();
            ball.BallUpdate(gameTime);
            base.Update(gameTime);

            if (ball.BoundingBox.Intersects(player1.BoundingBox) || ball.BoundingBox.Intersects(player2.BoundingBox))
            {
                ball.GenerateAngle();
                Ball.BallSpeed.X = Ball.BallSpeed.X * -1.1f;
                Ball.BallSpeed.Y = Ball.BallSpeed.Y + ball.RandomAngle*1.5f;
                Hud.Rally++;
            }

        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            player1.Draw(gameTime, spriteBatch);
            player2.Draw(gameTime, spriteBatch);
            ball.Draw(gameTime, spriteBatch);
            hud.Draw(gameTime, spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public Player1 Player1
        {
            get { return player1; }
        }

        public Player2 Player2
        {
            get { return player2; }
        }
    }
}
