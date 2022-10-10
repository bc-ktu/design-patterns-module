using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Utils.Math;
using Utils.Helpers;

namespace Utils.GameObjects
{
    public class ExplosiveHVDi : Explosive
    {
        public ExplosiveHVDi(Vector2 position, Vector2 size, Vector4 collider, Bitmap image) : base(position, size, collider, image)
        {
            Vector2[] explosionDirections = new Vector2[]{
                Direction.Up,
                Direction.UpRight,
                Direction.Right,
                Direction.DownRight,
                Direction.Down,
                Direction.DownLeft,
                Direction.Left,
                Direction.UpLeft
            };
            SetExplosionDirections(explosionDirections);
            SetRange(3);
        }

        public ExplosiveHVDi(int x, int y, int width, int height, int cx, int cy, int cWidth, int cHeight, Bitmap image)
            : base(x, y, width, height, cx, cy, cWidth, cHeight, image)
        {
            Vector2[] explosionDirections = new Vector2[]{
                Direction.Up,
                Direction.UpRight,
                Direction.Right,
                Direction.DownRight,
                Direction.Down,
                Direction.DownLeft,
                Direction.Left,
                Direction.UpLeft
            };
            SetExplosionDirections(explosionDirections);
            SetRange(3);
        }

    }
}
