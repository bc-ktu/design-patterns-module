using Utils.Flyweight;
using Utils.Math;

namespace Utils.GameObjects.Walls
{
    internal class StoneWall : DestructableWall
    {
        public StoneWall() 
        {
            Durability = 7;
        }

        public StoneWall(StoneWall sw) : base(sw) { }

        public StoneWall(Vector2 position, Vector2 size, Vector4 collider, ImageFlyweight image) 
            : base(position, size, collider, image)
        {
            Durability = 7;
        }

        public StoneWall(int x, int y, int width, int height, int cx, int cy, int cWidth, int cHeight, ImageFlyweight image)
            : base(x, y, width, height, cx, cy, cWidth, cHeight, image)
        {
            Durability = 7;
        }

        public override GameObject Clone()
        {
            return new StoneWall(this);
        }

    }
}
