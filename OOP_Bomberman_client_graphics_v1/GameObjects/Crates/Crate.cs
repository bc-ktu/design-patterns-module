using client_graphics.AbstractFactory;
using client_graphics.GameLogic;
using client_graphics.GameObjects.Interactables;
using client_graphics.Map;
using client_graphics.Flyweight;
using Utils.Math;

namespace client_graphics.GameObjects.Crates
{
    public class Crate : DestructableGameObject
    {
        public Crate()
        {
            Durability = 1;
        }

        public Crate(Crate c) : base(c) { }

        public Crate(Vector2 position, Vector2 size, Vector4 collider, ImageFlyweight image)
            : base(position, size, collider, image)
        {
            Durability = 1;
        }

        public Crate(int x, int y, int width, int height, int cx, int cy, int cWidth, int cHeight, ImageFlyweight image)
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
