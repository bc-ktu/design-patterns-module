using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Utils.Math;

namespace Utils.GameObjects
{
    public abstract class DestructableWall : GameObject
    {
        private int _durability;

        public int Durability { get { return _durability; } }

        public DestructableWall()
        {

        }

        public DestructableWall(Vector2 position, Vector2 size, Vector4 collider, Bitmap image) : base(position, size, collider, image)
        {

        }

        public DestructableWall(int x, int y, int width, int height, int cx, int cy, int cWidth, int cHeight, Bitmap image) 
            : base(x, y, width, height, cx, cy, cWidth, cHeight, image)
        {

        }

        protected void SetDurability(int durability)
        {
            _durability = durability;
        }

        public void DecreaseDurability()
        {
            _durability--;
            if (_durability >= 0)
                Break();
        }

        private void Break()
        {
            throw new NotImplementedException("Wall cannot break");
        }
    }
}
