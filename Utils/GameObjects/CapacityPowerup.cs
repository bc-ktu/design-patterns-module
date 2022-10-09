using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils.GameObjects
{
    internal class CapacityPowerup : Powerup
    {
        public CapacityPowerup()
        {
            SetSpeedModifier(0);
            SetCapacityModifier(1);
            SetDamageModifier(0);
        }
    }
}
