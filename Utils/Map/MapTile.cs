using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

using Utils.GameObjects;
using Utils.GameObjects.Animates;
using Utils.Math;

namespace Utils.Map
{
    public abstract class MapTile
    {
        public Vector2 LocalPosition { get; private set; }
        public Vector2 WorldPosition { get { return LocalPosition + Size / 2; } }
        public Vector2 Size { get; private set; }
        public Bitmap Image { get; private set; }
        public List<GameObject> GameObjects { get; private set; }
        public bool IsEmpty { get { return GameObjects.Count == 0; } }

        public MapTile(Vector2 localPosition, Vector2 size, Bitmap image)
        {
            LocalPosition = localPosition;
            Size = size;
            Image = image;
            GameObjects = new List<GameObject>();
        }

        public MapTile(int x, int y, int width, int height, Bitmap image)
        {
            LocalPosition = new Vector2(x, y);
            Size = new Vector2(width, height);
            Image = image;
            GameObjects = new List<GameObject>();
        }

        public Rectangle ToRectangle()
        {
            return new Rectangle(LocalPosition.X, LocalPosition.Y, Size.X, Size.Y);
        }

        public abstract void AffectPlayer(Player player);
    }
}
