using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Map;

namespace Utils.Iterator
{
    internal interface IAbstractIterator
    {
        Tuple<int, int> First();
        Tuple<int, int> Next();
        bool IsDone();
        Tuple<int, int> CurrentItem();
    }
}
