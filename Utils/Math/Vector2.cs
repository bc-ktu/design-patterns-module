using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils.Math
{
    public class Vector2 : IEquatable<Vector2>
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Vector2(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Vector2(Point point)
        {
            X = point.X;
            Y = point.Y;
        }

        public Point ToPoint()
        {
            return new Point(X, Y);
        }

        public static Vector2 operator +(Vector2 left, Vector2 right)
        {
            return new Vector2(left.X + right.X, left.Y + right.Y);
        }

        public static Vector2 operator -(Vector2 left, Vector2 right)
        {
            return new Vector2(left.X - right.X, left.Y - right.Y);
        }

        public static Vector2 operator *(int k, Vector2 v)
        {
            return new Vector2(k * v.X, k * v.Y);
        }

        public static Vector2 operator *(double k, Vector2 v)
        {
            return new Vector2((int)(k * v.X), (int)(k * v.Y));
        }

        public static Vector2 operator /(Vector2 v, int k)
        {
            return new Vector2(v.X / k, v.Y / k);
        }

        public static Vector2 operator /(Vector2 v, double k)
        {
            return new Vector2((int)(v.X / k), (int)(v.Y / k));
        }

        public static Vector2 operator /(Vector2 left, Vector2 right)
        {
            return new Vector2(left.X / right.X, left.Y / right.Y);
        }

        public static bool operator ==(Vector2 left, Vector2 right)
        {
            return left.X == right.X && left.Y == right.Y;
        }

        public static bool operator !=(Vector2 left, Vector2 right)
        {
            return left.X != right.X && left.Y != right.Y;
        }

        public bool Equals(Vector2 other)
        {
            return X == other.X && Y == other.Y;
        }

        public int GetHashCode()
        {
            return X + Y;
        }

        public override string ToString()
        {
            return "(" + X.ToString() + ", " + Y.ToString() + ")";
        }
    }
}