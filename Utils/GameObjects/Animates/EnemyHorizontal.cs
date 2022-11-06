using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Math;
using Utils.Strategy;

namespace Utils.GameObjects.Animates
{
    public class EnemySideWays : Enemy
    {
        public Vector2 direction { get; private set; }
        public EnemySideWays()
        {
            direction = new Vector2(-1, 0);
            movingType = new MoveLeftRigh();
        }
    }
}
