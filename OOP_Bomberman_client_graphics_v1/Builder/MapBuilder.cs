﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using client_graphics.AbstractFactory;
using client_graphics.GameObjects;
using client_graphics.GameObjects.Crates;
using client_graphics.GameObjects.Walls;
using client_graphics.Map;
using Utils.Math;

namespace client_graphics.Builder
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
            for (int y = 1; y < mapSize.Y - 1; y++)
            {
                for (int x = 1; x < mapSize.X - 1; x++)
                {
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
                            var prm = gameMap.CreateScaledGameObjectParameters(x, y, crateImage);
                            go = new Crate(prm.Item1, prm.Item2, prm.Item3, prm.Item4);
                            gameMap[x, y].GameObjects.Add(go);
                        }
                    }

                    index++;
                }
            }
        }

        public void AddOuterRing()
        {
            for (int i = 0; i < mapSize.X; i++)
            {
                gameMap.SetTile(i, 0, mapTileImage);
                var prm = gameMap.CreateScaledGameObjectParameters(i, 0, outerWallImage);
                gameMap[i, 0].GameObjects.Add(new IndestructableWall(prm.Item1, prm.Item2, prm.Item3, prm.Item4));

                gameMap.SetTile(i, mapSize.Y - 1, mapTileImage);
                prm = gameMap.CreateScaledGameObjectParameters(i, mapSize.Y - 1, outerWallImage);
                gameMap[i, mapSize.Y - 1].GameObjects.Add(new IndestructableWall(prm.Item1, prm.Item2, prm.Item3, prm.Item4));
            }

            for (int i = 1; i < mapSize.Y - 1; i++)
            {
                gameMap.SetTile(0, i, mapTileImage);
                var prm = gameMap.CreateScaledGameObjectParameters(0, i, outerWallImage);
                gameMap[0, i].GameObjects.Add(new IndestructableWall(prm.Item1, prm.Item2, prm.Item3, prm.Item4));

                gameMap.SetTile(mapSize.X - 1, i, mapTileImage);
                prm = gameMap.CreateScaledGameObjectParameters(mapSize.X - 1, i, outerWallImage);
                gameMap[mapSize.X - 1, i].GameObjects.Add(new IndestructableWall(prm.Item1, prm.Item2, prm.Item3, prm.Item4));
            }
        }

        public GameMap GetMap()
        {
            return gameMap;
        }

        public abstract void AddSpecialTiles();
    }
}
