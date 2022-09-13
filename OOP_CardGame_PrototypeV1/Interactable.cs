using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_CardGame_PrototypeV1
{
    internal abstract class Interactable : IDrawable
    {
        protected Vector2 _size;
        protected int _rotation; // -1 upside down, 1 normal

        protected Vector2 _position; // top left corner position

        /// <summary>
        /// Size of the card in local space: x and y is axes depends on card's rotaion 
        /// </summary>
        public Vector2 LocalSize { get { return _rotation * _size; } }
        /// <summary>
        /// Size of the card in world space: x is always positive right, y is always positive down
        /// </summary>
        public Vector2 WorldSpaceSize { get { return _size; } }

        /// <summary>
        /// Position in world space: card's origin is at top left corner
        /// </summary>
        public Vector2 WorldSpacePosition { get { return _position; } }
        /// <summary>
        /// Position in world space: card's origin is at it's geometrical center
        /// </summary>
        public Vector2 CenteredPosition { get { return _position - _rotation * (_size / 2); } }

        public Vector4 Collider
        {
            get
            {
                int tlx = _position.x - _size.x / 2;
                int tly = _position.y - _size.y / 2;
                int brx = tlx + _size.x;
                int bry = tly + _size.y;
                return new Vector4(tlx, tly, brx, bry);
            }
        }

        public void SetPosition(int x, int y)
        {
            _position = new Vector2(x, y);
        }

        public void SetPosition(Vector2 newPosition)
        {
            _position = newPosition;
        }

        public void SetSize(int x, int y)
        {
            _size = new Vector2(x, y);
        }

        public void SetSize(Vector2 newSize)
        {
            _size = newSize;
        }
        public Rectangle ToRectangle()
        {
            return new Rectangle(CenteredPosition.x, CenteredPosition.y, LocalSize.x, LocalSize.y);
        }
    }
}
