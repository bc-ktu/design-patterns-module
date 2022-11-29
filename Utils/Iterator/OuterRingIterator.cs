using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Map;

namespace Utils.Iterator
{
    internal class OuterRingIterator : MapIterator
    {
        int xIndex = 0;
        int yIndex = 0;

        bool first = false;

        public OuterRingIterator(GameMap map) : base(map) { }

        public override Tuple<int, int> CurrentItem()
        {
            return new Tuple<int, int>(xIndex, yIndex);
        }

        public override Tuple<int, int> First()
        {
            first = true;
            return new Tuple<int, int>(0, 0);
        }

        public override bool IsDone()
        {
            if (xIndex == 0 && yIndex == 0 && !first) return true;
            return false;
        }

        public override Tuple<int, int> Next()
        {
            bool done = false;
            first = false;
            if (xIndex < gameMap.Size.X - 1 && yIndex == 0 && !done) { xIndex++; done = true; }
            if (xIndex > 0 && yIndex == gameMap.Size.Y - 1 && !done) { xIndex--; done = true; }
            if (yIndex < gameMap.Size.Y - 1 && xIndex == gameMap.Size.X - 1 && !done) { yIndex++; done = true; }
            if (yIndex > 0 && xIndex == 0 && !done) { yIndex--; done = true; }
            if (IsDone()) return null;
            return new Tuple<int, int>(xIndex, yIndex);
        }
    }
}
