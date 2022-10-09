using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Powerup()
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
