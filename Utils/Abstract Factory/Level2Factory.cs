using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Utils.GameObjects;
using Utils.Math;

namespace Utils.AbstractFactory
{
    internal class Level2Factory : ILevelFactory
    {
        public Level2Factory()
        {

        }

        public GameObject CreateExplosive(Vector2 position, Vector2 size, Vector4 collider, Bitmap image, Bitmap fireImage)
        {
            return new ExplosiveDi(position, size, collider, image, fireImage);
        }

        public GameObject CreateExplosive(int x, int y, int width, int height, int cx, int cy, int cWidth, int cHeight, Bitmap image, Bitmap fireImage)
        {
            return new ExplosiveDi(x, y, width, height, cx, cy, cWidth, cHeight, image, fireImage);
        }

        public GameObject CreatePowerup(Vector2 position, Vector2 size, Vector4 collider, Bitmap image)
        {
            return new CapacityPowerup(position, size, collider, image);
        }

        public GameObject CreatePowerup(int x, int y, int width, int height, int cx, int cy, int cWidth, int cHeight, Bitmap image)
        {
            return new CapacityPowerup(x, y, width, height, cx, cy, cWidth, cHeight, image);
        }

        public GameObject CreateWall(Vector2 position, Vector2 size, Vector4 collider, Bitmap image)
        {
            return new WoodenWall(position, size, collider, image);
        }

        public GameObject CreateWall(int x, int y, int width, int height, int cx, int cy, int cWidth, int cHeight, Bitmap image)
        {
            return new WoodenWall(x, y, width, height, cx, cy, cWidth, cHeight, image);
        }
    }
}
