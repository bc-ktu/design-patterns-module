using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace OOP_CardGame_PrototypeV1
{
    internal class Vector2
    {
        public int x;
        public int y;

        public Vector2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public Vector2(Point point)
        {
            x = point.X;
            y = point.Y;
        }

        public Point ToPoint()
        {
            return new Point(x, y);
        }

        public static Vector2 operator +(Vector2 left, Vector2 right)
        {
            return new Vector2(left.x + right.x, left.y + right.y);
        }

        public static Vector2 operator -(Vector2 left, Vector2 right)
        {
            return new Vector2(left.x - right.x, left.y - right.y);
        }

        public static Vector2 operator * (int k, Vector2 v)
        {
            return new Vector2(k * v.x, k * v.y);
        }

        public static Vector2 operator *(double k, Vector2 v)
        {
            return new Vector2((int)(k * v.x), (int)(k * v.y));
        }

        public static Vector2 operator /(Vector2 v, int k)
        {
            return new Vector2(v.x / k, v.y / k);
        }

        public static Vector2 operator /(Vector2 v, double k)
        {
            return new Vector2((int)(v.x / k), (int)(v.y / k));
        }

        public override string ToString()
        {
            return "(" + x.ToString() + ", " + y.ToString() + ")";
        }

    }
}
