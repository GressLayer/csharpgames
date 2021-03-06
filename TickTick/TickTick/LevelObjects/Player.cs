using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

class Player : AnimatedGameObject
{
    const float walkingSpeed = 400; // Standard walking speed, in game units per second.
    const float jumpSpeed = 900; // Lift-off speed when the character jumps.

    const float gravity = 2300; // Strength of the gravity force that pulls the character down.
    const float maxFallSpeed = 1200; // The maximum vertical speed at which the character can fall.

    const float damageSpeedX = 600; // Small "kickback" while taking damage.
    const float damageSpeedY = 650; // Small "jump" while taking damage.

    const float iceFriction = 1; // Friction factor that determines how slippery the ice is; closer to 0 means more slippery.
    const float normalFriction = 20; // Friction factor that determines how slippery a normal surface is.
    const float airFriction = 5; // Friction factor that determines how much (horizontal) air resistance there is.

    bool facingLeft; // Whether or not the character is currently looking to the left.

    bool isGrounded; // Whether or not the character is currently standing on something.
    bool standingOnIceTile, standingOnHotTile, standingOnGooTile; // Whether or not the character is standing on an ice tile or a hot tile.
    bool standingOnPlatform;

    float desiredHorizontalSpeed; // The horizontal speed at which the character would like to move.

    Level level;
    Vector2 startPosition;

    double damageTimer;
    bool canTakeDamage;

    bool isCelebrating; // Whether or not the player is celebrating a level victory.
    bool isExploding;

    bool ghost; // Added as a means to ignore collision.

    public static int health = 3;

    public bool IsAlive { get; private set; }
    public bool IsGrounded { get { return isGrounded; } }
    public bool CanCollideWithObjects { get { return IsAlive && !isCelebrating && !ghost; } }
    public bool CanTakeDamage { get { return IsAlive && canTakeDamage && !isCelebrating; } }
    public bool IsMoving { get { return velocity != Vector2.Zero; } }

    public Player(Level level, Vector2 startPosition) : base(TickTick.Depth_LevelPlayer)
    {
        this.level = level;
        this.startPosition = startPosition;

        ghost = false;

        // load all animations
        LoadAnimation("Sprites/LevelObjects/Player/spr_idle", "idle", true, 0.1f);
        LoadAnimation("Sprites/LevelObjects/Player/spr_run@13", "run", true, 0.04f);
        LoadAnimation("Sprites/LevelObjects/Player/spr_jump@14", "jump", false, 0.08f);
        LoadAnimation("Sprites/LevelObjects/Player/spr_celebrate@14", "celebrate", false, 0.05f);
        LoadAnimation("Sprites/LevelObjects/Player/spr_die@5", "die", true, 0.1f);
        LoadAnimation("Sprites/LevelObjects/Player/spr_explode@5x5", "explode", false, 0.04f);

        Reset();
    }

    public override void Reset()
    {
        // go back to the starting position
        localPosition = startPosition;
        velocity = Vector2.Zero;
        desiredHorizontalSpeed = 0;

        // start with the idle sprite
        PlayAnimation("idle", true);
        SetOriginToBottomCenter();
        facingLeft = false;
        isGrounded = true;
        standingOnIceTile = standingOnHotTile = standingOnGooTile = false;
        standingOnPlatform = false;

        health = 3;
        Shoe.shoeCollected = false;

        IsAlive = true;
        isExploding = false;
        isCelebrating = false;

        ghost = false;

        canTakeDamage = true;
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        if (!CanCollideWithObjects)
            return;

        // arrow keys: move left or right
        if (inputHelper.KeyDown(Keys.Left))
        {
            facingLeft = true;
            desiredHorizontalSpeed = -walkingSpeed;
            if (isGrounded)
                PlayAnimation("run");
        }
        else if (inputHelper.KeyDown(Keys.Right))
        {
            facingLeft = false;
            desiredHorizontalSpeed = walkingSpeed;
            if (isGrounded)
                PlayAnimation("run");
        }

        // no arrow keys: don't move
        else
        {
            desiredHorizontalSpeed = 0;
            if (isGrounded)
                PlayAnimation("idle");
        }

        // spacebar: jump
        if (isGrounded && inputHelper.KeyPressed(Keys.Space))
        {
            // Jump higher after collecting a shoe.
            if (Shoe.shoeCollected && Shoe.shoeTimer > 0)
                Jump(jumpSpeed * 1.3f);
            else
                Jump();
        }

        // falling?
        if (!isGrounded)
            PlayAnimation("jump", false, 8);

        // set the origin to the character's feet
        SetOriginToBottomCenter();

        // make sure the sprite is facing the correct direction
        sprite.Mirror = facingLeft;
    }

    public void Jump(float speed = jumpSpeed)
    {
        velocity.Y = -speed;
        // Play the jump animation; always make sure that the animation restarts
        PlayAnimation("jump", true);
        // Play a sound
        ExtendedGame.AssetManager.PlaySoundEffect("Sounds/snd_player_jump");
    }

    // Returns whether or not the Player is currently falling down.
    public bool IsFalling
    {
        get { return velocity.Y > 0 && !isGrounded; }
    }

    void SetOriginToBottomCenter()
    {
        Origin = new Vector2(sprite.Width / 2, sprite.Height);
    }

    public override void Update(GameTime gameTime)
    {
        Vector2 previousPosition = localPosition;

        // Damage countdown after TickTick gets hit.
        if (damageTimer > 0)
        {
            damageTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (damageTimer <= 0)
            {
                canTakeDamage = true;
            }
        }

        // Timer for the shoe item.
        if (Shoe.shoeTimer > 0)
        {
            Shoe.shoeTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (Shoe.shoeTimer <= 0)
            {
                Shoe.shoeCollected = false;
            }
        }

        // Timer for the stopwatch item.
        if (Stopwatch.stopTimer > 0)
        {
            Stopwatch.stopTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (Stopwatch.stopTimer <= 0)
            {
                Stopwatch.stopwatchCollected = false;
            }
        }

        // Timer for the star item.
        if (Star.starTimer > 0)
        {
            canTakeDamage = false;

            Star.starTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (Star.starTimer <= 0)
            {
                Star.starCollected = false;
                canTakeDamage = true;
            }
        }

        // Goo tiles make you slower.
        if (standingOnGooTile)
            desiredHorizontalSpeed /= 2;

        // Run faster with a shoe collected.
        if (Shoe.shoeCollected && Shoe.shoeTimer > 0)
            desiredHorizontalSpeed *= 1.5f;

        // Camera horizontal scrolling
        if (ExtendedGame.camera.OffsetX >= 0)
            if (localPosition.X <= (480) + ExtendedGame.camera.OffsetX && facingLeft)
                ExtendedGame.camera.OffsetX += (int)velocity.X * (int)gameTime.ElapsedGameTime.TotalMilliseconds / 1000;
        if (ExtendedGame.camera.OffsetX <= level.BoundingBox.Width - 1440)
            if (localPosition.X >= (960) + ExtendedGame.camera.OffsetX && !facingLeft)
                ExtendedGame.camera.OffsetX += (int)velocity.X * (int)gameTime.ElapsedGameTime.TotalMilliseconds / 1000;

        // Camera vertical scrolling
        if (ExtendedGame.camera.OffsetY >= 0 && ExtendedGame.camera.OffsetY <= level.BoundingBox.Width + 825)
            ExtendedGame.camera.OffsetY = (int)localPosition.Y - 400;

        // Scrolling caps: make sure the camera does not overscroll past the edge blocks of the level.
        if (ExtendedGame.camera.OffsetX <= 0)
            ExtendedGame.camera.OffsetX = 0;
        if (ExtendedGame.camera.OffsetX >= level.BoundingBox.Width - 1440)
            ExtendedGame.camera.OffsetX = level.BoundingBox.Width - 1440;
        if (ExtendedGame.camera.OffsetY <= 0)
            ExtendedGame.camera.OffsetY = 0;
        if (ExtendedGame.camera.OffsetY >= level.BoundingBox.Height - 825)
            ExtendedGame.camera.OffsetY = level.BoundingBox.Height - 825;

        if (CanCollideWithObjects)
            ApplyFriction(gameTime);
        else
            velocity.X = 0;

        if (!isExploding)
            ApplyGravity(gameTime);

        base.Update(gameTime);

        if (IsAlive)
        {
            // check for collisions with tiles
            HandleTileCollisions(previousPosition);
            // check if we've fallen down through the level
            if (BoundingBox.Center.Y > level.BoundingBox.Bottom)
                Die();

            if (standingOnHotTile)
                level.Timer.Multiplier = 2;
            else
                level.Timer.Multiplier = 1;
        }
            
    }

    void ApplyFriction(GameTime gameTime)
    {
        // determine the friction coefficient for the character
        float friction;
        if (standingOnIceTile)
            friction = iceFriction;
        else if (isGrounded)
            friction = normalFriction;
        else
            friction = airFriction;

        // calculate how strongly the horizontal speed should move towards the desired value
        float multiplier = MathHelper.Clamp(friction * (float)gameTime.ElapsedGameTime.TotalSeconds, 0, 1);

        // update the horizontal speed
        velocity.X += (desiredHorizontalSpeed - velocity.X) * multiplier;
        if (Math.Abs(velocity.X) < 1)
            velocity.X = 0;
    }

    void ApplyGravity(GameTime gameTime)
    {
        velocity.Y += gravity * (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (velocity.Y > maxFallSpeed)
            velocity.Y = maxFallSpeed;
    }

    // Checks for collisions between the character and the level's tiles, and handles these collisions when needed.
    void HandleTileCollisions(Vector2 previousPosition)
    {
        isGrounded = false;
        standingOnIceTile = false;
        standingOnHotTile = false;
        standingOnGooTile = false;
        standingOnPlatform = false;

        // determine the range of tiles to check
        Rectangle bbox = BoundingBoxForCollisions;
        Point topLeftTile = level.GetTileCoordinates(new Vector2(bbox.Left, bbox.Top)) - new Point(1, 1);
        Point bottomRightTile = level.GetTileCoordinates(new Vector2(bbox.Right, bbox.Bottom)) + new Point(1, 1);

        for (int y = topLeftTile.Y; y <= bottomRightTile.Y; y++)
        {
            for (int x = topLeftTile.X; x <= bottomRightTile.X; x++)
            {
                Tile.Type tileType = level.GetTileType(x, y);

                // ignore empty tiles
                if (tileType == Tile.Type.Empty)
                    continue;

                // ignore platform tiles if the player is standing below them
                Vector2 tilePosition = level.GetCellPosition(x, y);
                if (tileType == Tile.Type.Platform && localPosition.Y > tilePosition.Y && previousPosition.Y > tilePosition.Y)
                {
                    standingOnPlatform = true;
                    continue;
                }

                // if there's no intersection with the tile, ignore this tile
                Rectangle tileBounds = new Rectangle((int)tilePosition.X, (int)tilePosition.Y, Level.TileWidth, Level.TileHeight);
                if (!tileBounds.Intersects(bbox))
                    continue;

                // calculate how large the intersection is
                Rectangle overlap = CollisionDetection.CalculateIntersection(bbox, tileBounds);

                // if the x-component is smaller, treat this as a horizontal collision
                if (overlap.Width < overlap.Height)
                {
                    if ((velocity.X >= 0 && bbox.Center.X < tileBounds.Left) || // right wall
                        (velocity.X <= 0 && bbox.Center.X > tileBounds.Right)) // left wall
                    {
                        localPosition.X = previousPosition.X;
                        velocity.X = 0;
                    }
                }

                // otherwise, treat this as a vertical collision
                else
                {
                    if (velocity.Y >= 0 && bbox.Center.Y < tileBounds.Top && overlap.Width > 6) // floor
                    {
                        isGrounded = true;
                        velocity.Y = 0;
                        localPosition.Y = tileBounds.Top;

                        // check the surface type: are we standing on a hot tile or an ice tile?
                        Tile.SurfaceType surface = level.GetSurfaceType(x, y);
                        if (surface == Tile.SurfaceType.Hot)
                            standingOnHotTile = true;
                        else if (surface == Tile.SurfaceType.Ice)
                            standingOnIceTile = true;
                        else if (surface == Tile.SurfaceType.Goo)
                            standingOnGooTile = true;
                    }
                    else if (velocity.Y <= 0 && bbox.Center.Y > tileBounds.Bottom && overlap.Height > 2) // ceiling
                    {
                        localPosition.Y = previousPosition.Y;
                        velocity.Y = 0;
                    }
                }
            }
        }
    }

    Rectangle BoundingBoxForCollisions
    {
        get
        {
            Rectangle bbox = BoundingBox;
            // adjust the bounding box
            bbox.X += 20;
            bbox.Width -= 40;
            bbox.Height += 1;

            return bbox;
        }
    }

    public void TakeDamage()
    {
        if (canTakeDamage)
        {
            if (health > 0)
            {
                if (facingLeft)
                    velocity = new Vector2(damageSpeedX, -damageSpeedY);
                if (!facingLeft)
                    velocity = new Vector2(-damageSpeedX, -damageSpeedY);
            }

            canTakeDamage = false;
            health--;
            damageTimer = 0.8;
        }

        if (damageTimer > 0)
            PlayAnimation("die");

        if (health <= 0)
            Die();
    }

    public void Die()
    {
        IsAlive = false;
        health = 0;
        PlayAnimation("die");
        velocity = new Vector2(0, -jumpSpeed);
        level.Timer.Running = false;

        ExtendedGame.AssetManager.PlaySoundEffect("Sounds/snd_player_die");
    }

    public void Explode()
    {
        IsAlive = false;
        isExploding = true;
        PlayAnimation("explode");
        velocity = Vector2.Zero;

        ExtendedGame.AssetManager.PlaySoundEffect("Sounds/snd_player_explode");
    }

    /// <summary>
    /// Lets this Player object start celebrating due to completing a level.
    /// The Player will show an animation, and it will stop responding to keyboard input.
    /// </summary>
    public void Celebrate()
    {
        isCelebrating = true;
        PlayAnimation("celebrate");
        SetOriginToBottomCenter();

        // stop moving
        velocity = Vector2.Zero;
    }
}