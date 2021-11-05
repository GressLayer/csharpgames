using Microsoft.Xna.Framework;

namespace Engine
{

    public class Camera : GameObject
    {

        Rectangle cameraPosition;

        public int OffsetX { get; set; }
        public int OffsetY { get; set; }

        public Camera(int offsetX, int offsetY)
        {
            OffsetX = offsetX;
            OffsetY = offsetY;

            cameraPosition = new Rectangle(OffsetX, OffsetY, 1440, 825);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            localPosition = new Vector2(OffsetX, OffsetY);
        }

        public override void Reset()
        {
            OffsetX = 0;
            OffsetY = 0;
        }
    }
}
