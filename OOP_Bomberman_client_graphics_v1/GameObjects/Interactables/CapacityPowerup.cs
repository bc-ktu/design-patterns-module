using Utils.Math;

namespace client_graphics.GameObjects.Interactables
{
    public class CapacityPowerup : Powerup
    {
        public CapacityPowerup()
        { 
            Initialize(); 
        }

        public CapacityPowerup(CapacityPowerup cp) : base(cp) { }

        public CapacityPowerup(Vector2 position, Vector2 size, Vector4 collider, Bitmap image)
            : base(position, size, collider, image)
        {
            Initialize();
        }

        public CapacityPowerup(int x, int y, int width, int height, int cx, int cy, int cWidth, int cHeight, Bitmap image)
            : base(x, y, width, height, cx, cy, cWidth, cHeight, image)
        {
            Initialize();
        }

        private void Initialize()
        {
            SpeedModifier = 0;
            CapacityModifier = 1;
            DamageModifier = 0;
            RangeModifier = 0;
        }

        public override GameObject Clone()
        {
            return new CapacityPowerup(this);
        }

    }
}
