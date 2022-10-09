using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils.GameObjects
{
    internal class SpeedPowerup : Powerup
    {
        public SpeedPowerup()
        {
            SetSpeedModifier(1);
            SetCapacityModifier(0);
            SetDamageModifier(0);
        }

    }
}
