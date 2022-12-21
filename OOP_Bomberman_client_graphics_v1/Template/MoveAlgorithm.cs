using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using client_graphics.GameLogic;
using client_graphics.GameObjects;
using client_graphics.GameObjects.Animates;
using client_graphics.Helpers;
using client_graphics.Map;
using Utils.Helpers;
using Utils.Math;

namespace client_graphics.Template
{
    public abstract class MoveAlgorithm
    {
        public void Move(Vector2 direction, Enemy enemy, GameMap gameMap)
        {
            int collisionCount = GetCollisionCount(direction, enemy, gameMap);

            if (ShouldChangeToDirection1(collisionCount, direction))
                direction = GetDirection1();
            else if (ShouldChangeToDirection2(collisionCount, direction))
                direction = GetDirection2();

            PerformMove(enemy, direction);
        }

        public int GetCollisionCount(Vector2 direction, Enemy enemy, GameMap gameMap)
        {
            Enemy dummy = (Enemy)enemy.Clone();
            dummy.Move(direction);
            LookupTable dummyCollisions = GamePhysics.GetCollisions(dummy, gameMap);
            return dummyCollisions.Get<SolidGameObject>().Count;
        }

        public abstract bool ShouldChangeToDirection1(int collisionCount, Vector2 direction);
        public abstract Vector2 GetDirection1();
        public abstract bool ShouldChangeToDirection2(int collisionCount, Vector2 direction);
        public abstract Vector2 GetDirection2();

        public void PerformMove(Enemy enemy, Vector2 direction)
        {
            enemy.Move(direction);
        }

    }
}
