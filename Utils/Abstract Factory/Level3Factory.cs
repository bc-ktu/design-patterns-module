using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Utils.GameObjects;
using Utils.Math;

namespace Utils.AbstractFactory
{
    internal class Level3Factory : ILevelFactory
    {
        public Level3Factory()
        {

        }

        public GameObject CreateExplosive(Vector2 position, Vector2 size, Vector4 collider, Bitmap image)
        {
            return new ExplosiveHVDi(position, size, collider, image);
        }

        public GameObject CreateExplosive(int x, int y, int width, int height, int cx, int cy, int cWidth, int cHeight, Bitmap image)
        {
            return new ExplosiveHVDi(x, y, width, height, cx, cy, cWidth, cHeight, image);
        }

        public GameObject CreatePowerup(Vector2 position, Vector2 size, Vector4 collider, Bitmap image)
        {
            return new DamagePowerup(position, size, collider, image);
        }

        public GameObject CreatePowerup(int x, int y, int width, int height, int cx, int cy, int cWidth, int cHeight, Bitmap image)
        {
            return new DamagePowerup(x, y, width, height, cx, cy, cWidth, cHeight, image);
        }

        public GameObject CreateWall(Vector2 position, Vector2 size, Vector4 collider, Bitmap image)
        {
            return new StoneWall(position, size, collider, image);
        }

        public GameObject CreateWall(int x, int y, int width, int height, int cx, int cy, int cWidth, int cHeight, Bitmap image)
        {
            return new StoneWall(x, y, width, height, cx, cy, cWidth, cHeight, image);
        }
    }
}
