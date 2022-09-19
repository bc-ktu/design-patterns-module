using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Bomberman_client_graphics_v1
{
    internal static class Settings
    {
        // Defaults
        public static Keys DefaultKeyUp { get { return Keys.W; } }
        public static Keys DefaultKeyDown { get { return Keys.S; } }
        public static Keys DefaultKeyRight { get { return Keys.D; } }
        public static Keys DefaultKeyLeft { get { return Keys.A; } }
        public static Keys DefaultKeyBomb { get { return Keys.Space; } }

        public static Vector2 DefaultPlayerDirection { get { return Direction.Down; } }
        public static int DefaultPlayerSpeed { get { return 7; } }
        public static int DefaultPlayerCapacity { get { return 3; } }
        public static int DefaultPlayerHealth { get { return 3; } }
        public static int DefaultPlayerRange { get { return 1; } }

        // Private fields
        private static Keys _keyUp = DefaultKeyUp;
        private static Keys _keyDown = DefaultKeyDown;
        private static Keys _keyRight = DefaultKeyRight;
        private static Keys _keyLeft = DefaultKeyLeft;
        private static Keys _keyBomb = DefaultKeyBomb;

        private static Vector2 _facing = DefaultPlayerDirection;
        private static int _speed = DefaultPlayerSpeed;
        private static int _capacity = DefaultPlayerCapacity;
        private static int _range = DefaultPlayerRange;
        private static int _health = DefaultPlayerHealth;

        // Properties
        public static Keys KeyUp { get { return _keyUp; } set { _keyUp = value; } }
        public static Keys KeyDown { get { return _keyDown; } set { _keyDown = value; } }
        public static Keys KeyRight { get { return _keyRight; } set { _keyRight = value; } }
        public static Keys KeyLeft { get { return _keyLeft; } set { _keyLeft = value; } }
        public static Keys KeyBomb { get { return _keyBomb; } set { _keyBomb = value; } }

        public static Vector2 InitialPlayerDirection { get { return _facing; } set { _facing = value; } }
        public static int InitialPlayerSpeed { get { return _speed; } set { _speed = value; } }
        public static int InitialPlayerCapacity { get { return _capacity; } set { _capacity = value; } }
        public static int InitialPlayerRange { get { return _range; } set { _range = value; } }
        public static int InitialPlayerHealth { get { return _health; } set { _health = value; } }

        //public static Dictionary<Keys, Vector2> KeyAction = new Dictionary<Keys, Vector2>
        //{
        //    { KeyUp, Direction.Up },
        //    { KeyDown, Direction.Down },
        //    [ Key]
        //};

    }
}
