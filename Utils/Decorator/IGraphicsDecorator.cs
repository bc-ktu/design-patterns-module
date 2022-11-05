using Utils.Math;

namespace Utils.Decorator
{
    public interface IGraphicsDecorator : IDrawable
    {
        protected IGraphicsDecorator wrappee { get; }
    }
}
