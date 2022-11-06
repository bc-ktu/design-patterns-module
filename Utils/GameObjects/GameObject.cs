using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Decorator;
using Utils.Math;
using Utils.Prototype;

namespace Utils.GameObjects
{
    public abstract class GameObject : ICloneable<GameObject>, IDrawable
    {
        public Vector2 LocalPosition { get; protected set; }
        public Vector2 WorldPosition { get { return LocalPosition + Size / 2; } }
        public Vector2 Size { get; protected set; }
        public Vector4 Collider { get; protected set; }

        public Bitmap Image { get; protected set; }

        public GameObject() 
        {
            LocalPosition = new Vector2(0, 0);
            Size = new Vector2(0, 0);
            Collider = new Vector4(0, 0, 0, 0);
            Image = new Bitmap(0, 0);
        }

        public GameObject(GameObject go)
        {
            LocalPosition = go.LocalPosition.Clone();
            Size = go.Size.Clone();
            Collider = go.Collider.Clone();
            Image = (Bitmap)go.Image.Clone();
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

        public void Teleport(Vector2 position)
        {
            Vector2 vPtoC = new Vector2(Collider.X, Collider.Y) - LocalPosition;
            Vector2 vTLtoBR = new Vector2(Collider.Z - Collider.X, Collider.W - Collider.Y);
            LocalPosition = position;
            int tlx = LocalPosition.X + vPtoC.X;
            int tly = LocalPosition.Y + vPtoC.Y;
            int brx = tlx + vTLtoBR.X;
            int bry = tly + vTLtoBR.Y;
            Collider = new Vector4(tlx, tly, brx, bry);
        }
        
        public override string ToString()
        {
            return "position: " + LocalPosition.ToString() + "\n" +
                   "size: " + Size.ToString() + "\n" +
                   "collider: " + Collider.ToString();
        }

        public abstract GameObject Clone();

        public void Draw(PaintEventArgs e)
        {
            e.Graphics.DrawImage(Image, ToRectangle());
        }

        public Rectangle ToRectangle()
        {
            return new Rectangle(LocalPosition.X, LocalPosition.Y, Size.X, Size.Y);
        }

    }
}
