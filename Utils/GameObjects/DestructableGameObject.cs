using Utils.Flyweight;
using Utils.Math;

namespace Utils.GameObjects
{
    public abstract class DestructableGameObject : SolidGameObject
    {
        public int Durability { get; protected set; }

        public DestructableGameObject() { }

        public DestructableGameObject(DestructableGameObject dsg) : base(dsg) 
        { 
            Durability = dsg.Durability;
        }

        public DestructableGameObject(Vector2 position, Vector2 size, Vector4 collider, ImageFlyweight image)
            : base(position, size, collider, image)
        { }

        public DestructableGameObject(int x, int y, int width, int height, int cx, int cy, int cWidth, int cHeight, ImageFlyweight image)
            : base(x, y, width, height, cx, cy, cWidth, cHeight, image)
        { }

        public void DecreaseDurability()
        {
            Durability--;
        }

    }
}
