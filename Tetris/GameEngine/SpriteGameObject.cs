using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Tetris
{
    class SpriteGameObject : GameObject
    {
        protected Texture2D sprite;
        protected Vector2 origin;

        protected Rectangle boundingBox;

        public SpriteGameObject(string spriteName)
        {
            sprite = ExtendedGame.ContentManager.Load<Texture2D>(spriteName);
            origin = Vector2.Zero;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (Visible)
            {
                spriteBatch.Draw(sprite, GlobalPosition, null, Color.White, 0, origin, 1.0f, SpriteEffects.None, 0);
            }
        }    
        public virtual Rectangle BoundingBox
        {
            get
            {
                boundingBox = sprite.Bounds;
                boundingBox.Offset(GlobalPosition - origin);
                return boundingBox;
            }
        }
    }

}
