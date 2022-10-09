using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Utils.GameObjects;

namespace Utils.AbstractFactory
{
    internal class Level2Factory : ILevelFactory
    {
        public Level2Factory()
        {

        }

        public GameObject CreateExplosive()
        {
            return new ExplosiveDi();
        }

        public GameObject CreatePowerup()
        {
            return new CapacityPowerup();
        }

        public GameObject CreateWall()
        {
            return new WoodenWall();
        }
    }
}
