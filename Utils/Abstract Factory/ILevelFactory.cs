using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Utils.GameObjects;

namespace Utils.AbstractFactory
{
    internal interface ILevelFactory
    {
        public GameObject CreateExplosive();
        public GameObject CreateWall();
        public GameObject CreatePowerup();
    }
}
