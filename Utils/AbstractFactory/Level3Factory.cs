using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Utils.GameObjects.Walls;
using Utils.GameObjects.Explosives;
using Utils.GameObjects.Interactables;
using Utils.Math;
using Utils.Map;
using Utils.Helpers;

namespace Utils.AbstractFactory
{
    public class Level3Factory : ILevelFactory
    {
        public Level3Factory()
        {

        }

        public Explosive CreateExplosive(GameMap gameMap, Vector2 index)
        {
            string filepath = Pather.Create(Pather.FolderAssets, Pather.FolderTextures, Pather.FolderGUI, Pather.GuiDamageIcon);
            Bitmap fireImage = new Bitmap(filepath);
            filepath = Pather.Create(Pather.FolderAssets, Pather.FolderTextures, Pather.FolderSprites, Pather.FolderExplosives, Pather.ExplosiveImage);
            Bitmap explosiveImage = new Bitmap(filepath);

            var prm = gameMap.CreateScaledGameObjectParameters(index.X, index.Y, explosiveImage);
            return new ExplosiveHVDi(prm.Item1, prm.Item2, prm.Item3, prm.Item4, fireImage);
        }

        public Powerup CreatePowerup(Vector2 position, Vector2 size, Vector4 collider, Bitmap image)
        {
            return new DamagePowerup(position, size, collider, image);
        }

        public Powerup CreatePowerup(int x, int y, int width, int height, int cx, int cy, int cWidth, int cHeight, Bitmap image)
        {
            return new DamagePowerup(x, y, width, height, cx, cy, cWidth, cHeight, image);
        }

        public DestructableWall CreateWall(Vector2 position, Vector2 size, Vector4 collider, Bitmap image)
        {
            return new StoneWall(position, size, collider, image);
        }

        public DestructableWall CreateWall(int x, int y, int width, int height, int cx, int cy, int cWidth, int cHeight, Bitmap image)
        {
            return new StoneWall(x, y, width, height, cx, cy, cWidth, cHeight, image);
        }
    }
}
