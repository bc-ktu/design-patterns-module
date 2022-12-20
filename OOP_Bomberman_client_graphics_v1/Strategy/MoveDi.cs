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

namespace client_graphics.Strategy
{
    public class MoveDi : Moves
    {
        public void Move(Vector2 direction, int speed, Vector4 Collider, Vector2 LocalPosition, GameMap gameMap, Enemy enemy)
        {
            Vector2 upRight = new Vector2(1, -1);
            Vector2 downLeft = new Vector2(-1, 1);

            Enemy dummy = (Enemy)enemy.Clone();
            dummy.Move(direction);
            LookupTable dummyCollisions = GamePhysics.GetCollisions(dummy, gameMap);
            int dummyCollisionCount = dummyCollisions.Get<SolidGameObject>().Count;

            if (dummyCollisionCount >= 1 && direction == upRight)
            {
                direction = downLeft;
            }
            else if (dummyCollisionCount >= 1 && direction == downLeft)
            {
                direction = upRight;
            }
            enemy.Move(direction);
        }
    }
}
