using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using System;

namespace Pong
{
    class BallFunction
    {
        Vector2 BallPosition, BallOrigin, BallSpeed, BallInitial;
        Texture2D Ball;
        KeyboardState KeyboardCurrent, KeyboardPrevious;
        bool started, touchingBat1, touchingBat2;

        public Vector2 BatPos1, BatPos2;

        public BallFunction(ContentManager Content)
        {
            Ball = Content.Load<Texture2D>("Pong Ball");
            BallPosition = new Vector2(800, 450);
            BallOrigin = new Vector2(Ball.Width / 2, Ball.Height / 2);
            BallInitial = new Vector2(8, 2);
            BallSpeed = new Vector2(0, 0);
            started = false;
        }

        // Allows for a button to be pressed once instead of held for certain actions.
        // Any action using "if (KeysPressed(...))" does not need the key to be held down in order to activate.
        public bool KeyPressed(Keys k)
        {
            return KeyboardCurrent.IsKeyDown(k) && KeyboardPrevious.IsKeyUp(k);
        }

        // Method used after the ball does not hit the bat or is reset.
        public void ResetBall()
        {
            started = false;
            BallSpeed.X = 0;
            BallSpeed.Y = 0;
            BallPosition.X = 800;
            BallPosition.Y = 450;
        }

        public void BallUpdate(GameTime gameTime)
        {
            KeyboardPrevious = KeyboardCurrent;
            KeyboardCurrent = Keyboard.GetState();

            // Start button command: used to allow the ball to move after a reset.
            if (KeyPressed(Keys.Space) && started == false)
            {
                BallSpeed = BallInitial;
                started = true;
                touchingBat1 = false;
                touchingBat2 = false;
            }

            // Reset button command: used for developer purposes only (comment out with /**/ if required).
            if (KeyPressed(Keys.R) && started == true)
            {
                ResetBall();
            }

            if (started)
            {
                BallPosition += BallSpeed;
            }

            if (BallPosition.Y >= BatPos1.Y && BallPosition.Y <= (BatPos1.Y + 192))
            {
                touchingBat1 = true;
            }
            if (BallPosition.Y >= BatPos2.Y && BallPosition.Y <= (BatPos2.Y + 192))
            {
                touchingBat2 = true;
            }

            if (touchingBat1 == false || touchingBat2 == false)
            {
                if (BallPosition.X <= 15 || BallPosition.X >= 1585)
                {
                    ResetBall();
                }
            }

            if (touchingBat1 == true || touchingBat2 == true)
            {
                
                if (BallPosition.X <= 15 || BallPosition.X >= 1585)
                {
                    BallSpeed.X *= -1.1f;
                }
            }
            if (BallPosition.Y <= 15 || BallPosition.Y >= 885)
            {
                BallSpeed.Y *= -1.1f;
            }

            // Speed caps, used to keep the game from becoming too impossible, while still allowing the ball to be fast enough to easily score a point.
            if (BallSpeed.X >= 40)
            {
                BallSpeed.X = 40;
            }
            if (BallSpeed.X <= -40)
            {
                BallSpeed.X = -40;
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

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Ball, BallPosition, null, Color.White, 0.0f, BallOrigin, 0.25f, SpriteEffects.None, 0);
        }
    }
}
