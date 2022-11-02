using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Utils.AbstractFactory;
using Utils.Factory;
using Utils.GameObjects;
using Utils.Math;

namespace Utils.Builder
{
    public class L1MapBuilder : MapBuilder
    {
        public L1MapBuilder(Vector2 mapSize, Vector2 viewSize, List<int> mapSeed, Bitmap mapTileImage, Bitmap crateImage, Bitmap outerWallImage, Bitmap specTileImage, ILevelFactory levelFactory) : base(mapSize, viewSize, mapSeed, mapTileImage, crateImage, outerWallImage, levelFactory)
        {
            this.specTileImage = specTileImage;
        }

        public override void AddSpecialTiles()
        {
            int index = mapSeed.Count - 10;
            for (int i = index; i < index + 5; i++)
            {
                int rx = mapSeed[i];
                int ry = mapSeed[i + 5];
                gameMap._tiles[rx, ry] = new MudTile(rx * gameMap.TileSize.X, ry * gameMap.TileSize.Y, gameMap.TileSize.X, gameMap.TileSize.Y, specTileImage);
            }
        }
    }
}
