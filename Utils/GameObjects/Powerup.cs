using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Utils.Math;

namespace Utils.GameObjects
{
    internal abstract class Powerup : GameObject
    {
        private int _speedModifier;
        private int _capacityModifier;
        private int _damageModifier;

        public int SpeedModifier { get { return _speedModifier; } }
        public int CapacityModifier { get { return _capacityModifier; } }
        public int DamageModifier { get { return _damageModifier; } }

        public Powerup(Vector2 position, Vector2 size, Vector4 collider, Bitmap image) : base(position, size, collider, image)
        {

        }

        public Powerup(int x, int y, int width, int height, int cx, int cy, int cWidth, int cHeight, Bitmap image)
            : base(x, y, width, height, cx, cy, cWidth, cHeight, image)
        {

        }

        protected void SetSpeedModifier(int speedModifier)
        {
            _speedModifier = speedModifier;
        }

        protected void SetCapacityModifier(int capacityModifier)
        {
            _capacityModifier = capacityModifier;
        }

        protected void SetDamageModifier(int damageModifier)
        {
            _damageModifier = damageModifier;
        }

        public void Affect(Character character)
        {
            throw new NotImplementedException();
        }

    }
}
