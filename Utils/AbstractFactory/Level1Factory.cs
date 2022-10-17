using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Utils.GameObjects;
using Utils.GameObjects.Destructables.Walls;
using Utils.GameObjects.Explosives;
using Utils.GameObjects.Interactables;
using Utils.Math;

namespace Utils.AbstractFactory
{
    public class Level1Factory : ILevelFactory
    {
        public Level1Factory() 
        { 
            
        }

        public GameObject CreateExplosive(Vector2 position, Vector2 size, Vector4 collider, Bitmap image, Bitmap fireImage)
        {
            return new ExplosiveHV(position, size, collider, image, fireImage);
        }

        public GameObject CreateExplosive(int x, int y, int width, int height, int cx, int cy, int cWidth, int cHeight, Bitmap image, Bitmap fireImage)
        {
            return new ExplosiveHV(x, y, width, height, cx, cy, cWidth, cHeight, image, fireImage);
        }

        public GameObject CreatePowerup(Vector2 position, Vector2 size, Vector4 collider, Bitmap image)
        {
            return new SpeedPowerup(position, size, collider, image);
        }

        public GameObject CreatePowerup(int x, int y, int width, int height, int cx, int cy, int cWidth, int cHeight, Bitmap image)
        {
            return new SpeedPowerup(x, y, width, height, cx, cy, cWidth, cHeight, image);
        }

        public GameObject CreateWall(Vector2 position, Vector2 size, Vector4 collider, Bitmap image)
        {
            return new PaperWall(position, size, collider, image);
        }

        public GameObject CreateWall(int x, int y, int width, int height, int cx, int cy, int cWidth, int cHeight, Bitmap image)
        {
            return new PaperWall(x, y, width, height, cx, cy, cWidth, cHeight, image);
        }
    }
}
