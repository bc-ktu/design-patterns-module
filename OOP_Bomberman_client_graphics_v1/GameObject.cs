﻿using OOP_CardGame_PrototypeV1;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Bomberman_client_graphics_v1
{
    internal class GameObject : INullable
    {
        private Vector2 _position;
        private Vector2 _size;
        private Vector4 _collider;
        
        private Bitmap _image;

        public Vector2 LocalPosition { get { return _position; } }
        public Vector2 WorldPosition { get { return _position + _size / 2; } }
        public Vector4 Colider { get { return _collider; } }

        public Bitmap Image { get { return _image; } }

        // How to implement? Is it neccesary
        public bool IsNull => throw new NotImplementedException();

        public GameObject(Vector2 position, Vector2 size, Vector4 collider, Bitmap image)
        {
            _position = position;
            _size = size;
            _collider = collider;
            _image = image;
        }

        /// <param name="x">Top left x coordinate corner of the sprite</param>
        /// <param name="y">Top left y coordinate corner of the sprite</param>
        /// <param name="cx">Top left x coordinate corner of the collider</param>
        /// <param name="cy">Top left y coordinate corner of the collider</param>
        /// <param name="cWidth">Width of the collider</param>
        /// <param name="cHeight">Height of the collider</param>
        public GameObject(int x, int y, int width, int height, int cx, int cy, int cWidth, int cHeight, Bitmap image)
        {
            _position = new Vector2(x, y);
            _size = new Vector2(width, height);
            _collider = new Vector4(cx, cy, cx + cWidth, cy + cHeight);
            _image = image;
        }

        public Rectangle ToRectangle()
        {
            return new Rectangle(_position.X, _position.Y, _size.X, _size.Y);
        }

    }
}
