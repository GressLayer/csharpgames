using System;
using Microsoft.Xna.Framework;
using Engine;

class Star : SpriteGameObject
{
    Level level;
    protected float bounce;
    Vector2 startPosition;

    public static double starTimer;
    public static bool starCollected;

    public Star(Level level, Vector2 startPosition) : base("Sprites/LevelObjects/spr_star", TickTick.Depth_LevelObjects)
    {
        this.level = level;
        this.startPosition = startPosition;

        SetOriginToCenter();

        Reset();
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        double t = gameTime.TotalGameTime.TotalSeconds * 3.0f + LocalPosition.X;
        bounce = (float)Math.Sin(t) * 0.2f;
        localPosition.Y += bounce;

        // check if the player collects this item
        if (Visible && level.Player.CanCollideWithObjects && HasPixelPreciseCollision(level.Player))
        {
            Visible = false;

            starCollected = true;

            // Collecting a star makes the player invincible.
            if (starCollected)
                starTimer = 6;

            ExtendedGame.AssetManager.PlaySoundEffect("Sounds/snd_watercollected");
        }
            
    }

    public override void Reset()
    {
        localPosition = startPosition;
        Visible = true;
        starTimer = 0;

        starCollected = false;
    }
}