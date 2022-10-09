using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils.Math
{
    public class Vector2
    {
        private int _x;
        private int _y;

        public int X { get { return _x; } }
        public int Y { get { return _y; } }

        public Vector2(int x, int y)
        {
            _x = x;
            _y = y;
        }

        public Vector2(Point point)
        {
            _x = point.X;
            _y = point.Y;
        }

        public Point ToPoint()
        {
            return new Point(_x, _y);
        }

        public void SetX(int X)
        {
            _x = X;
        }
        public void SetY(int Y)
        {
            _y = Y;
        }
        public static Vector2 operator +(Vector2 left, Vector2 right)
        {
            return new Vector2(left._x + right._x, left._y + right._y);
        }

        public static Vector2 operator -(Vector2 left, Vector2 right)
        {
            return new Vector2(left._x - right._x, left._y - right._y);
        }

        public static Vector2 operator *(int k, Vector2 v)
        {
            return new Vector2(k * v._x, k * v._y);
        }

        public static Vector2 operator *(double k, Vector2 v)
        {
            return new Vector2((int)(k * v._x), (int)(k * v._y));
        }

        public static Vector2 operator /(Vector2 v, int k)
        {
            return new Vector2(v._x / k, v._y / k);
        }

        public static Vector2 operator /(Vector2 v, double k)
        {
            return new Vector2((int)(v._x / k), (int)(v._y / k));
        }

        public static Vector2 operator /(Vector2 left, Vector2 right)
        {
            return new Vector2(left._x / right._x, left._y / right._y);
        }

        public override string ToString()
        {
            return "(" + _x.ToString() + ", " + _y.ToString() + ")";
        }

    }
}