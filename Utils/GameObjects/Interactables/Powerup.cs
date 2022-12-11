using Utils.Flyweight;
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
        public int RangeModifier { get; protected set; }

        public Powerup() { }

        public Powerup(Powerup p) : base(p) 
        {
            SpeedModifier = p.SpeedModifier;
            CapacityModifier = p.CapacityModifier;
            DamageModifier = p.DamageModifier;
            RangeModifier = p.RangeModifier;
        }

        public Powerup(Vector2 position, Vector2 size, Vector4 collider, ImageFlyweight image) 
            : base(position, size, collider, image)
        { }

        public Powerup(int x, int y, int width, int height, int cx, int cy, int cWidth, int cHeight, ImageFlyweight image)
            : base(x, y, width, height, cx, cy, cWidth, cHeight, image)
        { }
        
        public void Affect(Player character, GameMap gameMap)
        {
            character.ChangeSpeed(SpeedModifier);
            character.ChangeExplosivesCapacity(CapacityModifier);
            character.ChangeExplosiveDamage(DamageModifier);
            character.ChangeExplosiveRange(RangeModifier);

            Vector2 index = WorldPosition / gameMap.TileSize;
            gameMap[index].GameObjects.Remove(this);
            gameMap.PowerupLookupTable.Remove(index, this);
        }

    }
}
