using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tetris
{
    class GameObject
    {
        protected Vector2 localPosition;
        protected Vector2 velocity;
        public GameObject Parent { get; set; }

        public bool Visible { get; set; }

        public GameObject()
        {
            LocalPosition = Vector2.Zero;
            velocity = new Vector2(0, 128);
            Visible = true;
        }

        public virtual void HandleInput(InputHelper inputHelper)
        { }

        public virtual void Update(GameTime gameTime)
        {
            if (gameTime.ElapsedGameTime.TotalSeconds % 32 == 0)
                localPosition.Y += 32;
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {}

        public virtual void Reset()
        {
            velocity = Vector2.Zero;
        }

        public Vector2 GlobalPosition
        {
            get
            {
                if (Parent == null)
                    return LocalPosition;
                return LocalPosition + Parent.GlobalPosition;
            }
        }

        public Vector2 LocalPosition { get { return localPosition; } set { localPosition = value; } }

    }


}
