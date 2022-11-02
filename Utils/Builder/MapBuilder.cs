﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.AbstractFactory;
using Utils.GameObjects;
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
            for (int y = 1; y < mapSize.Y - 1; y++)
            {
                for (int x = 1; x < mapSize.X - 1; x++)
                {
                    gameMap.SetTile(x, y, mapTileImage);
                    GameObject go = new EmptyGameObject();

                    int isEmpty = mapSeed[index];
                    if (isEmpty == 0)
                    {
                        // var prm = gameMap.CreateScaledGameObjectParameters(x, y, crateImage);
                        // go = new DestructableGameObject(prm.Item1, prm.Item2, prm.Item3, prm.Item4);
                        var prm = gameMap.CreateScaledGameObjectParameters(x, y, crateImage);
                        go = levelFactory.CreateWall(prm.Item1, prm.Item2, prm.Item3, prm.Item4);
                    }

                    gameMap[x, y].GameObject = go;
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
                gameMap[i, 0].GameObject = new IndestructableWall(prm.Item1, prm.Item2, prm.Item3, prm.Item4);

                gameMap.SetTile(i, mapSize.Y - 1, mapTileImage);
                prm = gameMap.CreateScaledGameObjectParameters(i, mapSize.Y - 1, outerWallImage);
                gameMap[i, mapSize.Y - 1].GameObject = new IndestructableWall(prm.Item1, prm.Item2, prm.Item3, prm.Item4);
            }

            for (int i = 1; i < mapSize.Y - 1; i++)
            {
                gameMap.SetTile(0, i, mapTileImage);
                var prm = gameMap.CreateScaledGameObjectParameters(0, i, outerWallImage);
                gameMap[0, i].GameObject = new IndestructableWall(prm.Item1, prm.Item2, prm.Item3, prm.Item4);

                gameMap.SetTile(mapSize.X - 1, i, mapTileImage);
                prm = gameMap.CreateScaledGameObjectParameters(mapSize.X - 1, i, outerWallImage);
                gameMap[mapSize.X - 1, i].GameObject = new IndestructableWall(prm.Item1, prm.Item2, prm.Item3, prm.Item4);
            }
        }

        public GameMap GetMap()
        {
            return gameMap;
        }

        public abstract void AddSpecialTiles();
    }
}
