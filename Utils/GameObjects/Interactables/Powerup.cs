using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.GameObjects.Animates;
using Utils.Map;
using Utils.Math;

namespace Utils.GameObjects.Interactables
{
    public abstract class Powerup : TriggerGameObject
    {
        public int SpeedModifier { get; protected set; }
        public int CapacityModifier { get; protected set; }
        public int DamageModifier { get; protected set; }

        public Powerup(Vector2 position, Vector2 size, Vector4 collider, Bitmap image) 
            : base(position, size, collider, image)
        {

        }

        public Powerup(int x, int y, int width, int height, int cx, int cy, int cWidth, int cHeight, Bitmap image)
            : base(x, y, width, height, cx, cy, cWidth, cHeight, image)
        {

        }
        
        public void Affect(Player character, GameMap gameMap)
        {
            character.ChangeSpeed(SpeedModifier);
            character.ChangeExplosivesCapacity(CapacityModifier);
            character.ChangeExplosivesDamage(DamageModifier);

            Vector2 index = WorldPosition / gameMap.TileSize;
            gameMap[index].GameObjects.Remove(this);
            gameMap.PowerupLookupTable.Remove(index, this);
        }

    }
}
