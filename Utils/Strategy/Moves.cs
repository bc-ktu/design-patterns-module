using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.GameObjects.Animates;
using Utils.Map;
using Utils.Math;

namespace Utils.Strategy
{
    public interface Moves
    {
        void Move(Vector2 direction, int speed, Vector4 Collider, Vector2 LocalPosition, GameMap gameMap, Enemy player);
    }
}
