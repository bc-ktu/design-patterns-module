using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Utils.AbstractFactory;
using Utils.GameLogic;
using Utils.GameObjects.Interactables;
using Utils.Map;
using Utils.Math;

namespace Utils.GameObjects.Crates
{
    public class Crate : DestructableGameObject
    {
        public Crate()
        {
            Durability = 1;
        }

        public Crate(Crate c) : base(c) { }

        public Crate(Vector2 position, Vector2 size, Vector4 collider, Bitmap image)
            : base(position, size, collider, image)
        {
            Durability = 1;
        }

        public Crate(int x, int y, int width, int height, int cx, int cy, int cWidth, int cHeight, Bitmap image)
            : base(x, y, width, height, cx, cy, cWidth, cHeight, image)
        {

            Durability = 1;
        }

        public void CreatePowerup(GameMap gameMap, ILevelFactory levelFactory)
        {
            Random rand = new Random();
            double chance = rand.NextDouble();

            if (chance >= GameSettings.CrateDropRate)
                return;

            Vector2 index = WorldPosition / gameMap.TileSize;
            Powerup powerup = levelFactory.CreatePowerup(gameMap, index);

            gameMap[index].GameObjects.Add(powerup);
            gameMap.PowerupLookupTable.Add(index, powerup);
        }

        public override GameObject Clone()
        {
            return new Crate(this);
        }

    }
}
