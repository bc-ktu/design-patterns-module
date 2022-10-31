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
using Utils.Math;

namespace Utils.GameLogic
{
    public static class GameLogic
    {
        public static void ApplyEffects(Character player, GameMap gameMap, List<Vector2> indexes)
        {
            for (int i = 0; i < indexes.Count; i++)
            {
                GameObject go = gameMap[indexes[i]].GameObject;
                if (go is Fire) {
                    Fire fire = go as Fire;
                    player.TakeDamage(fire.Damage);
                }
                if (go is Powerup)
                {
                    Powerup powerup = go as Powerup;
                    powerup.Affect(player, gameMap);
                }
            }

            Vector2 characterIndex = player.WorldPosition / gameMap.TileSize;
            gameMap[characterIndex].AffectPlayer(player);
        }

        public static void UpdateExplosives(Character player, GameMap gameMap)
        {
            for (int i = 0; i < gameMap.ExplosivesLookupTable.Count; i++)
            {
                Explosive explosive = gameMap.ExplosivesLookupTable.GameObjects[i] as Explosive;
                explosive.UpdateState(gameMap, player);
            }
        }

        public static void UpdateFires(GameMap gameMap)
        {
            for (int i = 0; i < gameMap.FireLookupTable.Count; i++)
            {
                Fire fire = gameMap.FireLookupTable.GameObjects[i] as Fire;
                fire.UpdateState(gameMap);
            }
        }

        public static void UpdateGUI(Character player, GUI gui)
        {
            gui.SetHealthValue(player.Health);
            gui.SetSpeedValue(player.MovementSpeed);
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
                    if (gameMap[x, y].GameObject is EmptyGameObject)
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
                var prm = gameMap.CreateScaledGameObjectParameters(position.X, position.Y, image);
                Powerup powerup = levelFactory.CreatePowerup(prm.Item1, prm.Item2, prm.Item3, prm.Item4);
                gameMap[position].GameObject = powerup;
                gameMap.PowerupLookupTable.Set(position, powerup);
                emptyTiles.Remove(position);
            }
        }

    }
}
