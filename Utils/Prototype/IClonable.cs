using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Utils.GameObjects;

namespace Utils.Prototype
{
    public interface IClonable
    {
        public abstract GameObject Clone();
    }
}
