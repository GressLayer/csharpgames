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

        public bool KeyPressed(Keys k)
        {
            return KeyboardCurrent.IsKeyDown(k) && KeyboardPrevious.IsKeyUp(k);
        }

        public void ResetBall()
        {
            started = false;
            touchingBat1 = false;
            touchingBat2 = false;
            BallSpeed.X = 0;
            BallSpeed.Y = 0;
            BallPosition.X = 800;
            BallPosition.Y = 450;
        }

        public void BallUpdate(GameTime gameTime)
        {
            KeyboardPrevious = KeyboardCurrent;
            KeyboardCurrent = Keyboard.GetState();

            if (KeyPressed(Keys.Space) && started == false)
            {
                BallSpeed = BallInitial;
                started = true;
                touchingBat1 = false;
                touchingBat2 = false;
            }

            if (started)
            {
                BallPosition += BallSpeed;
            }

            if (BallPosition.Y >= BatPos1.Y && BallPosition.Y <= BatPos1.Y + 192)
            {
                touchingBat1 = true;
            }
            if (BallPosition.Y >= BatPos2.Y && BallPosition.Y <= BatPos2.Y + 192)
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

            if (BallPosition.X <= 15 || BallPosition.X >= 1585)
            {
                if (touchingBat1 == true || touchingBat2 == true)
                {
                    BallSpeed.X *= -1.1f;
                    touchingBat1 = false;
                    touchingBat2 = false;
                }
            }
            if (BallPosition.Y <= 15 || BallPosition.Y >= 885)
            {
                BallSpeed.Y *= -1.1f;
            }

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

            if (KeyPressed(Keys.R))
            {
                ResetBall();
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Ball, BallPosition, null, Color.White, 0.0f, BallOrigin, 0.25f, SpriteEffects.None, 0);
        }
    }
}
