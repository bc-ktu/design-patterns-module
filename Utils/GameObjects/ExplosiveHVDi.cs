using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Utils.Math;
using Utils.Helpers;

namespace Utils.GameObjects
{
    public class ExplosiveHVDi : Explosive
    {
        public ExplosiveHVDi()
        {
            Vector2[] explosionDirections = new Vector2[]{
                Direction.Up,
                Direction.UpRight,
                Direction.Right,
                Direction.DownRight,
                Direction.Down,
                Direction.DownLeft,
                Direction.Left,
                Direction.UpLeft
            };

            SetExplosionDirections(explosionDirections);
        }
    }
}
