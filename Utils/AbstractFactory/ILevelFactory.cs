using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Builder;
using Utils.GameObjects;
using Utils.GameObjects.Destructables.Walls;
using Utils.GameObjects.Explosives;
using Utils.GameObjects.Interactables;
using Utils.Helpers;
using Utils.Math;

namespace Utils.AbstractFactory
{
    public interface ILevelFactory
    {
        public Explosive CreateExplosive(Vector2 position, Vector2 size, Vector4 collider, Bitmap image, Bitmap fireImage);
        public Explosive CreateExplosive(int x, int y, int width, int height, int cx, int cy, int cWidth, int cHeight, Bitmap image, Bitmap fireImage);

        public DestructableWall CreateWall(Vector2 position, Vector2 size, Vector4 collider, Bitmap image);
        public DestructableWall CreateWall(int x, int y, int width, int height, int cx, int cy, int cWidth, int cHeight, Bitmap image);

        public Powerup CreatePowerup(Vector2 position, Vector2 size, Vector4 collider, Bitmap image);
        public Powerup CreatePowerup(int x, int y, int width, int height, int cx, int cy, int cWidth, int cHeight, Bitmap image);

        public MapBuilder CreateBuilder(Vector2 mapSize, Vector2 viewSize, List<int> mapSeed, Bitmap mapTileImage, Bitmap crateImage, Bitmap outerWallImage, Bitmap specTileImage, ILevelFactory levelFactory);
        public Bitmap GetSpecialTileImage();
    }
}
