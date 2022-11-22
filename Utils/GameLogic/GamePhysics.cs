using Utils.GameObjects;
using Utils.GameObjects.Animates;
using Utils.GameObjects.Explosives;
using Utils.Helpers;
using Utils.Map;
using Utils.Math;

namespace Utils.GameLogic
{
    public static class GamePhysics
    {
        /// <summary>
        /// Returns a list of indexes on map where collisions are detected
        /// </summary>
        public static LookupTable GetCollisions(GameObject character, GameMap gameMap)
        {
            LookupTable collisions = new LookupTable();

            Vector2 characterIndex = character.WorldPosition / gameMap.TileSize;
            for (int y = -1; y <= 1; y++)
            {
                for (int x = -1; x <= 1; x++)
                {
                    Vector2 index = characterIndex + new Vector2(x, y);

                    if (index.X < 0 || index.X > gameMap.Size.X - 1 || index.Y < 0 || index.Y > gameMap.Size.Y - 1)
                        continue;

                    MapTile mapTile = gameMap[index];

                    if (mapTile.IsEmpty)
                        continue;

                    foreach (GameObject go in mapTile.GameObjects)
                        if (IsColliding(character, go))
                            collisions.Add(index, go);
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
            if (v.X >= go.Collider.X && v.Y >= go.Collider.Y)
            {
                if (v.X <= go.Collider.Z && v.Y <= go.Collider.W)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool IsDummyColliding(Vector2 direction, Player player, GameMap gameMap)
        {
            LookupTable playerCollisions = GamePhysics.GetCollisions(player, gameMap);
            bool playerIsOnExplosive = playerCollisions.Has<Explosive>();

            Player dummy = (Player)player.Clone();
            dummy.Move(direction);
            LookupTable dummyCollisions = GamePhysics.GetCollisions(dummy, gameMap);
            int dummyCollisionCount = dummyCollisions.Get<SolidGameObject>().Count;

            if (playerIsOnExplosive && dummyCollisionCount >= 2 || !playerIsOnExplosive && dummyCollisionCount >= 1)
                return true;

            return false;
        }

    }
}
