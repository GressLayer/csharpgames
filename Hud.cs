using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;

namespace Pong
{
	public class Hud
	{
		// This enum holds all the game states used by the program.
		public enum State
        {
			Welcome,
			Controls,
			Playing,
			Over
        }

		/* HUDc: Classic
		 * HUDr: Rally
		 * HUDs: Sudden Death
		 * HUDl: Lives
		 */
		Texture2D welcome, hudC, hudR, hudS, hudL, gameOver;
		SpriteFont hudFont;

		Vector2 hudPos;

		public static int p1Score, p2Score, Rally;

		// Make the game start at the Welcome screen.
		public static State state = State.Welcome;

		public Hud(ContentManager Content)
		{
			welcome = Content.Load<Texture2D>("GameOver");

			hudC = Content.Load<Texture2D>("HUDc");
			hudR = Content.Load<Texture2D>("HUDr");
			hudS = Content.Load<Texture2D>("HUDs");
			hudL = Content.Load<Texture2D>("HUDl");
			hudFont = Content.Load<SpriteFont>("Font");
			hudPos = new Vector2(0, 900);

			gameOver = Content.Load<Texture2D>("GameOver");
		}

		public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
			/* Short explanation of the HUD's draw instructions:
			 * - HUD frame is drawn below the actual playing field (based on gamemode);
			 * - Speed of the ball is drawn as an absolute value, rounded to 2 decimals.
			 * - Score is drawn as "p1Score - p2Score".
			 * - The Rally counter is drawn, increasing by one for every successful bounce.
			 */

			// Draw HUD based on selected gamemode
			if(Ball.mode == 0) { spriteBatch.Draw(hudC, hudPos, Color.White); }
			if(Ball.mode == 1) { spriteBatch.Draw(hudR, hudPos, Color.White); }
			if(Ball.mode == 2) { spriteBatch.Draw(hudS, hudPos, Color.White); }
			if(Ball.mode == 3) { spriteBatch.Draw(hudL, hudPos, Color.White); }

			// Welcome screen.
			if (state == State.Welcome)
			{
				spriteBatch.Draw(welcome, new Vector2(0, 0), Color.White);
				spriteBatch.DrawString(hudFont, "PONG", new Vector2(670, 200), Color.White, 0, new Vector2(0, 0), 6.0f, SpriteEffects.None, 0f);
				spriteBatch.DrawString(hudFont, "Choose a gamemode, then press ENTER to start", new Vector2(300, 500), Color.White, 0, new Vector2(0, 0), 3.0f, SpriteEffects.None, 0f);
				spriteBatch.DrawString(hudFont, "To view the controls, press BACKSPACE", new Vector2(380, 550), Color.White, 0, new Vector2(0, 0), 3.0f, SpriteEffects.None, 0f);
			}

			// Controls screen.
			if (state == State.Controls)
			{
				spriteBatch.DrawString(hudFont, "MOVEMENT", new Vector2(300, 50), Color.White, 0, new Vector2(0, 0), 4.0f, SpriteEffects.None, 0f);
				spriteBatch.DrawString(hudFont, "Player 1: W/S to move\nPlayer 2: UP/DOWN to move\nSPACEBAR to ready the ball", new Vector2(300, 150), Color.White, 0, new Vector2(0, 0), 2.0f, SpriteEffects.None, 0f);
				spriteBatch.DrawString(hudFont, "GAMEMODES", new Vector2(300, 300), Color.White, 0, new Vector2(0, 0), 3.0f, SpriteEffects.None, 0f);
				spriteBatch.DrawString(hudFont, "Gamemode 1: CLASSIC MODE (blue)\n- 1 point per goal: 10 points to win.\n\nGamemode 2: RALLY MODE (green)\n- Keep the rally going to raise the stakes! 200 points to win.\n\nGamemode 3: SUDDEN DEATH MODE (yellow)\n- One point to win, and one point only. Who budges first?\n\nGamemode 4: LIVES MODE (white)\n- 3 lives, with one lost per goal. Stay alive the longest!", new Vector2(300, 375), Color.White, 0, new Vector2(0, 0), 2.0f, SpriteEffects.None, 0f);
				spriteBatch.DrawString(hudFont, "Choose a gamemode, then press ENTER to start", new Vector2(300, 800), Color.White, 0, new Vector2(0, 0), 3.0f, SpriteEffects.None, 0f);
			}

			// Basic HUD during gameplay.
			if (state == State.Playing)
			{
				spriteBatch.DrawString(hudFont, "SPEED: " + Math.Round(Math.Abs(Ball.BallSpeed.X), 2), new Vector2(24, 924), Color.White, 0, new Vector2(0, 0), 4.0f, SpriteEffects.None, 0f);
				spriteBatch.DrawString(hudFont, p1Score + " - " + p2Score, new Vector2(736, 924), Color.White, 0, new Vector2(0, 0), 4.0f, SpriteEffects.None, 0f);
				spriteBatch.DrawString(hudFont, "RALLY: " + Rally, new Vector2(1024, 924), Color.White, 0, new Vector2(0, 0), 4.0f, SpriteEffects.None, 0f);
			}

			// Game Over display.
			if (state == State.Over)
			{
				spriteBatch.Draw(gameOver, new Vector2(0, 0), Color.White);
				spriteBatch.DrawString(hudFont, "GAME OVER: PLAYER " + Ball.winner + " WINS", new Vector2(380, 250), Color.White, 0, new Vector2(0, 0), 4.0f, SpriteEffects.None, 0f);
				spriteBatch.DrawString(hudFont, "Ended at " + p1Score + " - " + p2Score, new Vector2(720, 350), Color.White, 0, new Vector2(0, 0), 2.0f, SpriteEffects.None, 0f);
				spriteBatch.DrawString(hudFont, "To play again, press SPACE", new Vector2(620, 500), Color.White, 0, new Vector2(0, 0), 2.0f, SpriteEffects.None, 0f);
			}
		}
	}
}
