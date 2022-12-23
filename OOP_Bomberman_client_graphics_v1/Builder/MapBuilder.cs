using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using client_graphics.AbstractFactory;
using client_graphics.GameObjects;
using client_graphics.GameObjects.Crates;
using client_graphics.GameObjects.Walls;
using client_graphics.Iterator;
using client_graphics.Map;
using client_graphics.Flyweight;
using Utils.Math;

namespace client_graphics.Builder
{
    public abstract class MapBuilder
    {
        protected GameMap gameMap;
        protected Vector2 mapSize { get; set; }
        protected List<int> mapSeed { get; set; }
        protected Bitmap mapTileImage { get; set; }
        protected ImageFlyweight crateImage { get; set; }
        protected ImageFlyweight outerWallImage { get; set; }
        protected Bitmap specTileImage { get; set; }
        protected ILevelFactory levelFactory { get; set; }

        public MapBuilder(Vector2 mapSize, Vector2 viewSize, List<int> mapSeed, Bitmap mapTileImage, ImageFlyweight crateImage, ImageFlyweight outerWallImage, ILevelFactory levelFactory)
        {
            this.mapSize = mapSize;
            this.mapSeed = mapSeed;
            this.gameMap = new GameMap(mapSize, viewSize);
            this.mapTileImage = mapTileImage;
            this.crateImage = crateImage;
            this.outerWallImage = outerWallImage;
            this.levelFactory = levelFactory;
        }

        private List<Vector2> GetRangePowerupSpawnPoints()
        {
            List<Vector2> spawnPoints = new List<Vector2>();
            for (int i = mapSeed.Count - 20; i < mapSeed.Count - 10; i+=2)
            {
                spawnPoints.Add(new Vector2(mapSeed[i], mapSeed[i + 1]));
            }
            return spawnPoints;
        }

        public void AddCrates()
        {
            List<Vector2> rangePowerups = GetRangePowerupSpawnPoints();
            int index = 0;
            MapIterator iterator = new InnerMapIterator(gameMap);
            for (Vector2 t = iterator.First(); !iterator.IsDone(); t = iterator.Next())
            {
                int x = t.X;
                int y = t.Y;
                gameMap.SetTile(x, y, mapTileImage);

                    if (x == 1 && y == 1 || x == 1 && y == 2 || x == 2 && y == 1 ||
                        x == mapSize.X - 2 && y == mapSize.X - 2 ||
                        x == mapSize.X - 2 && y == mapSize.X - 3 ||
                        x == mapSize.X - 3 && y == mapSize.X - 2)
                        continue;

                    int tile = mapSeed[index];
                    if (tile == 0)
                    {
                        GameObject go = levelFactory.CreateWall(gameMap, new Vector2(x, y));
                        gameMap[x, y].GameObjects.Add(go);
                    }
                    else
                    {
                        GameObject go = levelFactory.CreatePowerup(gameMap, new Vector2(x, y));
                        if (rangePowerups.Contains(new Vector2(x, y))) go = levelFactory.CreateRangePowerup(gameMap, new Vector2(x, y));
                        gameMap[x, y].GameObjects.Add(go);
                        gameMap.PowerupLookupTable.Add(new Vector2(x, y), go);
                        if (tile == 1 || tile == 2 || tile == 3)
                        {
                            var prm = gameMap.CreateScaledGameObjectParameters(x, y, crateImage.Image);
                            go = new Crate(prm.Item1, prm.Item2, prm.Item3, crateImage);
                            gameMap[x, y].GameObjects.Add(go);
                        }
                    }

                index++;
            }
        }

        public void AddOuterRing()
        {
            MapIterator iterator = new OuterRingIterator(gameMap);
            for (Vector2 tile = iterator.First(); !iterator.IsDone(); tile = iterator.Next())
            {
                gameMap.SetTile(tile.X, tile.Y, mapTileImage);
                var prm = gameMap.CreateScaledGameObjectParameters(tile.X, tile.Y, outerWallImage.Image);
                gameMap._tiles[tile.X, tile.Y].GameObjects.Add(new IndestructableWall(prm.Item1, prm.Item2, prm.Item3, outerWallImage));
            }
        }

        public GameMap GetMap()
        {
            return gameMap;
        }

        public abstract void AddSpecialTiles();
    }
}
