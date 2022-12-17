using Utils.Flyweight;
using Utils.Math;

namespace Utils.GameObjects.Walls
{
    public class IndestructableWall : SolidGameObject
    {
        public IndestructableWall() { }

        public IndestructableWall(IndestructableWall iw) : base(iw) { }

        public IndestructableWall(Vector2 position, Vector2 size, Vector4 collider, ImageFlyweight image) 
            : base(position, size, collider, image)
        { }

        public IndestructableWall(int x, int y, int width, int height, int cx, int cy, int cWidth, int cHeight, ImageFlyweight image)
            : base(x, y, width, height, cx, cy, cWidth, cHeight, image)
        { }

        public override GameObject Clone()
        {
            return new IndestructableWall(this);
        }
    }
}
