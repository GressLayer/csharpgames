using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tetris
{
    class GameObject
    {
        protected Vector2 localPosition;
        protected Point gridPosition;
        protected int velocity;

        public GameObject Parent { get; set; }

        public bool Visible { get; set; }

        public GameObject()
        {
            LocalPosition = Vector2.Zero;
            GridPosition = Point.Zero;
            velocity = 128;
            Visible = true;
        }

        public virtual void HandleInput(InputHelper inputHelper)
        { }

        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {}

        public virtual void Reset()
        {
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
        public float LocalPositionX { get { return localPosition.X; } set { localPosition.X = value; } }
        public float LocalPositionY { get { return localPosition.Y; } set { localPosition.Y = value; } }
        public Point GridPosition { get { return gridPosition; } set { gridPosition = value; } }

    }


}
