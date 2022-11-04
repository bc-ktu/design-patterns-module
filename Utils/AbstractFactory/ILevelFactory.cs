using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Utils.GameObjects.Walls;
using Utils.GameObjects.Explosives;
using Utils.GameObjects.Interactables;
using Utils.Math;
using Utils.Map;

namespace Utils.AbstractFactory
{
    public interface ILevelFactory
    {
        public Explosive CreateExplosive(GameMap gameMap, Vector2 index);
        public Powerup CreatePowerup(GameMap gameMap, Vector2 index);
        public DestructableWall CreateWall(GameMap gameMap, Vector2 index);
    }
}
