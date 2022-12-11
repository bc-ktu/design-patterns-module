using Utils.Flyweight;
using Utils.Math;

namespace Utils.GameObjects.Interactables
{
    internal class SpeedPowerup : Powerup
    {
        public SpeedPowerup()
        {
            Initialize();
        }

        public SpeedPowerup(SpeedPowerup sp) : base(sp) { }

        public SpeedPowerup(Vector2 position, Vector2 size, Vector4 collider, ImageFlyweight image) 
            : base(position, size, collider, image)
        {
            Initialize();
        }

        public SpeedPowerup(int x, int y, int width, int height, int cx, int cy, int cWidth, int cHeight, ImageFlyweight image)
            : base(x, y, width, height, cx, cy, cWidth, cHeight, image)
        {
            Initialize();
        }

        private void Initialize()
        {
            SpeedModifier = 1;
            CapacityModifier = 0;
            DamageModifier = 0;
            RangeModifier = 0;
        }

        public override GameObject Clone()
        {
            return new SpeedPowerup(this);
        }

    }
}
