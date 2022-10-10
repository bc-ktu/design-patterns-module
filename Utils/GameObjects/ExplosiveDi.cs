using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Utils.Math;
using Utils.Helpers;
using Utils.GameLogic;

namespace Utils.GameObjects
{
    public class ExplosiveDi : Explosive
    {
        public ExplosiveDi(Vector2 position, Vector2 size, Vector4 collider, Bitmap image) : base(position, size, collider, image)
        {
            Vector2[] explosionDirections = new Vector2[]{
                Direction.UpRight,
                Direction.DownRight,
                Direction.DownLeft,
                Direction.UpLeft
            };
            SetExplosionDirections(explosionDirections);
            SetRange(2);
        }

        public ExplosiveDi(int x, int y, int width, int height, int cx, int cy, int cWidth, int cHeight, Bitmap image)
            : base(x, y, width, height, cx, cy, cWidth, cHeight, image)
        {
            Vector2[] explosionDirections = new Vector2[]{
                Direction.UpRight,
                Direction.DownRight,
                Direction.DownLeft,
                Direction.UpLeft
            };
            SetExplosionDirections(explosionDirections);
            SetRange(2);
        }

    }
}
