using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Engine
{
    public class Camera : GameObject
    {

        Rectangle cameraPosition;

        public int OffsetX { get; private set; }
        public int OffsetY { get; private set; }

        public Camera(int offsetX, int offsetY)
        {
            OffsetX = offsetX;
            OffsetY = offsetY;
            cameraPosition = new Rectangle(OffsetX, OffsetY, 5, 5);
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            if (inputHelper.KeyDown(Keys.A))
                OffsetX -= 10;
            if (inputHelper.KeyDown(Keys.D))
                OffsetX += 10;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (OffsetX <= 0)
                OffsetX = 0;

            localPosition = new Vector2(OffsetX, OffsetY);
        }

        public override void Reset()
        {
            OffsetX = 0;
            OffsetY = 0;
        }
    }
}
