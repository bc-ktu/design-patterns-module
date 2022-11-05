﻿using Utils.Math;

namespace Utils.GameObjects.Walls
{
    internal class WoodenWall : DestructableWall
    {
        public WoodenWall()
        {
            Durability = 5;
        }

        public WoodenWall(WoodenWall ww) : base(ww) { }

        public WoodenWall(Vector2 position, Vector2 size, Vector4 collider, Bitmap image) 
            : base(position, size, collider, image)
        {
            Durability = 5;
        }

        public WoodenWall(int x, int y, int width, int height, int cx, int cy, int cWidth, int cHeight, Bitmap image)
            : base(x, y, width, height, cx, cy, cWidth, cHeight, image)
        {
            Durability = 5;
        }

        public override GameObject Clone()
        {
            return new WoodenWall(this);
        }

    }
}
