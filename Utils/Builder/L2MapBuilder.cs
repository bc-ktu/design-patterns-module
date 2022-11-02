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
    public class L2MapBuilder : MapBuilder
    {
        public L2MapBuilder(Vector2 mapSize, Vector2 viewSize, List<int> mapSeed, Bitmap mapTileImage, Bitmap crateImage, Bitmap outerWallImage, Bitmap specTileImage, ILevelFactory levelFactory) : base(mapSize, viewSize, mapSeed, mapTileImage, crateImage, outerWallImage, levelFactory)
        {
            this.specTileImage = specTileImage;
        }

        public override void AddSpecialTiles()
        {
            int index = mapSeed.Count - 10;
            int rx = mapSeed[index];
            int ry = mapSeed[index + 1];
            PortalTile pt1 = new PortalTile(rx * gameMap.TileSize.X, ry * gameMap.TileSize.Y, gameMap.TileSize.X, gameMap.TileSize.Y, specTileImage);

            int rxx = mapSeed[index + 3];
            int ryy = mapSeed[index + 4];
            MapTile tile = gameMap._tiles[rxx, ryy];
            if (tile is PortalTile)
            {
                rxx++;
                if (rxx >= mapSize.X) rxx -= 2;
            }
            PortalTile pt2 = new PortalTile(rxx * gameMap.TileSize.X, ryy * gameMap.TileSize.Y, gameMap.TileSize.X, gameMap.TileSize.Y, specTileImage);
            pt1.ExitTile = pt2;
            gameMap._tiles[rx, ry] = pt1;
            gameMap._tiles[rxx, ryy] = pt2;
        }
    }
}
