using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Utils.Math;

namespace Utils.GameObjects.Destructables.Walls
{
    internal class PaperWall : DestructableWall
    {
        public PaperWall(Vector2 position, Vector2 size, Vector4 collider, Bitmap image) 
            : base(position, size, collider, image)
        {
            Durability = 3;
        }

        public PaperWall(int x, int y, int width, int height, int cx, int cy, int cWidth, int cHeight, Bitmap image)
            : base(x, y, width, height, cx, cy, cWidth, cHeight, image)
        {
            Durability = 3;
        }

    }
}
