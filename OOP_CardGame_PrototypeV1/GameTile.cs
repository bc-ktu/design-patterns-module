using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_CardGame_PrototypeV1
{
    internal class GameTile : Interactable
    {
        private Bitmap _image;

        public Bitmap Image { get { return _image; } }

        public GameTile(Bitmap image, Vector2 position, Vector2 size, bool upsideDown = false)
        {
            _image = image;
            _position = position;
            _size = size;
            _rotation = upsideDown ? -1 : 1;
        }

        public GameTile(Bitmap image, int x, int y, int width, int height, bool upsideDown = false)
        {
            _image = image;
            _position = new Vector2(x, y);
            _size = new Vector2(width, height);
            _rotation = upsideDown ? -1 : 1;
        }
    }
}
