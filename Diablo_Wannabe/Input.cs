using System.Linq;
using Microsoft.Xna.Framework.Input;

namespace Diablo_Wannabe
{
    public class Input
    {
        private KeyboardState keyState;
        private KeyboardState prevKeyState;
        private MouseState mouseState;
        private MouseState previousMouseState;

        public MouseState MouseState
        {
            get { return this.mouseState; }
            set { this.mouseState = value; }
        }

        public MouseState PreviousMouseState
        {
            get { return this.mouseState; }
            set { this.mouseState = value; }
        }

        private static Input instance;

        public static Input Instance = instance ?? (instance = new Input());

        private Input()
        {
            this.keyState = Keyboard.GetState();
            this.mouseState = Mouse.GetState();
        }

        public void Update()
        {
            prevKeyState = keyState;
            previousMouseState = mouseState;
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
    }
}