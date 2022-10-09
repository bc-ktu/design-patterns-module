using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Utils.GameObjects;

namespace Utils.AbstractFactory
{
    internal class Level1Factory : ILevelFactory
    {
        public Level1Factory() 
        { 
        
        }

        public GameObject CreateExplosive()
        {
            return new ExplosiveHV();
        }

        public GameObject CreateWall()
        {
            return new PaperWall();
        }

        public GameObject CreatePowerup()
        {
            return new SpeedPowerup();
        }
    }
}
