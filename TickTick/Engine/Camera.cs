using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Engine
{
    public class Camera : GameObject
    {
        static Rectangle window;


        public Camera(int gridWidth, int gridHeight)
        {
            window = new Rectangle(0, 0, gridWidth, gridHeight);
        }

    }
}
