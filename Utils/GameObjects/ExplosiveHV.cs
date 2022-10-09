using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Utils.Math;
using Utils.Helpers;

namespace Utils.GameObjects
{
    internal class ExplosiveHV : Explosive
    {
        public ExplosiveHV()
        {
            Vector2[] explosionDirections = new Vector2[]{
                Direction.Up,
                Direction.Right,
                Direction.Down,
                Direction.Left
            };

            SetExplosionDirections(explosionDirections);
        }
    }
}
