using Utils.Math;

namespace Utils.Decorator
{
    public abstract class TitleDecorator : IGraphicsDecorator
    {
        protected Vector2 _offset;

        public IGraphicsDecorator wrappee { get; private set; }

        public Vector2 LocalPosition { get { return wrappee.LocalPosition + _offset; } }
        public Vector2 Size { get; protected set; }

        public TitleDecorator()
        {
            _offset = new Vector2(0, 0);
            wrappee = null;
            Size = new Vector2(0, 0);
        }

        public TitleDecorator(IGraphicsDecorator decorator, Vector2 offest, Vector2 size)
        {
            _offset = offest;
            wrappee = decorator;
            Size = size;
        }

        public abstract void Draw(PaintEventArgs e);

        public Rectangle ToRectangle()
        {
            return new Rectangle(LocalPosition.X, LocalPosition.Y, Size.X, Size.Y);
        }
    }
}
