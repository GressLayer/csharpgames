using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Pong
{
    public class Player2
    {
        public Vector2 Bat2Position, Bat2Origin;
        Texture2D Bat2;

        public Player2(ContentManager Content)
        {
            Bat2 = Content.Load<Texture2D>("Pong Bat");

            Bat2Position = new Vector2(1600, 450);
            Bat2Origin = new Vector2(Bat2.Width, Bat2.Height / 2);
        }

        // Handles movement for Player 2's bat.
        public void Movement()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                Bat2Position.Y -= 8;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                Bat2Position.Y += 8;
            }
        }

        public void Boundaries()
        {
            if (Bat2Position.Y <= Bat2Origin.Y)
            {
                Bat2Position.Y = Bat2Origin.Y;
            }

            if (Bat2Position.Y >= 900 - Bat2Origin.Y)
            {
                Bat2Position.Y = 900 - Bat2Origin.Y;
            }
        }

        // "Hitbox" for Player 2's bat.
        public Rectangle BoundingBox
        {
            get
            {
                Rectangle spriteBounds = Bat2.Bounds;
                spriteBounds.Offset(Bat2Position - Bat2Origin);
                return spriteBounds;
            }
        }

        // Draws Player 2's bat.
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if(Hud.state == Hud.State.Playing) { spriteBatch.Draw(Bat2, Bat2Position, null, Color.White, 0.0f, Bat2Origin, 1.0f, SpriteEffects.None, 0); }
        }
    }
}
