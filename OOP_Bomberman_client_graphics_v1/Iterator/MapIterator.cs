﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using client_graphics.Map;

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

        public abstract Tuple<int, int> CurrentItem();

        public abstract Tuple<int, int> First();

        public abstract Tuple<int, int> Next();
    }
}
