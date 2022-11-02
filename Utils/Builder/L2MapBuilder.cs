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
            MapTile tile;
            Random random = new Random();
            int rx = random.Next(1, mapSize.X - 1);
            int ry = random.Next(1, mapSize.Y - 1);
            PortalTile pt1 = new PortalTile(rx * gameMap.TileSize.X, ry * gameMap.TileSize.Y, gameMap.TileSize.X, gameMap.TileSize.Y, specTileImage);

            int rxx = random.Next(1, mapSize.X - 1);
            int ryy = random.Next(1, mapSize.Y - 1);
            tile = gameMap._tiles[rx, ry];
            if (tile is PortalTile)
            {
                rxx++;
                if (rxx >= mapSize.X) rxx -= 2;
            }
            PortalTile pt2 = new PortalTile(rxx * gameMap.TileSize.X, ryy * gameMap.TileSize.Y, gameMap.TileSize.X, gameMap.TileSize.Y, specTileImage);
            pt1.ExitTile = pt2;
            gameMap._tiles[rx, ry] = pt1;
            gameMap._tiles[rxx, ryy] = pt2;
            //return;
        }
    }
}
