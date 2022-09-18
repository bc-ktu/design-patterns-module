﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Bomberman_client_graphics_v1
{
    internal class Wall : GameObject
    {
        public Wall(Vector2 position, Vector2 size, Vector4 collider, Bitmap image) : base(position, size, collider, image)
        {

        }

        public Wall(int x, int y, int width, int height, int cx, int cy, int cWidth, int cHeight, Bitmap image) 
            : base(x, y, width, height, cx, cy, cWidth, cHeight, image)
        {

        }
    }
}