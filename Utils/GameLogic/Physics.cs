using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Utils.GameObjects;
using Utils.Math;

namespace Utils.GameLogic
{
    public static class Physics
    {
        /// <summary>
        /// Returns a list of indexes on map where collisions are detected
        /// </summary>
        public static List<Vector2> GetCollisions(Character character, Map gameMap)
        {
            List<Vector2> collisions = new List<Vector2>();

            Vector2 characterIndex = character.WorldPosition / gameMap.TileSize;
            for (int y = -1; y <= 1; y++)
            {
                for (int x = -1; x <= 1; x++)
                {
                    Vector2 index = characterIndex + new Vector2(x, y);
                    GameObject otherGO = gameMap.Tiles[index.X, index.Y].GameObject;

                    if (otherGO is EmptyGameObject)
                        continue;

                    if (IsColliding(character, otherGO))
                        collisions.Add(index);
                }
            }

            return collisions;
        }

        public static bool IsColliding(GameObject go1, GameObject go2)
        {
            Vector2 tl = new Vector2(go1.Collider.X, go1.Collider.Y);
            Vector2 tr = new Vector2(go1.Collider.Z, go1.Collider.Y);
            Vector2 br = new Vector2(go1.Collider.Z, go1.Collider.W);
            Vector2 bl = new Vector2(go1.Collider.X, go1.Collider.W);

            bool p1Collides = IsColliding(tl, go2);
            bool p2Collides = IsColliding(tr, go2);
            bool p3Collides = IsColliding(br, go2);
            bool p4Collides = IsColliding(bl, go2);

            return p1Collides || p2Collides || p3Collides || p4Collides;
        }

        public static bool IsColliding(Vector2 v, GameObject go)
        {
            if (v.X > go.Collider.X && v.Y > go.Collider.Y)
            {
                if (v.X < go.Collider.Z && v.Y < go.Collider.W)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
