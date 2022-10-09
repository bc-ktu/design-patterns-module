using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils.GameObjects
{
    internal class DamagePowerup : Powerup
    {
        public DamagePowerup()
        {
            SetSpeedModifier(0);
            SetCapacityModifier(0);
            SetDamageModifier(1);
        }
    }
}
