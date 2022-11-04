using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.GameObjects.Walls;
using Utils.Builder;
using Utils.GameObjects.Explosives;
using Utils.GameObjects.Interactables;
using Utils.Math;
using Utils.Helpers;
using Utils.Map;
using System.Drawing;
using System.Reflection;
using Utils.GameLogic;

namespace Utils.AbstractFactory
{
    public class Level1Factory : ILevelFactory
    {
        public Level1Factory() 
        {

        }

        public Bitmap GetSpecialTileImage()
        {
            string path = Pather.Create(Pather.FolderAssets, Pather.FolderTextures, Pather.FolderSpritesheets, Pather.MudTileImage);
            return new Bitmap(path);
        }

        public MapBuilder CreateBuilder(Vector2 mapSize, Vector2 viewSize, List<int> mapSeed, Bitmap mapTileImage, Bitmap crateImage, Bitmap outerWallImage, Bitmap specTileImage, ILevelFactory levelFactory)
        {
            return new L1MapBuilder(mapSize, viewSize, mapSeed, mapTileImage, crateImage, outerWallImage, specTileImage, levelFactory);
        }

        public Explosive CreateExplosive(GameMap gameMap, Vector2 index)
        {
            
string filepath = Pather.Create(Pather.FolderAssets, Pather.FolderTextures, Pather.FolderGUI, Pather.GuiDamageIcon);
            Bitmap fireImage = new Bitmap(filepath);
            filepath = Pather.Create(Pather.FolderAssets, Pather.FolderTextures, Pather.FolderSprites, Pather.FolderExplosives, Pather.ExplosiveImage);
            Bitmap explosiveImage = new Bitmap(filepath);

            var prmf = gameMap.CreateScaledGameObjectParameters(index.X, index.Y, fireImage, GameSettings.ExplosiveColliderScale);
            Fire fire = new Fire(prmf.Item1, prmf.Item2, prmf.Item3, prmf.Item4);
            var prm = gameMap.CreateScaledGameObjectParameters(index.X, index.Y, explosiveImage, GameSettings.ExplosiveColliderScale);
            return new ExplosiveHV(prm.Item1, prm.Item2, prm.Item3, prm.Item4, fire);
        }

        public Powerup CreatePowerup(GameMap gameMap, Vector2 index)
        {
            string filepath = Pather.Create(Pather.FolderAssets, Pather.FolderTextures, Pather.FolderSprites, Pather.FolderPowerups, Pather.RangePowerupImage);
            Bitmap rangeImage = new Bitmap(filepath);
            filepath = Pather.Create(Pather.FolderAssets, Pather.FolderTextures, Pather.FolderSprites, Pather.FolderPowerups, Pather.SpeedPowerupImage);
            Bitmap image = new Bitmap(filepath);
            var prm = gameMap.CreateScaledGameObjectParameters(index.X, index.Y, image, GameSettings.PowerupColliderScale);

            Random rnd = new Random();
            double chance = rnd.NextDouble();

            if (chance <= GameSettings.Level1RangePowerupChance)
                return new RangePowerup(prm.Item1, prm.Item2, prm.Item3, rangeImage);

            return new SpeedPowerup(prm.Item1, prm.Item2, prm.Item3, prm.Item4);
        }

        public DestructableWall CreateWall(GameMap gameMap, Vector2 index)
        {
            string filepath = Pather.Create(Pather.FolderAssets, Pather.FolderTextures, Pather.FolderSprites, Pather.FolderWalls, Pather.PaperWallImage);
            Bitmap image = new Bitmap(filepath);

            var prm = gameMap.CreateScaledGameObjectParameters(index.X, index.Y, image);
            return new PaperWall(prm.Item1, prm.Item2, prm.Item3, prm.Item4);
        }
    }
}
