using Utils.Math;

namespace Utils.Decorator
{
    public interface IDrawable
    {
        public Vector2 LocalPosition { get; }
        public Vector2 WorldPosition { get { return LocalPosition + Size / 2; } }
        public Vector2 Size { get; }

        public abstract void Draw(PaintEventArgs e);
        public abstract Rectangle ToRectangle();
    }
}
