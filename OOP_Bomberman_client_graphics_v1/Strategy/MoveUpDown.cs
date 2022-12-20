using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using client_graphics.GameLogic;
using client_graphics.GameObjects;
using client_graphics.GameObjects.Animates;
using client_graphics.Map;
using Utils.Math;
using client_graphics.Helpers;
using client_graphics.GameObjects.Explosives;

namespace client_graphics.Strategy
{
    public class MoveUpDown : Moves
    {
        public void Move(Vector2 direction, int speed, Vector4 Collider, Vector2 LocalPosition, GameMap gameMap, Enemy enemy)
        {
            Vector2 up = new Vector2(0, -1);
            Vector2 down = new Vector2(0, 1);

            Enemy dummy = (Enemy)enemy.Clone();
            dummy.Move(direction);
            LookupTable dummyCollisions = GamePhysics.GetCollisions(dummy, gameMap);
            int dummyCollisionCount = dummyCollisions.Get<SolidGameObject>().Count;

            if (dummyCollisionCount >= 1 && direction == up)
            {
                direction = down;
            }
            else if (dummyCollisionCount >= 1 && direction == down)
            {
                direction = up;
            }
            enemy.Move(direction);
        }
    }
}
