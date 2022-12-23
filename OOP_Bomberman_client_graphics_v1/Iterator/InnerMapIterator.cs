using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using client_graphics.Map;
using Utils.Math;

namespace client_graphics.Iterator
{
    internal class InnerMapIterator : MapIterator
    {
        int xIndex = 1;
        int yIndex = 1;

        public InnerMapIterator(GameMap map) : base(map) { }

        public override Vector2 CurrentItem()
        {
            return new Vector2(xIndex, yIndex);
        }

        public override Vector2 First()
        {
            return new Vector2(1, 1);
        }

        public override bool IsDone()
        {
            if (xIndex == gameMap.Size.X - 2 && yIndex == gameMap.Size.Y - 1) return true;
            return false;
        }

        public override Vector2 Next()
        {
            bool done = false;
            if (xIndex < gameMap.Size.X - 2) { xIndex++; done = true; }
            if (yIndex <= gameMap.Size.Y - 2 && xIndex == gameMap.Size.X - 2 && !done)
            {
                yIndex++;
                xIndex = 1;
            }
            if (IsDone()) return null;
            return new Vector2(xIndex, yIndex);
        }
    }
}
