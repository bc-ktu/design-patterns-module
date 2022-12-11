using Utils.Math;
using Utils.Helpers;
using Utils.Flyweight;

namespace Utils.GameObjects.Explosives
{
    public class ExplosiveDi : Explosive
    {
        public ExplosiveDi()
        {
            Initialize();
        }

        public ExplosiveDi(ExplosiveDi edi) : base(edi) { }

        public ExplosiveDi(Vector2 position, Vector2 size, Vector4 collider, ImageFlyweight image, Fire fire)
            : base(position, size, collider, image, fire)
        {
            Initialize();
        }

        public ExplosiveDi(int x, int y, int width, int height, int cx, int cy, int cWidth, int cHeight, ImageFlyweight image, Fire fire)
            : base(x, y, width, height, cx, cy, cWidth, cHeight, image, fire)
        {
            Initialize();
        }

        private void Initialize()
        {
            ExplosionDirections = new Vector2[]{
                Direction.UpRight,
                Direction.DownRight,
                Direction.DownLeft,
                Direction.UpLeft
            };
            Range = 2;
        }

        public override GameObject Clone()
        {
            return new ExplosiveDi(this);
        }

    }
}
