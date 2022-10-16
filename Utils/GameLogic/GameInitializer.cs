using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.GameObjects;
using Utils.GameObjects.Destructables;
using Utils.GUIElements;
using Utils.Helpers;
using Utils.Math;

namespace Utils.GameLogic
{
    public static class GameInitializer
    {
        public static GUI CreateGUI(Vector2 position, Vector2 size)
        {
            GUI gui;
            string filepath;

            filepath = Pather.Create(Pather.FolderAssets, Pather.FolderTextures, Pather.FolderGUI, Pather.GUIFrameImage);
            Bitmap frameImage = new Bitmap(filepath);
            filepath = Pather.Create(Pather.FolderAssets, Pather.FolderTextures, Pather.FolderGUI, Pather.GUIHealthIcon);
            Bitmap healthIcon = new Bitmap(filepath);
            filepath = Pather.Create(Pather.FolderAssets, Pather.FolderTextures, Pather.FolderGUI, Pather.GUISpeedIcon);
            Bitmap speedIcon = new Bitmap(filepath);
            filepath = Pather.Create(Pather.FolderAssets, Pather.FolderTextures, Pather.FolderGUI, Pather.GUICapacityIcon);
            Bitmap capacityIcon = new Bitmap(filepath);
            filepath = Pather.Create(Pather.FolderAssets, Pather.FolderTextures, Pather.FolderGUI, Pather.GuiRangeIcon);
            Bitmap rangeIcon = new Bitmap(filepath);
            filepath = Pather.Create(Pather.FolderAssets, Pather.FolderTextures, Pather.FolderGUI, Pather.GuiDamageIcon);
            Bitmap damageIcon = new Bitmap(filepath);

            Vector2 guiPosition = new Vector2(0, 0);
            gui = new GUI(position, size, frameImage);
            gui.SetHealthImage(healthIcon);
            gui.SetSpeedImage(speedIcon);
            gui.SetCapacityImage(capacityIcon);
            gui.SetRangeImage(rangeIcon);
            gui.SetDamageImage(damageIcon);

            return gui;
        }

        public static GameMap CreateMap(Vector2 mapSize, Vector2 viewSize, List<int> mapSeed)
        {
            Vector2 GTI = new Vector2(0, 0);

            GameMap gameMap = new GameMap(mapSize, viewSize);
            string filepath;

            filepath = Pather.Create(Pather.FolderAssets, Pather.FolderTextures, Pather.FolderSpritesheets, Pather.MapSpritesheet);
            Bitmap mapSpritesheet = new Bitmap(filepath);
            Bitmap[,] mapTileImages = Spritesheet.ExtractAll(mapSpritesheet, new Vector2(32, 32));

            filepath = Pather.Create(Pather.FolderAssets, Pather.FolderTextures, Pather.FolderSprites, Pather.FolderExplodables, Pather.CrateImage);
            Bitmap crateImage = new Bitmap(filepath);
            filepath = Pather.Create(Pather.FolderAssets, Pather.FolderTextures, Pather.FolderSprites, Pather.FolderWalls, Pather.InnerWallImage);
            Bitmap wallImage = new Bitmap(filepath);
            filepath = Pather.Create(Pather.FolderAssets, Pather.FolderTextures, Pather.FolderSprites, Pather.FolderWalls, Pather.OuterWallImage);
            Bitmap outerWallImage = new Bitmap(filepath);

            int index = 0;
            for (int y = 1; y < mapSize.Y - 1; y++)
            {
                for (int x = 1; x < mapSize.X - 1; x++)
                {
                    gameMap.SetTile(x, y, mapTileImages[GTI.X, GTI.Y]);
                    GameObject go = new EmptyGameObject();

                    int isEmpty = mapSeed[index];
                    if (isEmpty == 0)
                    {
                        var prm = gameMap.CreateScaledGameObjectParameters(x, y, crateImage);
                        go = new DestructableGameObject(prm.Item1, prm.Item2, prm.Item3, prm.Item4);
                    }

                    gameMap.Tiles[x, y].GameObject = go;
                    index++;
                }
            }

            for (int i = 0; i < mapSize.X; i++)
            {
                gameMap.SetTile(i, 0, mapTileImages[GTI.X, GTI.Y]);
                var prm = gameMap.CreateScaledGameObjectParameters(i, 0, outerWallImage);
                gameMap.Tiles[i, 0].GameObject = new IndestructableWall(prm.Item1, prm.Item2, prm.Item3, prm.Item4);

                gameMap.SetTile(i, mapSize.Y - 1, mapTileImages[GTI.X, GTI.Y]);
                prm = gameMap.CreateScaledGameObjectParameters(i, mapSize.Y - 1, outerWallImage);
                gameMap.Tiles[i, mapSize.Y - 1].GameObject = new IndestructableWall(prm.Item1, prm.Item2, prm.Item3, prm.Item4);
            }

            for (int i = 1; i < mapSize.Y - 1; i++)
            {
                gameMap.SetTile(0, i, mapTileImages[GTI.X, GTI.Y]);
                var prm = gameMap.CreateScaledGameObjectParameters(0, i, outerWallImage);
                gameMap.Tiles[0, i].GameObject = new IndestructableWall(prm.Item1, prm.Item2, prm.Item3, prm.Item4);

                gameMap.SetTile(mapSize.X - 1, i, mapTileImages[GTI.X, GTI.Y]);
                prm = gameMap.CreateScaledGameObjectParameters(mapSize.X - 1, i, outerWallImage);
                gameMap.Tiles[mapSize.X - 1, i].GameObject = new IndestructableWall(prm.Item1, prm.Item2, prm.Item3, prm.Item4);
            }

            return gameMap;
        }
    }
}
