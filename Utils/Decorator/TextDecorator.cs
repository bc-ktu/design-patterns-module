using Utils.GameLogic;
using Utils.Math;

namespace Utils.Decorator
{
    public class TextDecorator : TitleDecorator
    {
        public string Text { get; private set; }
        public Font Font { get; private set; }
        public Brush Color { get; private set; }

        public TextDecorator() : base()
        {
            Text = "";
            Font = GameSettings.DecoratorFont;
            Color = GameSettings.GUIFontColor;
        }

        public TextDecorator(IGraphicsDecorator decorator, string text, Font font, Brush color, Vector2 offest, Vector2 size) 
            : base(decorator, offest, size)
        {
            Text = text;
            Font = font;
            Color = color;
        }

        public override void Draw(PaintEventArgs e)
        {
            if (wrappee != null)
                wrappee.Draw(e);
            
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;
            e.Graphics.DrawString(Text, Font, Color, ToRectangle(), stringFormat);
        }
    }
}
