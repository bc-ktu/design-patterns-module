using Utils.Math;
using Utils.Helpers;

namespace Utils.GameObjects.Explosives
{
    internal class ExplosiveHV : Explosive
    {
        public ExplosiveHV()
        {
            Initialize();
        }

        public ExplosiveHV(ExplosiveHV ehv) : base(ehv) { }

        public ExplosiveHV(Vector2 position, Vector2 size, Vector4 collider, Bitmap image, Fire fire) 
            : base(position, size, collider, image, fire)
        {
            Initialize();
        }

        public ExplosiveHV(int x, int y, int width, int height, int cx, int cy, int cWidth, int cHeight, Bitmap image, Fire fire)
            : base(x, y, width, height, cx, cy, cWidth, cHeight, image, fire)
        {
            Initialize();
        }

        private void Initialize()
        {
            ExplosionDirections = new Vector2[]{
                Direction.Up,
                Direction.Right,
                Direction.Down,
                Direction.Left
            };
            Range = 1;
        }

        public override GameObject Clone()
        {
            return new ExplosiveHV(this);
        }

    }
}
