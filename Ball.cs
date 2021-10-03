using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System;

namespace Pong
{
    class Ball
    {
        Vector2 BallPosition, BallOrigin, BallInitial;
        public static Vector2 BallSpeed;
        Texture2D ball;
        KeyboardState KeyboardCurrent, KeyboardPrevious;
        static Random angle, anglemod;
        int randomangle;

        /* Mode variable:
         * - Set to 0 for Classic Mode (1 point per score, first to 10 wins)
         * - Set to 1 for Rally Mode (Rally becomes points scored, first to 200 wins)
         * = Set to 2 for Sudden Death Mode (1 point needed to win, first score wins)
         */
        public static int mode, winner;

        // Loads the ball sprite and assigns values to several of the ball's necesssary variables.
        public Ball(ContentManager Content)
        {
            ball = Content.Load<Texture2D>("Pong Ball");
            BallPosition = new Vector2(800, 450);
            BallOrigin = new Vector2(ball.Width / 2, ball.Height / 2);
            BallInitial = new Vector2(5, 0);
            BallSpeed = new Vector2(0, 0);
            angle = new Random(3);
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
            Hud.state = Hud.State.Playing;
            BallSpeed.X = 0.0f;
            BallSpeed.Y = 0.0f;
            BallPosition.X = 800;
            BallPosition.Y = 450;
            Hud.Rally = 0;
            winner = 0;
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
            if (KeyPressed(Keys.Space) && Hud.state == Hud.State.Playing) { BallSpeed = BallInitial; }

            if (Hud.state == Hud.State.Playing)
            {
                BallPosition += BallSpeed;

                // Reset button command: used for developer purposes only (comment out with /**/ in final version).
                if (KeyPressed(Keys.R))
                {
                    ResetBall();
                }

                // Handles scoring for each different gamemode.
                if (mode == 0) {
                    if (BallPosition.X <= -30) {
                        ResetBall();
                        Hud.p2Score++;
                    }
                    if (BallPosition.X >= 1600) {
                        ResetBall();
                        Hud.p1Score++;
                    }
                    if (Hud.p1Score == 10)
                    {
                        winner = 1;
                        Hud.state = Hud.State.Over;
                    }
                    if (Hud.p2Score == 10)
                    {
                        winner = 2;
                        Hud.state = Hud.State.Over;
                    }
                }
                if (mode == 1) {
                    if (BallPosition.X <= -30) {

                        Hud.p2Score += Hud.Rally;
                        ResetBall();
                    }
                    if (BallPosition.X >= 1600) {

                        Hud.p1Score += Hud.Rally;
                        ResetBall();
                    }
                    if (Hud.p1Score == 200)
                    {
                        winner = 1;
                        ResetBall();
                        Hud.state = Hud.State.Over;
                    }
                    if (Hud.p2Score == 200)
                    {
                        winner = 2;
                        ResetBall();
                        Hud.state = Hud.State.Over;
                    }
                }
                if (mode == 2)
                {
                    if (BallPosition.X <= -30)
                    {

                        Hud.p2Score++;
                        ResetBall();
                        winner = 2;
                        Hud.state = Hud.State.Over;
                    }
                    if (BallPosition.X >= 1600)
                    {
                        Hud.p1Score++;
                        ResetBall();
                        winner = 1;
                        Hud.state = Hud.State.Over;
                    }
                }
                if (mode == 3)
                    {
                    if (BallPosition.X <= -30)
                    {
                        Hud.p1Score--;
                        ResetBall();
                    }
                    if (BallPosition.X >= 1600)
                    {
                        Hud.p2Score--;
                        ResetBall();
                    }
                    if (Hud.p1Score == 0)
                    {
                        ResetBall();
                        winner = 2;
                        Hud.state = Hud.State.Over;
                    }
                    if (Hud.p2Score == 0)
                    {
                        ResetBall();
                        winner = 1;
                        Hud.state = Hud.State.Over;
                    }
                }

                // Makes the ball bounce harder in the opposite direction after hitting the top or bottom of the playing field.
                if (BallPosition.Y <= 15 || BallPosition.Y >= 885)
                {
                    BallSpeed.Y *= -1.1f;
                }
            }

            if (Hud.state == Hud.State.Over)
            {
                if (KeyPressed(Keys.Space))
                {
                    Hud.state = Hud.State.Welcome;
                }
            }

            if (Hud.state == Hud.State.Welcome)
            {
                if (KeyPressed(Keys.Enter)) { Hud.state = Hud.State.Playing; }
                if (KeyPressed(Keys.Back)) { Hud.state = Hud.State.Controls; }
            }

            if (Hud.state == Hud.State.Controls)
            {
                if (KeyPressed(Keys.Enter)) { Hud.state = Hud.State.Playing; }
            }

            if (Hud.state != Hud.State.Playing) {
                if (KeyPressed(Keys.D1)) { mode = 0; Hud.p1Score = 0; Hud.p2Score = 0; }
                if (KeyPressed(Keys.D2)) { mode = 1; Hud.p1Score = 0; Hud.p2Score = 0; }
                if (KeyPressed(Keys.D3)) { mode = 2; Hud.p1Score = 0; Hud.p2Score = 0; }
                if (KeyPressed(Keys.D4)) { mode = 3; Hud.p1Score = 3; Hud.p2Score = 3; }
            }

            /* Speed caps: hard speed limits on the ball.
             * These are used to keep the game from becoming too impossible, while still allowing the ball to be fast enough during big rallies.
             */
            if (BallSpeed.X >= 35) { BallSpeed.X = 35;}
            if (BallSpeed.X <= -35) { BallSpeed.X = -35; }
            if (BallSpeed.Y >= 30) { BallSpeed.X = 30; }
            if (BallSpeed.Y <= -30) { BallSpeed.X = -30; }
        }

        // Draw method that... well... draws the ball.
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (Hud.state == Hud.State.Playing) { spriteBatch.Draw(ball, BallPosition, null, Color.White, 0.0f, BallOrigin, 1.0f, SpriteEffects.None, 0); }
        }
    }
}
