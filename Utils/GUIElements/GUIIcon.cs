using Utils.Math;

namespace Utils.GUIElements
{
    public class GUIIcon
    {
        private Vector2 _position;
        private Vector2 _size;
        private Bitmap _image;

        public Vector2 Position { get { return _position; } }
        public Vector2 Size { get { return _size; } }
        public Bitmap Image { get { return _image; } }

        public GUIIcon()
        {

        }

        public void SetPosition(int x, int y)
        {
            _position = new Vector2(x, y);
        }

        public void SetPosition(Vector2 position)
        {
            _position = position;
        }

        public void SetSize(int width, int height)
        {
            _size = new Vector2(width, height);
        }

        public void SetSize(Vector2 size)
        {
            _size = size;
        }

        public void SetImage(Bitmap image)
        {
            _image = image;
        }
    
        public Rectangle ToRectangle()
        {
            return new Rectangle(_position.X, _position.Y, _size.X, _size.Y);
        }

    }
}
