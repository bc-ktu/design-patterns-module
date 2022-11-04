using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Utils.Math;
using Utils.Helpers;
using Utils.GameLogic;

namespace Utils.GameObjects.Explosives
{
    public class ExplosiveDi : Explosive
    {
        public ExplosiveDi()
        {
            Initialize();
        }

        public ExplosiveDi(ExplosiveDi edi) : base(edi) { }

        public ExplosiveDi(Vector2 position, Vector2 size, Vector4 collider, Bitmap image, Fire fire)
            : base(position, size, collider, image, fire)
        {
            Initialize();
        }

        public ExplosiveDi(int x, int y, int width, int height, int cx, int cy, int cWidth, int cHeight, Bitmap image, Fire fire)
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
