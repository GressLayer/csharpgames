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
		// HUDc is the Classic Mode HUD (blue), while HUDr is the Rally Mode HUD (green);
		Texture2D hudC, hudR, gameOver;
		SpriteFont hudFont;

		Vector2 hudPos;

		public static int p1Score, p2Score, Rally;
		public static bool over;

		public Hud(ContentManager Content)
		{
			hudC = Content.Load<Texture2D>("HUDc");
			hudR = Content.Load<Texture2D>("HUDr");
			hudFont = Content.Load<SpriteFont>("Font");
			hudPos = new Vector2(0, 900);

			over = false;
			gameOver = Content.Load<Texture2D>("GameOver");
		}

		public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
			/* Short explanation of the HUD's draw instructions:
			 * - HUD frame is drawn below the actual playing field (either Classic or Rally);
			 * - Speed of the ball is drawn as an absolute value, rounded to 2 decimals.
			 * - Score is drawn as p1Score - p2Score.
			 * - The Rally counter is drawn, increasing by one for every successful bounce.
			 */

			if (Ball.mode == 0) { spriteBatch.Draw(hudC, hudPos, Color.White); }
			if (Ball.mode == 1) { spriteBatch.Draw(hudR, hudPos, Color.White); }

			if (over == false)
			{
				spriteBatch.DrawString(hudFont, "SPEED: " + Math.Round(Math.Abs(Ball.BallSpeed.X), 2), new Vector2(24, 924), Color.White, 0, new Vector2(0, 0), 4.0f, SpriteEffects.None, 0f);
				spriteBatch.DrawString(hudFont, p1Score + " - " + p2Score, new Vector2(736, 924), Color.White, 0, new Vector2(0, 0), 4.0f, SpriteEffects.None, 0f);
				spriteBatch.DrawString(hudFont, "RALLY: " + Rally, new Vector2(1024, 924), Color.White, 0, new Vector2(0, 0), 4.0f, SpriteEffects.None, 0f);
			}

			if (over == true)
            {
				spriteBatch.Draw(gameOver, new Vector2(0, 0), Color.White);
				spriteBatch.DrawString(hudFont, "GAME OVER: PLAYER " + Ball.winner + " WINS", new Vector2(400, 300), Color.White, 0, new Vector2(0, 0), 4.0f, SpriteEffects.None, 0f);
				spriteBatch.DrawString(hudFont, "Ended at " + p1Score + " - " + p2Score, new Vector2(720, 400), Color.White, 0, new Vector2(0, 0), 2.0f, SpriteEffects.None, 0f);
				spriteBatch.DrawString(hudFont, "To play again, press ENTER", new Vector2(616, 450), Color.White, 0, new Vector2(0, 0), 2.0f, SpriteEffects.None, 0f);
			}
		}
	}
}
