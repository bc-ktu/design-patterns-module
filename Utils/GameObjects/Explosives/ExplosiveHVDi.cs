using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Utils.Math;
using Utils.Helpers;

namespace Utils.GameObjects.Explosives
{
    public class ExplosiveHVDi : Explosive
    {
        public ExplosiveHVDi() { }

        public ExplosiveHVDi(ExplosiveHVDi ehvdi) : base(ehvdi) { }

        public ExplosiveHVDi(Vector2 position, Vector2 size, Vector4 collider, Bitmap image, Fire fire)
            : base(position, size, collider, image, fire)
        {
            Initialize();
        }

        public ExplosiveHVDi(int x, int y, int width, int height, int cx, int cy, int cWidth, int cHeight, Bitmap image, Fire fire)
            : base(x, y, width, height, cx, cy, cWidth, cHeight, image, fire)
        {
            Initialize();
        }

        private void Initialize()
        {
            ExplosionDirections = new Vector2[]{
                Direction.Up,
                Direction.UpRight,
                Direction.Right,
                Direction.DownRight,
                Direction.Down,
                Direction.DownLeft,
                Direction.Left,
                Direction.UpLeft
            };
            Range = 3;
        }

        public override GameObject Clone()
        {
            return new ExplosiveHVDi(this);
        }

    }
}
