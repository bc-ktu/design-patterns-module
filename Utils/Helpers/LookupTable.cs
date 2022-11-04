using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Utils.GameObjects;
using Utils.Math;

namespace Utils.Helpers
{
    public class LookupTable
    {
        private Dictionary<Vector2, List<GameObject>> _table; // key - MapIndex, value - GameObjects

        public Vector2[] Positions { get { return _table.Keys.ToArray(); } }
        public GameObject[] GameObjects
        {
            get
            {
                List<GameObject> values = new List<GameObject>();

                foreach (Vector2 key in _table.Keys)
                    values.AddRange(_table[key]);

                return values.ToArray();
            }
        }
        public int Count { get { return GameObjects.Length; } }

        public LookupTable()
        {
            _table = new Dictionary<Vector2, List<GameObject>>();
        }

        public List<GameObject> Get(Vector2 position)
        {
            return _table[position];
        }

        public List<GameObject> Get<T>() where T : GameObject
        {
            List<GameObject> gameObjects = new List<GameObject>();

            foreach (Vector2 key in _table.Keys)
            {
                foreach (GameObject go in _table[key])
                {
                    if (go is T)
                        gameObjects.Add(go);
                }
            }

            return gameObjects;
        }

        public void Add(Vector2 position, GameObject gameObject)
        {
            if (!_table.ContainsKey(position))
                _table.Add(position, new List<GameObject>());

            _table[position].Add(gameObject);
        }

        public void Remove(Vector2 position, GameObject gameObject)
        {
            _table[position].Remove(gameObject);
        }

        public bool Contains(Vector2 position, GameObject gameObject)
        {
            return _table[position].Contains(gameObject);
        }

        public void Clear(Vector2 position)
        {
            _table[position].Clear();
        }

        public bool Has<T>() where T : GameObject
        {
            foreach (Vector2 key in _table.Keys)
            {
                foreach (GameObject go in _table[key])
                {
                    if (go is T)
                        return true;
                }
            }

            return false;
        }

    }
}
