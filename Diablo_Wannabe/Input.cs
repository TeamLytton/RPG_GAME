using System.Linq;
using Microsoft.Xna.Framework.Input;

namespace Diablo_Wannabe
{
    public class Input
    {
        private KeyboardState keyState;
        private KeyboardState prevKeyState;
        private MouseState mouseState;
        private MouseState prevMouseState;

        private static Input manager;

        public static Input Manager = manager ?? (manager = new Input());

        private Input()
        {
            this.keyState = Keyboard.GetState();
            this.mouseState = Mouse.GetState();
        }

        public void Update()
        {
            prevKeyState = keyState;
            prevMouseState = mouseState;
            keyState = Keyboard.GetState();
            mouseState = Mouse.GetState();
        }

        public bool KeyPressed(params Keys[] keys)
        {
            return keys.Any(key => keyState.IsKeyDown(key) && prevKeyState.IsKeyUp(key));
        }

        public bool KeyReleased(params Keys[] keys)
        {
            return keys.Any(key => keyState.IsKeyUp(key) && prevKeyState.IsKeyDown(key));
        }

        public bool KeyDown(params Keys[] keys)
        {
            return keys.Any(key => keyState.IsKeyDown(key));
        }

        public bool MousePressed(params Buttons[] buttons)
        {
            return buttons.Any(button => mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released
                                    || mouseState.RightButton == ButtonState.Pressed && prevMouseState.RightButton == ButtonState.Released);
        }

        public bool MouseReleased(params Buttons[] buttons)
        {
            return buttons.Any(button => mouseState.LeftButton == ButtonState.Released && prevMouseState.LeftButton == ButtonState.Pressed
                                    || mouseState.RightButton == ButtonState.Released && prevMouseState.RightButton == ButtonState.Pressed);
        }

        public bool MouseDown(params Buttons[] buttons)
        {
            return buttons.Any(button => mouseState.LeftButton == ButtonState.Pressed || mouseState.RightButton == ButtonState.Pressed);
        }

    }
}