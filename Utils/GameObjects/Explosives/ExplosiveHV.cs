﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Utils.Math;
using Utils.Helpers;

namespace Utils.GameObjects.Explosives
{
    internal class ExplosiveHV : Explosive
    {
        public ExplosiveHV(Vector2 position, Vector2 size, Vector4 collider, Bitmap image, Bitmap fireImage) 
            : base(position, size, collider, image, fireImage)
        {
            Initialize();
        }

        public ExplosiveHV(int x, int y, int width, int height, int cx, int cy, int cWidth, int cHeight, Bitmap image, Bitmap fireImage)
            : base(x, y, width, height, cx, cy, cWidth, cHeight, image, fireImage)
        {
            Initialize();
        }

        private void Initialize()
        {
            ExplosionDirections = new Vector2[]{
                Direction.Up,
                Direction.Right,
                Direction.Down,
                Direction.Left
            };
            Range = 1;
        }

    }
}