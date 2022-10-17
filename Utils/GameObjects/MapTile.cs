using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

using Utils.Math;

namespace Utils.GameObjects
{
    public class MapTile
    {
        public Vector2 LocalPosition { get; private set; }
        public Vector2 WorldPosition { get { return LocalPosition + Size / 2; } }
        public Vector2 Size { get; private set; }
        public Bitmap Image { get; private set; }

        public GameObject GameObject { get; set; }

        public MapTile(Vector2 localPosition, Vector2 size, Bitmap image)
        {
            LocalPosition = localPosition;
            Size = size;
            Image = image;
            GameObject = new EmptyGameObject();
        }

        public MapTile(int x, int y, int width, int height, Bitmap image)
        {
            LocalPosition = new Vector2(x, y);
            Size = new Vector2(width, height);
            Image = image;
            GameObject = new EmptyGameObject();
        }

        public Rectangle ToRectangle()
        {
            return new Rectangle(LocalPosition.X, LocalPosition.Y, Size.X, Size.Y);
        }
    }
}
