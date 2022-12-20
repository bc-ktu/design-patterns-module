using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using client_graphics.GameObjects.Animates;
using client_graphics.Map;
using Utils.Math;

namespace client_graphics.Strategy
{
    public interface Moves
    {
        void Move(Vector2 direction, int speed, Vector4 Collider, Vector2 LocalPosition, GameMap gameMap, Enemy player);
    }
}
