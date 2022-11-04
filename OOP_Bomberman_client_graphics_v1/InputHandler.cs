using Microsoft.AspNetCore.SignalR.Client;

using client_graphics.SignalR;
using Utils.Helpers;
using Utils.GameObjects.Animates;
using Utils.Map;
using Utils.AbstractFactory;
using Utils.Math;
using Utils.GameLogic;
using Utils.GameObjects;
using Utils.GameObjects.Explosives;

namespace client_graphics
{
    internal static class InputHandler
    {
        public static void HandleKey(Keys key, Player player, GameMap gameMap, ILevelFactory levelFactory, SignalRConnection Con)
        {
            if (key == Input.KeyUp)
            {
                MovePlayer(Direction.Up, player, gameMap, Con);
            }
            else if (key == Input.KeyDown)
            {
                MovePlayer(Direction.Down, player, gameMap, Con);
            }
            else if (key == Input.KeyRight)
            {
                MovePlayer(Direction.Right, player, gameMap, Con);
            }
            else if (key == Input.KeyLeft)
            {
                MovePlayer(Direction.Left, player, gameMap, Con);
            }
            else if (key == Input.KeyBomb)
            {
                player.PlaceExplosive(gameMap, levelFactory);
                //Con.Connection.InvokeAsync("MapSeed"); place bomb change later
            }
        }

        private static void MovePlayer(Vector2 direction, Player player, GameMap gameMap, SignalRConnection Con)
        {
            if (IsDummyColliding(direction, player, gameMap))
                return;

            player.Move(direction);
            Con.Connection.InvokeAsync("Move", direction.X, direction.Y, player.SpeedModifier, player.GetMoveSpeed());
        }

        private static bool IsDummyColliding(Vector2 direction, Player player, GameMap gameMap)
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
