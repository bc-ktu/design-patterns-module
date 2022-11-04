using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Builder;
using Utils.GameObjects.Walls;
using Utils.GameObjects.Explosives;
using Utils.GameObjects.Interactables;
using Utils.Helpers;
using Utils.Math;
using Utils.Map;

namespace Utils.AbstractFactory
{
    public interface ILevelFactory
    {
        public Explosive CreateExplosive(GameMap gameMap, Vector2 index);
        public Powerup CreatePowerup(GameMap gameMap, Vector2 index);
        public DestructableWall CreateWall(GameMap gameMap, Vector2 index);
        public MapBuilder CreateBuilder(Vector2 mapSize, Vector2 viewSize, List<int> mapSeed, Bitmap mapTileImage, Bitmap crateImage, Bitmap outerWallImage, Bitmap specTileImage, ILevelFactory levelFactory);
        public Bitmap GetSpecialTileImage();
    }
}
