using System;
using Microsoft.Xna.Framework;
using Engine;

class Shoe : SpriteGameObject
{
    Level level;
    protected float bounce;
    Vector2 startPosition;

    public static double shoeTimer;
    public static bool shoeCollected;

    public Shoe(Level level, Vector2 startPosition) : base("Sprites/LevelObjects/spr_shoe", TickTick.Depth_LevelObjects)
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

        // check if the player collects this water drop
        if (Visible && level.Player.CanCollideWithObjects && HasPixelPreciseCollision(level.Player))
        {
            Visible = false;

            shoeCollected = true;

            // Gain a 5-second speed boost upon collecting a shoe.
            if (shoeCollected)
                shoeTimer = 5;

            ExtendedGame.AssetManager.PlaySoundEffect("Sounds/snd_watercollected");
        }
            
    }

    public override void Reset()
    {
        localPosition = startPosition;
        Visible = true;
        shoeTimer = 0;

        shoeCollected = false;
    }
}