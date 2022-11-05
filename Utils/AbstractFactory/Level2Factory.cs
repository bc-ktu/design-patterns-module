﻿using Utils.GameObjects.Walls;
using Utils.GameObjects.Explosives;
using Utils.GameObjects.Interactables;
using Utils.Math;
using Utils.Map;
using Utils.Helpers;
using Utils.GameLogic;

namespace Utils.AbstractFactory
{
    public class Level2Factory : ILevelFactory
    {
        public Level2Factory()
        {

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
            string filepath = Pather.Create(Pather.FolderAssets, Pather.FolderTextures, Pather.FolderSprites, Pather.FolderPowerups, Pather.RangePowerupImage);
            Bitmap rangeImage = new Bitmap(filepath);
            filepath = Pather.Create(Pather.FolderAssets, Pather.FolderTextures, Pather.FolderSprites, Pather.FolderPowerups, Pather.CapacityPowerupImage);
            Bitmap image = new Bitmap(filepath);
            var prm = gameMap.CreateScaledGameObjectParameters(index.X, index.Y, image, GameSettings.PowerupColliderScale);

            Random rnd = new Random();
            double chance = rnd.NextDouble();

            if (chance <= GameSettings.Level2RangePowerupChance)
                return new RangePowerup(prm.Item1, prm.Item2, prm.Item3, rangeImage);

            return new CapacityPowerup(prm.Item1, prm.Item2, prm.Item3, prm.Item4);
        }

        public DestructableWall CreateWall(GameMap gameMap, Vector2 index)
        {
            string filepath = Pather.Create(Pather.FolderAssets, Pather.FolderTextures, Pather.FolderSprites, Pather.FolderWalls, Pather.WoodenWallImage);
            Bitmap image = new Bitmap(filepath);

            var prm = gameMap.CreateScaledGameObjectParameters(index.X, index.Y, image);
            return new WoodenWall(prm.Item1, prm.Item2, prm.Item3, prm.Item4);
        }

    }
}
