using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Utils.Math;

namespace Utils.GameObjects.Interactables
{
    internal class RangePowerup : Powerup
    {
        public RangePowerup()
        {
            Initialize();
        }

        public RangePowerup(RangePowerup rp) : base(rp) { }

        public RangePowerup(Vector2 position, Vector2 size, Vector4 collider, Bitmap image) 
            : base(position, size, collider, image)
        {
            Initialize();
        }

        public RangePowerup(int x, int y, int width, int height, int cx, int cy, int cWidth, int cHeight, Bitmap image)
            : base(x, y, width, height, cx, cy, cWidth, cHeight, image)
        {
            Initialize();
        }

        private void Initialize()
        {
            SpeedModifier = 0;
            CapacityModifier = 0;
            DamageModifier = 0;
            RangeModifier = 1;
        }

        public override GameObject Clone()
        {
            return new RangePowerup(this);
        }

    }
}
