using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Utils.Prototype;

namespace Utils.Math
{
    public class Vector4 : IEquatable<Vector4>, ICloneable<Vector4>
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public int W { get; set; }

        public Vector4(int x, int y, int z, int w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public static Vector4 operator +(Vector4 left, Vector4 right)
        {
            return new Vector4(left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W);
        }

        public static Vector4 operator -(Vector4 left, Vector4 right)
        {
            return new Vector4(left.X - right.X, left.Y - right.Y, left.Z - right.Z, left.W - right.W);
        }

        public static Vector4 operator *(int k, Vector4 v)
        {
            return new Vector4(k * v.X, k * v.Y, k * v.Z, k * v.W);
        }

        public static Vector4 operator /(Vector4 v, int k)
        {
            return new Vector4(v.X / k, v.Y / k, v.Z / k, v.W / k);
        }

        public static bool operator ==(Vector4 left, Vector4 right)
        {
            return left.X == right.X && left.Y == right.Y && left.Z == right.Z && left.W == right.W;
        }

        public static bool operator !=(Vector4 left, Vector4 right)
        {
            return left.X != right.X && left.Y != right.Y && left.Z != right.Z && left.W != right.W;
        }

        public override string ToString()
        {
            return "(" + X.ToString() + ", " + Y.ToString() + ", " + Z.ToString() + ", " + W.ToString() + ")";
        }

        public bool Equals(Vector4 other)
        {
            return X == other.X && Y == other.Y && Z == other.Z && W == other.W;
        }

        public int GetHashCode()
        {
            return X + Y + Z + W;
        }

        private Vector4(Vector4 v)
        {
            X = v.X;
            Y = v.Y;
            Z = v.Z;
            W = v.W;
        }

        public Vector4 Clone()
        {
            return new Vector4(this);
        }
    }
}
