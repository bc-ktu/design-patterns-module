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
using client_graphics.Map;
using Utils.Helpers;
using client_graphics.GameLogic;
using client_graphics.GameObjects.Animates;

namespace client_graphics.AbstractFactory
{
    public class Level2Factory : ILevelFactory
    {
        public Level2Factory()
        {

        }

        public Bitmap GetSpecialTileImage()
        {
            string path = Pather.Create(Pather.FolderAssets, Pather.FolderTextures, Pather.FolderSpritesheets, Pather.PortalTileImage);
            return new Bitmap(path);
        }

        public MapBuilder CreateBuilder(Vector2 mapSize, Vector2 viewSize, List<int> mapSeed, Bitmap mapTileImage, Bitmap crateImage, Bitmap outerWallImage, Bitmap specTileImage, ILevelFactory levelFactory)
        {
            return new L2MapBuilder(mapSize, viewSize, mapSeed, mapTileImage, crateImage, outerWallImage, specTileImage, levelFactory);
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
            return new ExplosiveDi(prm.Item1, prm.Item2, prm.Item3, prm.Item4, fire);
        }

        public Powerup CreatePowerup(GameMap gameMap, Vector2 index)
        {
            string filepath = Pather.Create(Pather.FolderAssets, Pather.FolderTextures, Pather.FolderSprites, Pather.FolderPowerups, Pather.CapacityPowerupImage);
            Bitmap image = new Bitmap(filepath);
            var prm = gameMap.CreateScaledGameObjectParameters(index.X, index.Y, image, GameSettings.PowerupColliderScale);

            return new CapacityPowerup(prm.Item1, prm.Item2, prm.Item3, prm.Item4);
        }

        public DestructableWall CreateWall(GameMap gameMap, Vector2 index)
        {
            string filepath = Pather.Create(Pather.FolderAssets, Pather.FolderTextures, Pather.FolderSprites, Pather.FolderWalls, Pather.WoodenWallImage);
            Bitmap image = new Bitmap(filepath);

            var prm = gameMap.CreateScaledGameObjectParameters(index.X, index.Y, image);
            return new WoodenWall(prm.Item1, prm.Item2, prm.Item3, prm.Item4);
        }

        public Enemy GetEnemyType()
        {
            return new EnemyUD();
        }
    }
}
