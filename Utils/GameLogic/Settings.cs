using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Utils.Helpers;
using Utils.Math;

namespace Utils.GameLogic
{
    public static class Settings
    {
        // Defaults
        public static Vector2 DefaultPlayerDirection { get { return Direction.Down; } }
        public static int DefaultPlayerHealth { get { return 3; } }
        public static int DefaultPlayerSpeed { get { return 7; } }
        public static int DefaultPlayerCapacity { get { return 3; } }
        public static int DefaultExplosionRange { get { return 3; } }
        public static int DefaultExplosionDamage { get { return 1; } }

        public static int DefaultTimeTillExplosion { get { return 3; } } // s -> tick

        // Private fields
        private static Vector2 _facing = DefaultPlayerDirection;
        private static int _health = DefaultPlayerHealth;
        private static int _speed = DefaultPlayerSpeed;
        private static int _capacity = DefaultPlayerCapacity;
        private static int _range = DefaultExplosionRange;
        private static int _damage = DefaultExplosionDamage;

        private static int _tileTillExposion = DefaultTimeTillExplosion;

        // Properties
        public static Vector2 InitialPlayerDirection { get { return _facing; } set { _facing = value; } }
        public static int InitialPlayerHealth { get { return _health; } set { _health = value; } }
        public static int InitialPlayerSpeed { get { return _speed; } set { _speed = value; } }
        public static int InitialPlayerCapacity { get { return _capacity; } set { _capacity = value; } }
        public static int InitialExplosionRange { get { return _range; } set { _range = value; } }
        public static int InitialExplosionDamage { get { return _damage; } set { _damage = value; } }

        public static int InitialTimeTillExplosion { get { return _tileTillExposion; } set { _tileTillExposion = value; } }
    }
}
