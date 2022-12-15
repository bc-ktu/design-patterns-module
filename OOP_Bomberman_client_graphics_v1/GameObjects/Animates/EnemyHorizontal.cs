using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Math;
using client_graphics.Strategy;

namespace client_graphics.GameObjects.Animates
{
    public class EnemySideWays : Enemy
    {
        public Vector2 direction { get; private set; }
        public EnemySideWays()
        {
            direction = new Vector2(1, -1);
            movingType = new MoveDi();
        }
    }
}
