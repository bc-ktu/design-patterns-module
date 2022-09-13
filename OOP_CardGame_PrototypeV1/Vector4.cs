using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_CardGame_PrototypeV1
{
    internal class Vector4
    {
        public int x;
        public int y;
        public int z;
        public int w;

        public Vector4(int x, int y, int z, int w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public static Vector4 operator +(Vector4 left, Vector4 right)
        {
            return new Vector4(left.x + right.x, left.y + right.y, left.z + right.z, left.w + right.w);
        }

        public static Vector4 operator -(Vector4 left, Vector4 right)
        {
            return new Vector4(left.x - right.x, left.y - right.y, left.z - right.z, left.w - right.w);
        }

        public static Vector4 operator *(int k, Vector4 v)
        {
            return new Vector4(k * v.x, k * v.y, k * v.z, k * v.w);
        }

        public static Vector4 operator /(Vector4 v, int k)
        {
            return new Vector4(v.x / k, v.y / k, v.z / k, v.w / k);
        }

        public override string ToString()
        {
            return "(" + x.ToString() + ", " + y.ToString() + ", " + z.ToString() + ", " + w.ToString() + ")";
        }


    }
}
