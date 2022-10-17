using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Utils.GameObjects;
using Utils.Math;

namespace Utils.AbstractFactory
{
    public interface ILevelFactory
    {
        public GameObject CreateExplosive(Vector2 position, Vector2 size, Vector4 collider, Bitmap image, Bitmap fireImage);
        public GameObject CreateExplosive(int x, int y, int width, int height, int cx, int cy, int cWidth, int cHeight, Bitmap image, Bitmap fireImage);

        public GameObject CreateWall(Vector2 position, Vector2 size, Vector4 collider, Bitmap image);
        public GameObject CreateWall(int x, int y, int width, int height, int cx, int cy, int cWidth, int cHeight, Bitmap image);

        public GameObject CreatePowerup(Vector2 position, Vector2 size, Vector4 collider, Bitmap image);
        public GameObject CreatePowerup(int x, int y, int width, int height, int cx, int cy, int cWidth, int cHeight, Bitmap image);
    }
}
