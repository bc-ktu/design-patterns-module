using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client_graphics
{
    internal static class Input
    {
        public static Keys DefaultKeyUp { get { return Keys.W; } }
        public static Keys DefaultKeyDown { get { return Keys.S; } }
        public static Keys DefaultKeyRight { get { return Keys.D; } }
        public static Keys DefaultKeyLeft { get { return Keys.A; } }
        public static Keys DefaultKeyBomb { get { return Keys.Space; } }
        public static Keys DefaultKeyInteract { get { return Keys.E; } }

        private static Keys _keyUp = DefaultKeyUp;
        private static Keys _keyDown = DefaultKeyDown;
        private static Keys _keyRight = DefaultKeyRight;
        private static Keys _keyLeft = DefaultKeyLeft;
        private static Keys _keyBomb = DefaultKeyBomb;
        private static Keys _keyInteract = DefaultKeyInteract;

        public static Keys KeyUp { get { return _keyUp; } set { _keyUp = value; } }
        public static Keys KeyDown { get { return _keyDown; } set { _keyDown = value; } }
        public static Keys KeyRight { get { return _keyRight; } set { _keyRight = value; } }
        public static Keys KeyLeft { get { return _keyLeft; } set { _keyLeft = value; } }
        public static Keys KeyBomb { get { return _keyBomb; } set { _keyBomb = value; } }
        public static Keys KeyInteract { get { return _keyInteract; } set { _keyInteract = value; } }
    }
}
