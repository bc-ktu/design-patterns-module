using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Utils.Math;
using Utils.Helpers;

namespace Utils.GameObjects
{
    internal class ExplosiveDi : Explosive
    {
        public ExplosiveDi()
        {
            Vector2[] explosionDirections = new Vector2[]{
                Direction.UpRight,
                Direction.DownRight,
                Direction.DownLeft,
                Direction.UpLeft
            };

            SetExplosionDirections(explosionDirections);
        }
    }
}
