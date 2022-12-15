using Utils.Math;

namespace client_graphics.GameObjects.Walls
{
    internal class PaperWall : DestructableWall
    {
        public PaperWall() 
        {
            Durability = 3;
        }

        public PaperWall(PaperWall pw) : base(pw) { }

        public PaperWall(Vector2 position, Vector2 size, Vector4 collider, Bitmap image) 
            : base(position, size, collider, image)
        {
            Durability = 3;
        }

        public PaperWall(int x, int y, int width, int height, int cx, int cy, int cWidth, int cHeight, Bitmap image)
            : base(x, y, width, height, cx, cy, cWidth, cHeight, image)
        {
            Durability = 3;
        }

        public override GameObject Clone()
        {
            return new PaperWall(this);
        }

    }
}
