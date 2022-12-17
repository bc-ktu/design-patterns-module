using Utils.Flyweight;
using Utils.Math;

namespace Utils.GameObjects.Interactables
{
    internal class DamagePowerup : Powerup
    {
        public DamagePowerup() 
        {
            Initialize();
        }

        public DamagePowerup(DamagePowerup dp) : base(dp) { }

        public DamagePowerup(Vector2 position, Vector2 size, Vector4 collider, ImageFlyweight image) 
            : base(position, size, collider, image)
        {
            Initialize();
        }

        public DamagePowerup(int x, int y, int width, int height, int cx, int cy, int cWidth, int cHeight, ImageFlyweight image)
            : base(x, y, width, height, cx, cy, cWidth, cHeight, image)
        {
            Initialize();
        }

        private void Initialize()
        {
            SpeedModifier = 0;
            CapacityModifier = 0;
            DamageModifier = 1;
            RangeModifier = 0;
        }

        public override GameObject Clone()
        {
            return new DamagePowerup(this);
        }

    }
}
