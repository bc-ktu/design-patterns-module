using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.GameObjects;

namespace Utils.Observer
{
    public interface IObserver
    {
        void Update(string sound);
    }
}
