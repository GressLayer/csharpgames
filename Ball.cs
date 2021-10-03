using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using System;

namespace Pong
{
    // Constructor
    class Ball
    {
        Vector2 BallPosition, BallOrigin, BallInitial;
        public static Vector2 BallSpeed;
        Texture2D ball;
        KeyboardState KeyboardCurrent, KeyboardPrevious;
        bool started;
        static Random angle, anglemod;
        int randomangle;

        // Loads the ball sprite and assigns values to several of the ball's necesssary variables.
        public Ball(ContentManager Content)
        {
            ball = Content.Load<Texture2D>("Pong Ball");
            BallPosition = new Vector2(800, 450);
            BallOrigin = new Vector2(ball.Width / 2, ball.Height / 2);
            BallInitial = new Vector2(5, 0);
            BallSpeed = new Vector2(0, 0);
            started = false;
            angle = new Random(3) ;
            anglemod = new Random(2);
        }

        // Allows for a button to be pressed once instead of held for certain actions.
        // Any action using "if (KeysPressed(...))" does not need the key to be held down in order to activate.
        public bool KeyPressed(Keys k)
        {
            return KeyboardCurrent.IsKeyDown(k) && KeyboardPrevious.IsKeyUp(k);
        }

        // Method that generates a random angle for the ball to bounce at whenever it is called.
        public void GenerateAngle()
        {
            randomangle = angle.Next(3);
            int anglemodifier = anglemod.Next(2);
            if (anglemodifier == 0)
            {
                randomangle = randomangle * -1;
            }
            
        }
        
        public int RandomAngle
        {
            get { return randomangle; }
        }

        // Method called after the ball does not hit the bat or is reset.
        public void ResetBall()
        {
            started = false;
            BallSpeed.X = 0.0f;
            BallSpeed.Y = 0.0f;
            BallPosition.X = 800;
            BallPosition.Y = 450;
            Hud.Rally = 0;

        }

        // Ball bounding box.
        public Rectangle BoundingBox
        {
            get
            {
                Rectangle spriteBounds = ball.Bounds;
                spriteBounds.Offset(BallPosition - BallOrigin);
                return spriteBounds;
            }
        }

        // Update method for the ball.
        public void BallUpdate(GameTime gameTime)
        {

            KeyboardPrevious = KeyboardCurrent;
            KeyboardCurrent = Keyboard.GetState();

            // Start button command: used to allow the ball to move after a reset.
            if (KeyPressed(Keys.Space) && started == false)
            {
                BallSpeed = BallInitial;
                started = true;
            }

            // Reset button command: used for developer purposes only (comment out with /**/ in final version).
            if (KeyPressed(Keys.R) && started == true)
            {
                ResetBall();
            }

            if (started)
            {
                BallPosition += BallSpeed;
            }

            if (BallPosition.X <= -30) { ResetBall(); Hud.p2Score++; }
            if (BallPosition.X >= 1600) { ResetBall(); Hud.p1Score++; }

            if (BallPosition.Y <= 15 || BallPosition.Y >= 885)
            {
                BallSpeed.Y *= -1.1f;
            }

            // Speed caps: hard speed limits on the ball.
            // These are used to keep the game from becoming too impossible, while still allowing the ball to be fast enough to easily score a point.

            // W: I would LOVE to use a cleaner structure here (like a switch-case), but that only allows for == checks (in short, useless for a speed cap).
            if (BallSpeed.X >= 35)
            {
                BallSpeed.X = 35;
            }
            if (BallSpeed.X <= -35)
            {
                BallSpeed.X = -35;
            }
            if (BallSpeed.Y >= 30)
            {
                BallSpeed.Y = 30;
            }
            if (BallSpeed.Y <= -30)
            {
                BallSpeed.Y = -30;
            }
        }

        // Draw method that... well... draws the ball.
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(ball, BallPosition, null, Color.White, 0.0f, BallOrigin, 1.0f, SpriteEffects.None, 0);
        }
    }
}
