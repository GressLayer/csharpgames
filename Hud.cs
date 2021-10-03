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
	public class Hud
	{
		Texture2D hud;
		SpriteFont hudFont;

		Vector2 hudPos;

		public static int p1Score, p2Score, Rally;

		public Hud(ContentManager Content)
		{
			hud = Content.Load<Texture2D>("HUD");
			hudFont = Content.Load<SpriteFont>("Font");
			hudPos = new Vector2(0, 900);
		}

		public void Update(GameTime gameTime)
        {

        }

		public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
			/* 
				Short explanation of the HUD's draw instructions:
					HUD frame is drawn below the actual playing field;
					Speed of the ball is drawn as an absolute value, rounded to 2 decimals.
					Score is drawn: Player 1 - Player 2.
					The Rally counter is drawn, increasing by one for every successful bounce.
			*/
			spriteBatch.Draw(hud, hudPos, Color.White);
			spriteBatch.DrawString(hudFont, "SPEED: " + Math.Round(Math.Abs(Ball.BallSpeed.X), 2), new Vector2(24, 924), Color.White, 0, new Vector2(0, 0), 4.0f, SpriteEffects.None, 0f);
			spriteBatch.DrawString(hudFont, p1Score + " - " + p2Score, new Vector2(736, 924), Color.White, 0, new Vector2(0, 0), 4.0f, SpriteEffects.None, 0f);
			spriteBatch.DrawString(hudFont, "RALLY: " + Rally, new Vector2(1024, 924), Color.White, 0, new Vector2(0, 0), 4.0f, SpriteEffects.None, 0f);
		}
	}
}
