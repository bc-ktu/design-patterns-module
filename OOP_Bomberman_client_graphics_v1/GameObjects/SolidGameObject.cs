using client_graphics.Flyweight;
using Utils.Math;

namespace client_graphics.GameObjects
{
    public abstract class SolidGameObject : GameObject
    {
        public SolidGameObject() { }

        public SolidGameObject(SolidGameObject sgo) : base(sgo) { }

        public SolidGameObject(Vector2 position, Vector2 size, Vector4 collider, ImageFlyweight image)
           : base(position, size, collider, image)
        { }

        public SolidGameObject(int x, int y, int width, int height, int cx, int cy, int cWidth, int cHeight, ImageFlyweight image)
            : base(x, y, width, height, cx, cy, cWidth, cHeight, image)
        { }
    }
}
