using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.AbstractFactory;
using Utils.GameObjects;
using Utils.GameObjects.Crates;
using Utils.GameObjects.Walls;
using Utils.Iterator;
using Utils.Map;
using Utils.Math;

namespace Utils.Builder
{
    public abstract class MapBuilder
    {
        protected GameMap gameMap;
        protected Vector2 mapSize { get; set; }
        protected List<int> mapSeed { get; set; }
        protected Bitmap mapTileImage { get; set; }
        protected Bitmap crateImage { get; set; }
        protected Bitmap outerWallImage { get; set; }
        protected Bitmap specTileImage { get; set; }
        protected ILevelFactory levelFactory { get; set; }

        public MapBuilder(Vector2 mapSize, Vector2 viewSize, List<int> mapSeed, Bitmap mapTileImage, Bitmap crateImage, Bitmap outerWallImage, ILevelFactory levelFactory)
        {
            this.mapSize = mapSize;
            this.mapSeed = mapSeed;
            this.gameMap = new GameMap(mapSize, viewSize);
            this.mapTileImage = mapTileImage;
            this.crateImage = crateImage;
            this.outerWallImage = outerWallImage;
            this.levelFactory = levelFactory;
        }

        public void AddCrates()
        {
            int index = 0;
            MapIterator iterator = new InnerMapIterator(gameMap);
            for (Tuple<int, int> t = iterator.First(); !iterator.IsDone(); t = iterator.Next())
            {
                int x = t.Item1;
                int y = t.Item2;
                gameMap.SetTile(x, y, mapTileImage);

                if (x == 1 && y == 1 || x == 1 && y == 2 || x == 2 && y == 1)
                    continue;

                int tile = mapSeed[index];
                if (tile == 0)
                {
                    GameObject go = levelFactory.CreateWall(gameMap, new Vector2(x, y));
                    gameMap[x, y].GameObjects.Add(go);
                }
                else if (tile == 1 || tile == 2 || tile == 3)
                {
                    var prm = gameMap.CreateScaledGameObjectParameters(x, y, crateImage);
                    GameObject go = new Crate(prm.Item1, prm.Item2, prm.Item3, prm.Item4);
                    gameMap[x, y].GameObjects.Add(go);
                }
                else
                {
                    GameObject go = levelFactory.CreatePowerup(gameMap, new Vector2(x, y));
                    gameMap[x, y].GameObjects.Add(go);
                    gameMap.PowerupLookupTable.Add(new Vector2(x, y), go);
                }

                index++;
            }
        }

        public void AddOuterRing()
        {
            MapIterator iterator = new OuterRingIterator(gameMap);
            for (Tuple<int, int> tile = iterator.First(); !iterator.IsDone(); tile = iterator.Next())
            {
                gameMap.SetTile(tile.Item1, tile.Item2, mapTileImage);
                var prm = gameMap.CreateScaledGameObjectParameters(tile.Item1, tile.Item2, outerWallImage);
                gameMap._tiles[tile.Item1, tile.Item2].GameObjects.Add(new IndestructableWall(prm.Item1, prm.Item2, prm.Item3, prm.Item4));
            }
        }

        public GameMap GetMap()
        {
            return gameMap;
        }

        public abstract void AddSpecialTiles();
    }
}
