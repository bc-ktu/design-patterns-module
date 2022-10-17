using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Utils.GameObjects;
using Utils.GameObjects.Animates;
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

            gui = new GUI(position, size, frameImage);
            gui.SetHealthImage(healthIcon);
            gui.SetSpeedImage(speedIcon);
            gui.SetCapacityImage(capacityIcon);
            gui.SetRangeImage(rangeIcon);
            gui.SetDamageImage(damageIcon);

            return gui;
        }

        public static GameMap CreateMap(Vector2 mapSize, Vector2 viewSize, List<int> mapSeed, Vector2 groundSpritesheetIndex)
        {
            GameMap gameMap = new GameMap(mapSize, viewSize);
            string filepath;

            filepath = Pather.Create(Pather.FolderAssets, Pather.FolderTextures, Pather.FolderSpritesheets, Pather.MapSpritesheet);
            Bitmap mapSpritesheet = new Bitmap(filepath);
            Bitmap mapTileImage = Spritesheet.ExtractSprite(mapSpritesheet, new Vector2(32, 32), groundSpritesheetIndex);
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
                    gameMap.SetTile(x, y, mapTileImage);
                    GameObject go = new EmptyGameObject();

                    int isEmpty = mapSeed[index];
                    if (isEmpty == 0)
                    {
                        var prm = gameMap.CreateScaledGameObjectParameters(x, y, crateImage);
                        go = new DestructableGameObject(prm.Item1, prm.Item2, prm.Item3, prm.Item4);
                    }

                    gameMap[x, y].GameObject = go;
                    index++;
                }
            }

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

            return gameMap;
        }

        public static Character CreatePlayer(GameMap gameMap, Vector2 playerSpritesheetIndex)
        {
            string filepath;

            filepath = Pather.Create(Pather.FolderAssets, Pather.FolderTextures, Pather.FolderSpritesheets, Pather.CharacterSpritesheet);
            Bitmap charactersSpritesheet = new Bitmap(filepath);
            Bitmap[,] characterImages = Spritesheet.ExtractAll(charactersSpritesheet, new Vector2(32, 32));
            filepath = Pather.Create(Pather.FolderAssets, Pather.FolderTextures, Pather.FolderSprites, Pather.FolderExplosives, Pather.ExplosiveImage);
            Bitmap explosiveImage = new Bitmap(filepath);
            filepath = Pather.Create(Pather.FolderAssets, Pather.FolderTextures, Pather.FolderGUI, Pather.GuiDamageIcon);
            Bitmap fireImage = new Bitmap(filepath);

            Vector2 position = gameMap.ViewSize / 2;
            double colliderSize = 0.75;
            int tlx = (int)(position.X + (1 - colliderSize) * gameMap.TileSize.X);
            int tly = (int)(position.Y + (1 - colliderSize) * gameMap.TileSize.Y);
            int brx = (int)(position.X + colliderSize * gameMap.TileSize.X);
            int bry = (int)(position.Y + colliderSize * gameMap.TileSize.Y);
            Vector4 collider = new Vector4(tlx, tly, brx, bry);

            return new Character(position, gameMap.TileSize, collider, characterImages[playerSpritesheetIndex.X, playerSpritesheetIndex.Y], explosiveImage, fireImage); // maybe later add not the image, but Explosive object
        }

    }
}
