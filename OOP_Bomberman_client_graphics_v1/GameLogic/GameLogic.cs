using client_graphics.AbstractFactory;
using client_graphics.GameObjects;
using client_graphics.GameObjects.Animates;
using client_graphics.GameObjects.Explosives;
using client_graphics.GameObjects.Interactables;
using Utils.GUIElements;
using client_graphics.Map;
using Utils.Math;
using client_graphics.SignalR;

namespace client_graphics.GameLogic
{
    public static class GameLogic
    {
        public static void ApplyEffects(Player player, GameMap gameMap, GameObject[] gameObjects, SignalRConnection connection)
        {
            for (int i = 0; i < gameObjects.Length; i++)
            {
                if (gameObjects[i] is Fire)
                {
                    Fire fire = (Fire)gameObjects[i];
                    player.TakeDamage(fire.Damage);
                }
                if (gameObjects[i] is Powerup)
                {
                    Powerup powerup = (Powerup)gameObjects[i];
                    powerup.Affect(player, gameMap, connection);
                }
                if (gameObjects[i] is Enemy)
                {
                    player.TakeDamage(1);
                }
            }

            Vector2 characterIndex = player.WorldPosition / gameMap.TileSize;
            gameMap[characterIndex].AffectPlayer(player);
        }

        public static void UpdateExplosives(Player player, GameMap gameMap)
        {
            for (int i = 0; i < gameMap.ExplosivesLookupTable.Count; i++)
            {
                Explosive explosive = (Explosive)gameMap.ExplosivesLookupTable.GameObjects[i];
                explosive.UpdateState(gameMap, player);
            }
        }

        public static void UpdateFires(GameMap gameMap, ILevelFactory levelFactory)
        {
            for (int i = 0; i < gameMap.FireLookupTable.Count; i++)
            {
                Fire fire = (Fire)gameMap.FireLookupTable.GameObjects[i];
                fire.UpdateState(gameMap, levelFactory);
            }
        }

        public static void UpdateGUI(Player player, GUI gui)
        {
            gui.SetHealthValue(player.Health);
            gui.SetSpeedValue(player.GetSpeed());
            gui.SetCapacityValue(player.ExplosivesCapacity);
            gui.SetRangeValue(player.Explosive.Range);
            gui.SetDamageValue(player.Explosive.Fire.Damage);
        }

        public static void GeneratePowerups(ILevelFactory levelFactory, GameMap gameMap, int amount, Bitmap image)
        {
            List<Vector2> emptyTiles = new List<Vector2>();
            for (int y = 1; y < gameMap.Size.Y - 1; y++)
            {
                for (int x = 1; x < gameMap.Size.X - 1; x++)
                {
                    if (gameMap[x, y].IsEmpty)
                    {
                        emptyTiles.Add(new Vector2(x, y));
                    }
                }
            }

            Random rnd = new Random();
            for (int i = 0; i < amount; i++)
            {
                int index = rnd.Next(0, emptyTiles.Count);
                Vector2 position = emptyTiles[index];
                //var prm = gameMap.CreateScaledGameObjectParameters(position.X, position.Y, image, 0.75);
                //Powerup powerup = levelFactory.CreatePowerup(prm.Item1, prm.Item2, prm.Item3, prm.Item4);
                Powerup powerup = levelFactory.CreatePowerup(gameMap, position);
                gameMap[position].GameObjects.Add(powerup);
                gameMap.PowerupLookupTable.Add(position, powerup);
                emptyTiles.Remove(position);
            }
        }

    }
}
