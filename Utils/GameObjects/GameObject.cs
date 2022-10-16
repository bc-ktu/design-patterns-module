using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Math;

namespace Utils.GameObjects
{
    public abstract class GameObject : INullable
    {
        public Vector2 LocalPosition { get; protected set; }
        public Vector2 WorldPosition { get { return LocalPosition + Size / 2; } }
        public Vector2 Size { get; protected set; }
        public Vector4 Collider { get; protected set; }

        public Bitmap Image { get; protected set; }

        // How to implement? Is it neccesary
        public bool IsNull => throw new NotImplementedException();

        public GameObject()
        {

        }

        /// <param name="localPosition">Top left corner coordinates of the sprite</param>
        /// <param name="collider">Top left corner (X, Y), bottom right corner (Z, W)</param>
        public GameObject(Vector2 localPosition, Vector2 size, Vector4 collider, Bitmap image)
        {
            LocalPosition = localPosition;
            Size = size;
            Collider = collider;
            Image = image;
        }

        /// <param name="x">Top left x coordinate corner of the sprite</param>
        /// <param name="y">Top left y coordinate corner of the sprite</param>
        /// <param name="cx">Top left x coordinate corner of the collider</param>
        /// <param name="cy">Top left y coordinate corner of the collider</param>
        /// <param name="cWidth">Width of the collider</param>
        /// <param name="cHeight">Height of the collider</param>
        public GameObject(int x, int y, int width, int height, int cx, int cy, int cWidth, int cHeight, Bitmap image)
        {
            LocalPosition = new Vector2(x, y);
            Size = new Vector2(width, height);
            Collider = new Vector4(cx, cy, cx + cWidth, cy + cHeight);
            Image = image;
        }

        public Rectangle ToRectangle()
        {
            return new Rectangle(LocalPosition.X, LocalPosition.Y, Size.X, Size.Y);
        }

        public override string ToString()
        {
            return "position: " + LocalPosition.ToString() + "\n" +
                   "size: " + Size.ToString() + "\n" +
                   "collider: " + Collider.ToString();
        }

    }
}
