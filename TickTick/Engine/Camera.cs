using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Engine
{
    public class Camera : GameObject
    {
        static Rectangle viewWindow;

        int offsetX;


        public Camera()
        {
            viewWindow = new Rectangle(0, 0, 1024, 768);
        }

    }
}
