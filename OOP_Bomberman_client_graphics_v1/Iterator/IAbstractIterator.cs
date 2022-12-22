using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using client_graphics.Map;
using Utils.Math;

namespace client_graphics.Iterator
{
    internal interface IAbstractIterator
    {
        Vector2 First();
        Vector2 Next();
        bool IsDone();
        Vector2 CurrentItem();
    }
}
