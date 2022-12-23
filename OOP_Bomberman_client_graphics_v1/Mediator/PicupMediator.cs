using client_graphics.GameLogic;
using client_graphics.GameObjects.Interactables;
using client_graphics.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Enum;
using Utils.Helpers;
using Utils.Math;

namespace client_graphics.Mediator
{
    public class PicupMediator : IMediator
    {
        public Powerup Send(PowerupType type, GameMap gameMap, Vector2 index)
        {
            return CreatePowerup(type, gameMap, index);
        }

        private Powerup CreatePowerup(PowerupType type, GameMap gameMap, Vector2 index)
        {
            switch (type)
            {
                case PowerupType.Speed:
                    string filepath = Pather.Create(Pather.FolderAssets, Pather.FolderTextures, Pather.FolderSprites, Pather.FolderPowerups, Pather.SpeedPowerupImage);
                    Bitmap image = new Bitmap(filepath);
                    var prm = gameMap.CreateScaledGameObjectParameters(index.X, index.Y, image, GameSettings.PowerupColliderScale);

                    return new SpeedPowerup(prm.Item1, prm.Item2, prm.Item3, image);

                case PowerupType.Range:
                    string filepathR = Pather.Create(Pather.FolderAssets, Pather.FolderTextures, Pather.FolderSprites, Pather.FolderPowerups, Pather.RangePowerupImage);
                    Bitmap imageR = new Bitmap(filepathR);
                    var prmR = gameMap.CreateScaledGameObjectParameters(index.X, index.Y, imageR, GameSettings.PowerupColliderScale);

                    return new RangePowerup(prmR.Item1, prmR.Item2, prmR.Item3, imageR);

                case PowerupType.Damage:
                    string filepathD = Pather.Create(Pather.FolderAssets, Pather.FolderTextures, Pather.FolderSprites, Pather.FolderPowerups, Pather.DamagePowerupImage);
                    Bitmap imageD = new Bitmap(filepathD);
                    var prmD = gameMap.CreateScaledGameObjectParameters(index.X, index.Y, imageD, GameSettings.PowerupColliderScale);

                    return new DamagePowerup(prmD.Item1, prmD.Item2, prmD.Item3, imageD);

                default:
                    return null;
            }
            
        }
    }
}
