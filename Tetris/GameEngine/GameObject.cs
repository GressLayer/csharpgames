using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Tetris
{
    class GameObject
    {
        public Vector2 LocalPosition { get; set; }
        protected Vector2 velocity;

        public bool Visible { get; set; }

        public GameObject()
        {
            LocalPosition = Vector2.Zero;
            velocity = Vector2.Zero;
            Visible = true;
        }

        public virtual void HandleInput(InputHelper inputHelper)
        {}

        public virtual void Update(GameTime gameTime)
        {
            LocalPosition += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {}

        public virtual void Reset()
        {
            velocity = Vector2.Zero;
        }
    }


}
