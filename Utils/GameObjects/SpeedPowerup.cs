using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Utils.Math;

namespace Utils.GameObjects
{
    internal class SpeedPowerup : Powerup
    {
        public SpeedPowerup(Vector2 position, Vector2 size, Vector4 collider, Bitmap image) : base(position, size, collider, image)
        {
            SetSpeedModifier(1);
            SetCapacityModifier(0);
            SetDamageModifier(0);
        }

        public SpeedPowerup(int x, int y, int width, int height, int cx, int cy, int cWidth, int cHeight, Bitmap image)
            : base(x, y, width, height, cx, cy, cWidth, cHeight, image)
        {
            SetSpeedModifier(1);
            SetCapacityModifier(0);
            SetDamageModifier(0);
        }

    }
}
