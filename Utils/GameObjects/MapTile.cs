﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Utils.Math;

namespace Utils.GameObjects
{
    public class MapTile
    {
        private Vector2 _position;
        private Vector2 _size;
        private Bitmap _image;

        public Vector2 LocalPosition { get { return _position; } }
        public Vector2 WorldPosition { get { return _position + _size / 2; } }
        public Vector2 Size { get { return _size; } }
        public Bitmap Image { get { return _image; } }

        public GameObject GameObject { get; set; }

        public MapTile(Vector2 position, Vector2 size, Bitmap image)
        {
            _position = position;
            _size = size;
            _image = image;
            GameObject = new EmptyGameObject();
        }

        public MapTile(int x, int y, int width, int height, Bitmap image)
        {
            _position = new Vector2(x, y);
            _size = new Vector2(width, height);
            _image = image;
            GameObject = new EmptyGameObject();
        }

        public Rectangle ToRectangle()
        {
            return new Rectangle(_position.X, _position.Y, _size.X, _size.Y);
        }
    }
}
