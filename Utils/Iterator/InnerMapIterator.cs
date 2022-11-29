using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Map;

namespace Utils.Iterator
{
    internal class InnerMapIterator : MapIterator
    {
        int xIndex = 1;
        int yIndex = 1;

        public InnerMapIterator(GameMap map) : base(map) { }

        public override Tuple<int, int> CurrentItem()
        {
            return new Tuple<int, int>(xIndex, yIndex);
        }

        public override Tuple<int, int> First()
        {
            return new Tuple<int, int>(1, 1);
        }

        public override bool IsDone()
        {
            if (xIndex == gameMap.Size.X - 2 && yIndex == gameMap.Size.Y - 2) return true;
            return false;
        }

        public override Tuple<int, int> Next()
        {
            if (xIndex < gameMap.Size.X - 2) xIndex++;
            if (yIndex < gameMap.Size.Y - 2 && xIndex == gameMap.Size.X - 2)
            {
                yIndex++;
                xIndex = 1;
            }
            if (IsDone()) return null;
            return new Tuple<int, int>(xIndex, yIndex);
        }
    }
}
