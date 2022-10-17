using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Utils.Math;

namespace Utils.GameObjects.Destructables
{
    public class DestructableGameObject : GameObject // make abstract
    {
        public int Durability { get; protected set; }

        public DestructableGameObject(Vector2 position, Vector2 size, Vector4 collider, Bitmap image)
            : base(position, size, collider, image)
        {

        }

        public DestructableGameObject(int x, int y, int width, int height, int cx, int cy, int cWidth, int cHeight, Bitmap image)
            : base(x, y, width, height, cx, cy, cWidth, cHeight, image)
        {

        }

        public void DecreaseDurability()
        {
            Durability--;
            if (Durability >= 0)
                Break();
        }

        private void Break()
        {
            throw new NotImplementedException("Wall cannot break");
        }

    }
}
