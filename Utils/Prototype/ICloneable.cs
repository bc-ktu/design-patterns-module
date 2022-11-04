using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Utils.GameObjects;

namespace Utils.Prototype
{
    public interface ICloneable<T> where T : class
    {
        public abstract T Clone();
    }
}
