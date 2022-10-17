﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Utils.GameObjects;
using Utils.GameObjects.Destructables.Walls;
using Utils.GameObjects.Explosives;
using Utils.GameObjects.Interactables;
using Utils.Math;

namespace Utils.AbstractFactory
{
    public class Level2Factory : ILevelFactory
    {
        public Level2Factory()
        {

        }

        public Explosive CreateExplosive(Vector2 position, Vector2 size, Vector4 collider, Bitmap image, Bitmap fireImage)
        {
            return new ExplosiveDi(position, size, collider, image, fireImage);
        }

        public Explosive CreateExplosive(int x, int y, int width, int height, int cx, int cy, int cWidth, int cHeight, Bitmap image, Bitmap fireImage)
        {
            return new ExplosiveDi(x, y, width, height, cx, cy, cWidth, cHeight, image, fireImage);
        }

        public Powerup CreatePowerup(Vector2 position, Vector2 size, Vector4 collider, Bitmap image)
        {
            return new CapacityPowerup(position, size, collider, image);
        }

        public Powerup CreatePowerup(int x, int y, int width, int height, int cx, int cy, int cWidth, int cHeight, Bitmap image)
        {
            return new CapacityPowerup(x, y, width, height, cx, cy, cWidth, cHeight, image);
        }

        public DestructableWall CreateWall(Vector2 position, Vector2 size, Vector4 collider, Bitmap image)
        {
            return new WoodenWall(position, size, collider, image);
        }

        public DestructableWall CreateWall(int x, int y, int width, int height, int cx, int cy, int cWidth, int cHeight, Bitmap image)
        {
            return new WoodenWall(x, y, width, height, cx, cy, cWidth, cHeight, image);
        }
    }
}
