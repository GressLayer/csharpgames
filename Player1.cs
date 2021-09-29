using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using System;
namespace Pong
{
    class Player1
    {
        public Vector2 Bat1Position, Bat1Origin;
        Texture2D Bat1;

        public Player1(ContentManager Content)
        {
            Bat1 = Content.Load<Texture2D>("Pong Bat");

            Bat1Position = new Vector2(0, 450);
            Bat1Origin = new Vector2(0, Bat1.Height / 2);
        }
        public void Movement()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                Bat1Position.Y -= 8;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                Bat1Position.Y += 8;
            }
        }

        public void Boundaries()
        {
            if (Bat1Position.Y <= Bat1Origin.Y)
            {
                Bat1Position.Y = Bat1Origin.Y;
            }

            if (Bat1Position.Y >= 900 - Bat1Origin.Y)
            {
                Bat1Position.Y = 900 - Bat1Origin.Y;
            }
        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Bat1, Bat1Position, null, Color.White, 0.0f, Bat1Origin, 1.0f, SpriteEffects.None, 0);
        }

        public Vector2 BatPos1 { get { return Bat1Position;} }
    }
}
