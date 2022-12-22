using client_graphics.Composite;
using client_graphics.GameObjects.Animates;
using client_graphics.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Math;

namespace client_graphics.Iterator
{
    internal class EnemyIterator : IAbstractIterator
    {
        Stack<Enemy> _stack = new Stack<Enemy>();
        Vector2 currentPosition;
        Enemy currentEnemy;

        public EnemyIterator(EnemyType root)
        {
            _stack.Push(root);
        }

        public Vector2 CurrentItem()
        {
            return currentPosition;
        }

        public Enemy CurrentEnemy()
        {
            return currentEnemy;
        }

        public Vector2 First()
        {
            currentEnemy = _stack.Pop();
            currentPosition = currentEnemy.WorldPosition;
            return currentPosition;
        }

        public bool IsDone()
        {
            return _stack.Count == 0;
        }

        public Vector2 Next()
        {
            throw new NotImplementedException();
        }
    }
}
