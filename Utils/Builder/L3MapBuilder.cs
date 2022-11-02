using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.AbstractFactory;
using Utils.Factory;
using Utils.GameObjects;
using Utils.Math;

namespace Utils.Builder
{
    public class L3MapBuilder : MapBuilder
    {
        public L3MapBuilder(Vector2 mapSize, Vector2 viewSize, List<int> mapSeed, Bitmap mapTileImage, Bitmap crateImage, Bitmap outerWallImage, Bitmap specTileImage, ILevelFactory levelFactory) : base(mapSize, viewSize, mapSeed, mapTileImage, crateImage, outerWallImage, levelFactory)
        {
            this.specTileImage = specTileImage;
        }

        public override void AddSpecialTiles()
        {
            Random random = new Random();
            int num = 5;
            for (int i = 0; i < num; i++)
            {
                int rx = random.Next(1, mapSize.X - 1);
                int ry = random.Next(1, mapSize.Y - 1);
                MapTile tile = gameMap._tiles[rx, ry];
                if (tile is not IceTile) gameMap._tiles[rx, ry] = new IceTile(rx * gameMap.TileSize.X, ry * gameMap.TileSize.Y, gameMap.TileSize.X, gameMap.TileSize.Y, specTileImage);
                else num++;
            }
            //return;
        }
    }
}
