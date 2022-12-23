using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Decorator;

namespace client_graphics.Flyweight
{
    public class ImageFlyweight
    {
        public Bitmap Image { get; }

        public ImageFlyweight(Bitmap image)
        {
            Image = image;
        }

        public void Draw(PaintEventArgs e, Rectangle rectangle)
        {
            e.Graphics.DrawImage(Image, rectangle);
        }
    }
}
