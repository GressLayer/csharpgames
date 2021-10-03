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

        /* Mode variable:
         * - Set to 0 for Classic Mode (1 point per score, first to 10 wins)
         * - Set to 1 for Rally Mode (Rally becomes points scored, first to 200 wins)
         * = Set to 2 for Sudden Death Mode (1 point needed to win, first score wins)
         */
        public static int mode = 2, winner;

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
            if (KeyPressed(Keys.Space) && started == false && Hud.over == false) { BallSpeed = BallInitial; started = true; }

            if (KeyPressed(Keys.D1) && started == false && Hud.over == true) { mode = 0; }
            if (KeyPressed(Keys.D2) && started == false && Hud.over == true) { mode = 1; }
            if (KeyPressed(Keys.D3) && started == false && Hud.over == true) { mode = 2; }

            if (started == true)
            {
                Hud.over = false;
                BallPosition += BallSpeed;

                // Reset button command: used for developer purposes only (comment out with /**/ in final version).
                if (KeyPressed(Keys.R) && Hud.over == false)
                {
                    ResetBall();
                }

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
                        Hud.over = true;
                    }
                    if (Hud.p2Score == 10)
                    {
                        winner = 2;
                        Hud.over = true;
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
                        Hud.over = true;
                    }
                    if (Hud.p2Score == 200)
                    {
                        winner = 2;
                        ResetBall();
                        Hud.over = true;
                    }
                }
                if (mode == 2)
                {
                    if (BallPosition.X <= -30)
                    {

                        Hud.p2Score++;
                        ResetBall();
                        winner = 2;
                        Hud.over = true;
                    }
                    if (BallPosition.X >= 1600)
                    {
                        Hud.p1Score++;
                        ResetBall();
                        winner = 2;
                        Hud.over = true;
                    }
                }

                if (BallPosition.Y <= 15 || BallPosition.Y >= 885)
                {
                    BallSpeed.Y *= -1.1f;
                }
            }

            if (Hud.over == true)
            {
                if (KeyPressed(Keys.Enter) && started == false) 
                { 
                    Hud.over = false; 
                    Hud.p1Score = 0; 
                    Hud.p2Score = 0;
                }
            }

            /* Speed caps: hard speed limits on the ball.
             * These are used to keep the game from becoming too impossible, while still allowing the ball to be fast enough to easily score a point.
             */

            // W: I would LOVE to use a cleaner structure here (like a switch-case), but that only allows for == checks (in short, useless for a speed cap).
            if (BallSpeed.X >= 35) { BallSpeed.X = 35;}
            if (BallSpeed.X <= -35) { BallSpeed.X = -35; }
            if (BallSpeed.Y >= 30) { BallSpeed.X = 30; }
            if (BallSpeed.Y <= -30) { BallSpeed.X = -30; }
        }

        // Draw method that... well... draws the ball.
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(ball, BallPosition, null, Color.White, 0.0f, BallOrigin, 1.0f, SpriteEffects.None, 0);
        }
    }
}
