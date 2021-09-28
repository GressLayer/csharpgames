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
        Vector2 BallPosition, BallOrigin;
        Texture2D Ball;

        public BallFunction(ContentManager Content)
        {
            Ball = Content.Load<Texture2D>("Pong Ball");
            BallPosition = new Vector2(800, 450);
            BallOrigin = new Vector2(Ball.Width / 2, Ball.Height / 2);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Ball, BallPosition, null, Color.White, 0.0f, BallOrigin, 0.25f, SpriteEffects.None, 0);
        }
    }
}
