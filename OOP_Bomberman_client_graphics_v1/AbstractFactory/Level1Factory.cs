using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using client_graphics.GameObjects.Walls;
using client_graphics.Builder;
using client_graphics.GameObjects.Explosives;
using client_graphics.GameObjects.Interactables;
using Utils.Math;
using Utils.Helpers;
using client_graphics.Map;
using client_graphics.GameLogic;
using client_graphics.Mediator;
using client_graphics.GameObjects.Animates;
using Utils.Enum;
using client_graphics.Flyweight;
using System.Diagnostics;

namespace client_graphics.AbstractFactory
{
    public class Level1Factory : ILevelFactory
    {
        private readonly IMediator _mediator;
        private ImageFlyweight _explosiveImage;
        private ImageFlyweight _fireImage;
        private ImageFlyweight _powerupImage;
        private ImageFlyweight _rangePowerupImage;
        private ImageFlyweight _wallImage;
        private ImageFlyweight _crateImage;
        private ImageFlyweight _outerWallImage;

        public Level1Factory()
        {
        }

        public Level1Factory(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Bitmap GetSpecialTileImage()
        {
            string path = Pather.Create(Pather.FolderAssets, Pather.FolderTextures, Pather.FolderSpritesheets, Pather.MudTileImage);
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

            return new L1MapBuilder(mapSize, viewSize, mapSeed, mapTileImage, _crateImage, _outerWallImage, specTileImage, levelFactory);
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

            return new ExplosiveHV(prm.Item1, prm.Item2, prm.Item3, _explosiveImage, fire);
        }

        public Powerup CreatePowerup(GameMap gameMap, Vector2 index)
        {
            return _mediator.Send(PowerupType.Speed, gameMap, index);
            Stopwatch watch = Stopwatch.StartNew();

            if (_powerupImage == null)
            {
                string filepath = Pather.Create(Pather.FolderAssets, Pather.FolderTextures, Pather.FolderSprites, Pather.FolderPowerups, Pather.RangePowerupImage);
                Bitmap rangeImage = new Bitmap(filepath);
                _rangePowerupImage = new ImageFlyweight(rangeImage);
                filepath = Pather.Create(Pather.FolderAssets, Pather.FolderTextures, Pather.FolderSprites, Pather.FolderPowerups, Pather.SpeedPowerupImage);
                Bitmap image = new Bitmap(filepath);
                _powerupImage = new ImageFlyweight(image);
            }

            var prm = gameMap.CreateScaledGameObjectParameters(index.X, index.Y, _powerupImage.Image, GameSettings.PowerupColliderScale);

            Random rnd = new Random();
            double chance = rnd.NextDouble();

            watch.Stop();
            if (GameSettings.CalculateTimeDiagnostics)
                IO.WriteToFile(Pather.Create(Pather.FolderAssets, Pather.FolderTextFiles, Pather.TimeDiagnostics), watch.Elapsed.ToString());

            if (chance <= GameSettings.Level1RangePowerupChance)
                return new RangePowerup(prm.Item1, prm.Item2, prm.Item3, _rangePowerupImage);

            return new SpeedPowerup(prm.Item1, prm.Item2, prm.Item3, _powerupImage);
        }

        public DestructableWall CreateWall(GameMap gameMap, Vector2 index)
        {
            Stopwatch watch = Stopwatch.StartNew();

            if (_wallImage == null)
            {
                string filepath = Pather.Create(Pather.FolderAssets, Pather.FolderTextures, Pather.FolderSprites, Pather.FolderWalls, Pather.PaperWallImage);
                Bitmap image = new Bitmap(filepath);
                _wallImage = new ImageFlyweight(image);
            }

            var prm = gameMap.CreateScaledGameObjectParameters(index.X, index.Y, _wallImage.Image);

            watch.Stop();
            if (GameSettings.CalculateTimeDiagnostics)
                IO.WriteToFile(Pather.Create(Pather.FolderAssets, Pather.FolderTextFiles, Pather.TimeDiagnostics), watch.Elapsed.ToString());

            return new PaperWall(prm.Item1, prm.Item2, prm.Item3, _wallImage);
        }

        public Enemy GetFirstEnemyType()
        {
            return new EnemyLR();
        }

        public Enemy GetSecondEnemyType()
        {
            return new EnemyUD();
        }
    }
}
