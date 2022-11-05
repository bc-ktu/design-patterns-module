using Utils.Math;

namespace Utils.Decorator
{
    public class IconDecorator : TitleDecorator
    {
        public Bitmap Icon { get; private set; }

        public IconDecorator() : base() 
        {
            Icon = new Bitmap(0, 0);
        }

        public IconDecorator(IGraphicsDecorator decorator, Bitmap icon, Vector2 offest, Vector2 size) : base(decorator, offest, size)
        {
            Icon = icon;
        }

        public override void Draw(PaintEventArgs e)
        {
            if (wrappee != null)
                wrappee.Draw(e);

            e.Graphics.DrawImage(Icon, ToRectangle());
        }

    }
}
