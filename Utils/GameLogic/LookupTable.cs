using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Utils.GameObjects;
using Utils.Math;

namespace Utils.GameLogic
{
    public class LookupTable
    {
        private List<Vector2> _positions; // in map, a.k.a index

        public List<GameObject> GameObjects { get; private set; }
        public int Count { get; private set; }

        public LookupTable()
        {
            _positions = new List<Vector2>();
            GameObjects = new List<GameObject>();
        }

        public void Set(Vector2 position, GameObject gameObject)
        {
            int index = _positions.IndexOf(position);
            if (index != -1)
            {
                _positions.RemoveAt(index);
                GameObjects.RemoveAt(index);
                Count--;
            }

            _positions.Add(position);
            GameObjects.Add(gameObject);
            Count++;
        }

        public void Remove(Vector2 position)
        {
            int index = _positions.IndexOf(position);
            _positions.RemoveAt(index);
            GameObjects.RemoveAt(index);
            Count--;
        }

    }
}
