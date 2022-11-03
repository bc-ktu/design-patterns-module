using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.AbstractFactory;
using Utils.GameObjects;
using Utils.GameObjects.Animates;
using Utils.GameObjects.Explosives;
using Utils.GameObjects.Interactables;
using Utils.GUIElements;
using Utils.Map;
using Utils.Math;

namespace Utils.GameLogic
{
    public static class GameLogic
    {
        public static void ApplyEffects(Player player, GameMap gameMap, GameObject[] gameObjects)
        {
            for (int i = 0; i < gameObjects.Length; i++)
            {
                if (gameObjects[i] is Fire)
                {
                    Fire fire = gameObjects[i] as Fire;
                    player.TakeDamage(fire.Damage);
                }
                if (gameObjects[i] is Powerup)
                {
                    Powerup powerup = gameObjects[i] as Powerup;
                    powerup.Affect(player, gameMap);
                }
            }

            Vector2 characterIndex = player.WorldPosition / gameMap.TileSize;
            gameMap[characterIndex].AffectPlayer(player);
        }

        public static void UpdateExplosives(Player player, GameMap gameMap)
        {
            for (int i = 0; i < gameMap.ExplosivesLookupTable.Count; i++)
            {
                Explosive explosive = gameMap.ExplosivesLookupTable.GameObjects[i] as Explosive;
                explosive.UpdateState(gameMap, player);
            }
        }

        public static void UpdateFires(GameMap gameMap, ILevelFactory levelFactory)
        {
            for (int i = 0; i < gameMap.FireLookupTable.Count; i++)
            {
                Fire fire = gameMap.FireLookupTable.GameObjects[i] as Fire;
                fire.UpdateState(gameMap, levelFactory);
            }
        }

        public static void UpdateGUI(Player player, GUI gui)
        {
            gui.SetHealthValue(player.Health);
            gui.SetSpeedValue(player.GetSpeed());
            gui.SetCapacityValue(player.ExplosivesCapacity);
            gui.SetRangeValue(player.ExplosivesRange);
            gui.SetDamageValue(player.ExplosiveDamage);
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
