using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Utils.GameObjects;

namespace Utils.AbstractFactory
{
    internal class Level3Factory : ILevelFactory
    {
        public Level3Factory()
        {

        }

        public GameObject CreateExplosive()
        {
            return new ExplosiveHVDi();
        }

        public GameObject CreatePowerup()
        {
            return new DamagePowerup();
        }

        public GameObject CreateWall()
        {
            return new StoneWall();
        }
    }
}
