using System.Drawing.Text;

using Utils.AbstractFactory;
using Utils.Builder;
using Utils.Factory;
using Utils.GameObjects;
using Utils.GameObjects.Animates;
using Utils.GameObjects.Crates;
using Utils.GameObjects.Explosives;
using Utils.GameObjects.Walls;
using Utils.GUIElements;
using Utils.Helpers;
using Utils.Map;
using Utils.Math;
using Utils.Observer;

namespace Utils.GameLogic
{
    public static class GameInitializer
    {
        public static void LoadTextures()
        {
            
        }

        public static GUI CreateGUI(Vector2 position, Vector2 size, Brush fontColor, int fontSize)
        {
            GUI gui;
            PrivateFontCollection pfc = new PrivateFontCollection();

            string filepath = Pather.Create(Pather.FolderAssets, Pather.FolderTextures, Pather.FolderGUI, Pather.GUIFrameImage);
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

            filepath = Pather.Create(Pather.FolderAssets, Pather.FolderFonts, Pather.GuiFontFile);
            pfc.AddFontFile(filepath);
            Font font = new Font(pfc.Families[0].Name, fontSize);

            gui = new GUI(position, size, frameImage, font, fontColor);
            gui.SetHealthImage(healthIcon);
            gui.SetSpeedImage(speedIcon);
            gui.SetCapacityImage(capacityIcon);
            gui.SetRangeImage(rangeIcon);
            gui.SetDamageImage(damageIcon);

            return gui;
        }

        public static GameMap CreateMap(ILevelFactory levelFactory, Vector2 mapSize, Vector2 viewSize, List<int> mapSeed, Vector2 groundSpritesheetIndex)
        {
            string filepath;

            filepath = Pather.Create(Pather.FolderAssets, Pather.FolderTextures, Pather.FolderSpritesheets, Pather.MapSpritesheet);
            Bitmap mapSpritesheet = new Bitmap(filepath);
            Bitmap mapTileImage = Spritesheet.ExtractSprite(mapSpritesheet, new Vector2(32, 32), groundSpritesheetIndex);
            filepath = Pather.Create(Pather.FolderAssets, Pather.FolderTextures, Pather.FolderSprites, Pather.FolderExplodables, Pather.CrateImage);
            Bitmap crateImage = new Bitmap(filepath);
            filepath = Pather.Create(Pather.FolderAssets, Pather.FolderTextures, Pather.FolderSprites, Pather.FolderWalls, Pather.OuterWallImage);
            Bitmap outerWallImage = new Bitmap(filepath);
            Bitmap specTileImage = levelFactory.GetSpecialTileImage();

            Director director = new Director();
            MapBuilder builder = levelFactory.CreateBuilder(mapSize, viewSize, mapSeed, mapTileImage, crateImage, outerWallImage, specTileImage, levelFactory);// = new L2MapBuilder(mapSize, viewSize, mapSeed, mapTileImage, crateImage, outerWallImage, specTileImage, levelFactory);
            director.Construct(builder);
            return builder.GetMap();
        }

        public static Player CreatePlayer(ILevelFactory levelFactory, GameMap gameMap, Vector2 playerSpritesheetIndex, Subject subject)
        {
            string filepath = Pather.Create(Pather.FolderAssets, Pather.FolderTextures, Pather.FolderSpritesheets, Pather.CharacterSpritesheet);
            
            Vector2 position = new Vector2(1, 1);
            double colliderSize = GameSettings.PlayerColliderScale;
            int tlx = (int)(position.X + (1 - colliderSize));
            int tly = (int)(position.Y + (1 - colliderSize));
            int brx = (int)(position.X + colliderSize);
            int bry = (int)(position.Y + colliderSize);
            Vector4 collider = new Vector4(tlx, tly, brx, bry);

            Vector2 index = position;

            return new Player(position, null, collider, null, null, subject);
        }
    }
}
