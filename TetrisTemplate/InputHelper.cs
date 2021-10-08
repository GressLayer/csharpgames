using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Tetris
{
    /// <summary>
    /// A class for helping out with input-related tasks, such as checking if a key or mouse button has been pressed.
    /// </summary>
    class InputHelper
    {
        // The current and previous mouse/keyboard states.
        MouseState mouseCurrent, mousePrev;
        KeyboardState keyboardCurrent, keyboardPrev;

        // Updates the InputHelper object by retrieving the new mouse/keyboard state, and keeping the previous state as a back-up.
        public void Update(GameTime gameTime)
        {
            // update the mouse and keyboard states
            mousePrev = mouseCurrent;
            keyboardPrev = keyboardCurrent;
            mouseCurrent = Mouse.GetState();
            keyboardCurrent = Keyboard.GetState();
        }

        // Gets the current position of the mouse cursor.
        public Vector2 MousePosition
        {
            get { return new Vector2(mouseCurrent.X, mouseCurrent.Y); }
        }

        // Returns whether or not the left mouse button has just been pressed.
        public bool MouseLeftButtonPressed()
        {
            return mouseCurrent.LeftButton == ButtonState.Pressed && mousePrev.LeftButton == ButtonState.Released;
        }

        // Returns whether or not a given keyboard key has just been pressed.
        public bool KeyPressed(Keys k)
        {
            return keyboardCurrent.IsKeyDown(k) && keyboardPrev.IsKeyUp(k);
        }

        // Returns whether or not a given keyboard key is currently being held down.
        public bool KeyDown(Keys k)
        {
            return keyboardCurrent.IsKeyDown(k);
        }
    }
}