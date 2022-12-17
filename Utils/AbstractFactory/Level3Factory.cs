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
using Utils.Map;
using Utils.Helpers;
using Utils.GameLogic;
using Utils.Flyweight;
using System.Diagnostics;

namespace Utils.AbstractFactory
{
    public class Level3Factory : ILevelFactory
    {
        private ImageFlyweight _explosiveImage;
        private ImageFlyweight _fireImage;
        private ImageFlyweight _powerupImage;
        private ImageFlyweight _rangePowerupImage;
        private ImageFlyweight _wallImage;
        private ImageFlyweight _crateImage;
        private ImageFlyweight _outerWallImage;

        public Level3Factory()
        {

        }

        public Bitmap GetSpecialTileImage()
        {
            string path = Pather.Create(Pather.FolderAssets, Pather.FolderTextures, Pather.FolderSpritesheets, Pather.IceTileImage);
            return new Bitmap(path);
        }

        public MapBuilder CreateBuilder(Vector2 mapSize, Vector2 viewSize, List<int> mapSeed, Bitmap mapTileImage, Bitmap specTileImage, ILevelFactory levelFactory)
        {
            Stopwatch watch = Stopwatch.StartNew();

            if (_crateImage == null || _outerWallImage == null)
            {
                string filepath = Pather.Create(Pather.FolderAssets, Pather.FolderTextures, Pather.FolderSprites, Pather.FolderExplodables, Pather.CrateImage);
                Bitmap crateImage = new Bitmap(filepath);
                _crateImage = new ImageFlyweight(crateImage);
                filepath = Pather.Create(Pather.FolderAssets, Pather.FolderTextures, Pather.FolderSprites, Pather.FolderWalls, Pather.OuterWallImage);
                Bitmap outerWallImage = new Bitmap(filepath);
                _outerWallImage = new ImageFlyweight(outerWallImage);
            }

            watch.Stop();
            if (GameSettings.CalculateTimeDiagnostics)
                IO.WriteToFile(Pather.Create(Pather.FolderAssets, Pather.FolderTextFiles, Pather.TimeDiagnostics), watch.Elapsed.ToString());

            return new L3MapBuilder(mapSize, viewSize, mapSeed, mapTileImage, _crateImage, _outerWallImage, specTileImage, levelFactory);
        }

        public Explosive CreateExplosive(GameMap gameMap, Vector2 index)
        {
            Stopwatch watch = Stopwatch.StartNew();

            if (_explosiveImage == null)
            {
                string filepath = Pather.Create(Pather.FolderAssets, Pather.FolderTextures, Pather.FolderGUI, Pather.GuiDamageIcon);
                Bitmap fireImage = new Bitmap(filepath);
                _fireImage = new ImageFlyweight(fireImage);
                filepath = Pather.Create(Pather.FolderAssets, Pather.FolderTextures, Pather.FolderSprites, Pather.FolderExplosives, Pather.ExplosiveImage);
                Bitmap explosiveImage = new Bitmap(filepath);
                _explosiveImage = new ImageFlyweight(explosiveImage);
            }

            var prmf = gameMap.CreateScaledGameObjectParameters(index.X, index.Y, _fireImage.Image, GameSettings.ExplosiveColliderScale);
            Fire fire = new Fire(prmf.Item1, prmf.Item2, prmf.Item3, _fireImage);
            var prm = gameMap.CreateScaledGameObjectParameters(index.X, index.Y, _explosiveImage.Image, GameSettings.ExplosiveColliderScale);

            watch.Stop();
            if (GameSettings.CalculateTimeDiagnostics)
                IO.WriteToFile(Pather.Create(Pather.FolderAssets, Pather.FolderTextFiles, Pather.TimeDiagnostics), watch.Elapsed.ToString());

            return new ExplosiveHVDi(prm.Item1, prm.Item2, prm.Item3, _explosiveImage, fire);
        }

        public Powerup CreatePowerup(GameMap gameMap, Vector2 index)
        {
            Stopwatch watch = Stopwatch.StartNew();

            if (_powerupImage == null)
            {
                string filepath = Pather.Create(Pather.FolderAssets, Pather.FolderTextures, Pather.FolderSprites, Pather.FolderPowerups, Pather.RangePowerupImage);
                Bitmap rangeImage = new Bitmap(filepath);
                _rangePowerupImage = new ImageFlyweight(rangeImage);
                filepath = Pather.Create(Pather.FolderAssets, Pather.FolderTextures, Pather.FolderSprites, Pather.FolderPowerups, Pather.DamagePowerupImage);
                Bitmap image = new Bitmap(filepath);
                _powerupImage = new ImageFlyweight(image);
            }

            var prm = gameMap.CreateScaledGameObjectParameters(index.X, index.Y, _powerupImage.Image, GameSettings.PowerupColliderScale);
            
            Random rnd = new Random();
            double chance = rnd.NextDouble();

            watch.Stop();
            if (GameSettings.CalculateTimeDiagnostics)
                IO.WriteToFile(Pather.Create(Pather.FolderAssets, Pather.FolderTextFiles, Pather.TimeDiagnostics), watch.Elapsed.ToString());

            if (chance <= GameSettings.Level3RangePowerupChance)
                return new RangePowerup(prm.Item1, prm.Item2, prm.Item3, _rangePowerupImage);

            return new DamagePowerup(prm.Item1, prm.Item2, prm.Item3, _powerupImage);
        }

        public DestructableWall CreateWall(GameMap gameMap, Vector2 index)
        {
            Stopwatch watch = Stopwatch.StartNew();

            if (_wallImage == null)
            {
                string filepath = Pather.Create(Pather.FolderAssets, Pather.FolderTextures, Pather.FolderSprites, Pather.FolderWalls, Pather.StoneWallImage);
                Bitmap image = new Bitmap(filepath);
                _wallImage = new ImageFlyweight(image);
            }

            var prm = gameMap.CreateScaledGameObjectParameters(index.X, index.Y, _wallImage.Image);

            watch.Stop();
            if (GameSettings.CalculateTimeDiagnostics)
                IO.WriteToFile(Pather.Create(Pather.FolderAssets, Pather.FolderTextFiles, Pather.TimeDiagnostics), watch.Elapsed.ToString());

            return new StoneWall(prm.Item1, prm.Item2, prm.Item3, _wallImage);
        }
    }
}
