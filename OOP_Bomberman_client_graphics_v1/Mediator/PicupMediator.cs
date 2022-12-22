using client_graphics.GameLogic;
using client_graphics.GameObjects.Interactables;
using client_graphics.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Helpers;
using Utils.Math;

namespace client_graphics.Mediator
{
    public class PicupMediator : IMediator
    {
        public Powerup Send(GameMap gameMap, Vector2 index)
        {
            return CreatePowerup(gameMap, index);
        }

        private Powerup CreatePowerup(GameMap gameMap, Vector2 index)
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
    }
}
