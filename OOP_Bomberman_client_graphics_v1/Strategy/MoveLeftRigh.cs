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
    public class MoveLeftRigh : Moves
    {
        public void Move(Vector2 direction, int speed, Vector4 Collider, Vector2 LocalPosition, GameMap gameMap, Enemy player)
        {
            Vector2 left = new Vector2(-1, 0);
            Vector2 right = new Vector2(1, 0);

            Enemy dummy = (Enemy)player.Clone();
            LookupTable dummyCollisions = GamePhysics.GetCollisions(dummy, gameMap);

            int dummyCollisionCount = dummyCollisions.Get<SolidGameObject>().Count;

            if (dummyCollisionCount >= 1 && direction == left)
            {
                direction = right;
            }

            if(dummyCollisionCount >= 1 && direction == right)
            {
                direction = left;
            }

            Vector2 vPtoC = new Vector2(Collider.X, Collider.Y) - LocalPosition;
            Vector2 vTLtoBR = new Vector2(Collider.Z - Collider.X, Collider.W - Collider.Y);
            LocalPosition += speed * direction;
            int tlx = LocalPosition.X + vPtoC.X;
            int tly = LocalPosition.Y + vPtoC.Y;
            int brx = tlx + vTLtoBR.X;
            int bry = tly + vTLtoBR.Y;
            Collider = new Vector4(tlx, tly, brx, bry);
        }
    }
}
