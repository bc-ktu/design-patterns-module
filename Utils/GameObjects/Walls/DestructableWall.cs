using Utils.Flyweight;
using Utils.Math;

namespace Utils.GameObjects.Walls
{
    public abstract class DestructableWall : DestructableGameObject
    {
        public DestructableWall() { }

        public DestructableWall(DestructableWall dw) : base(dw) { }
     
        public DestructableWall(Vector2 position, Vector2 size, Vector4 collider, ImageFlyweight image)
            : base(position, size, collider, image)
        { }

        public DestructableWall(int x, int y, int width, int height, int cx, int cy, int cWidth, int cHeight, ImageFlyweight image)
            : base(x, y, width, height, cx, cy, cWidth, cHeight, image)
        { }

    }
}
