using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

using Utils.Math;

namespace Utils.GUIElements
{
    public class GUIText
    {
        private Vector2 _position;
        private Vector2 _size;
        private string _text;

        public Vector2 Position { get { return _position; } }
        public Vector2 Size { get { return _size; } }
        public string Text { get { return _text; } }

        public GUIText()
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

        public void SetText(string text)
        {
            _text = text;
        }

        public Rectangle ToRectangle()
        {
            return new Rectangle(_position.X, _position.Y, _size.X, _size.Y);
        }
    }
}
