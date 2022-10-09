using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils.GameObjects
{
    internal class PaperWall : DestructableWall
    {
        public PaperWall()
        {
            SetDurability(3);
        }

    }
}
