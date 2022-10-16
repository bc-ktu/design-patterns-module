using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Utils.GameObjects;
using Utils.GameObjects.Animates;
using Utils.GameObjects.Explosives;
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
                GameObject go = gameMap.Tiles[indexes[i].X, indexes[i].Y].GameObject;
                if (go is Fire) {
                    Fire fire = go as Fire;
                    player.TakeDamage(fire.Damage);
                }
            }
        }

        public static void UpdateLookupTables(Character player, GameMap gameMap)
        {
            for (int i = 0; i < gameMap.ExplosivesLookupTable.Count; i++)
            {
                Explosive explosive = gameMap.ExplosivesLookupTable.GameObjects[i] as Explosive;
                explosive.UpdateState(gameMap, player);
            }
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

    }
}
