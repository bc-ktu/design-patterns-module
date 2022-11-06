namespace Utils.Prototype
{
    public interface ICloneable<T> where T : class
    {
        public abstract T Clone();
    }
}
