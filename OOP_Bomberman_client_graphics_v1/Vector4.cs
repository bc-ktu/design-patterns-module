using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Bomberman_client_graphics_v1
{
    internal class Vector4
    {
        private int _x;
        private int _y;
        private int _z;
        private int _w;

        public int X { get { return _x; } }
        public int Y { get { return _y; } }
        public int Z { get { return _z; } }
        public int W { get { return _w; } }

        public Vector4(int x, int y, int z, int w)
        {
            _x = x;
            _y = y;
            _z = z;
            _w = w;
        }

        public static Vector4 operator +(Vector4 left, Vector4 right)
        {
            return new Vector4(left._x + right._x, left._y + right._y, left._z + right._z, left._w + right._w);
        }

        public static Vector4 operator -(Vector4 left, Vector4 right)
        {
            return new Vector4(left._x - right._x, left._y - right._y, left._z - right._z, left._w - right._w);
        }

        public static Vector4 operator *(int k, Vector4 v)
        {
            return new Vector4(k * v._x, k * v._y, k * v._z, k * v._w);
        }

        public static Vector4 operator /(Vector4 v, int k)
        {
            return new Vector4(v._x / k, v._y / k, v._z / k, v._w / k);
        }

        public override string ToString()
        {
            return "(" + _x.ToString() + ", " + _y.ToString() + ", " + _z.ToString() + ", " + _w.ToString() + ")";
        }


    }
}
