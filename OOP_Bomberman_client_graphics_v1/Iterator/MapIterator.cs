using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using client_graphics.Map;
using Utils.Math;

namespace client_graphics.Iterator
{
    internal abstract class MapIterator : IAbstractIterator
    {
        protected GameMap gameMap;

        public MapIterator(GameMap map)
        {
            this.gameMap = map;
        }

        public abstract bool IsDone();

        public abstract Vector2 CurrentItem();

        public abstract Vector2 First();

        public abstract Vector2 Next();
    }
}
