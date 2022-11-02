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

namespace Utils.AbstractFactory
{
    public interface ILevelFactory
    {
        public Explosive CreateExplosive(GameMap gameMap, Vector2 index);

        public DestructableWall CreateWall(Vector2 position, Vector2 size, Vector4 collider, Bitmap image);
        public DestructableWall CreateWall(int x, int y, int width, int height, int cx, int cy, int cWidth, int cHeight, Bitmap image);

        public Powerup CreatePowerup(Vector2 position, Vector2 size, Vector4 collider, Bitmap image);
        public Powerup CreatePowerup(int x, int y, int width, int height, int cx, int cy, int cWidth, int cHeight, Bitmap image);
    }
}
