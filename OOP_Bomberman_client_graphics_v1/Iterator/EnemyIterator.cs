using client_graphics.Composite;
using client_graphics.GameObjects.Animates;
using Utils.Math;

namespace client_graphics.Iterator
{
    internal class EnemyIterator : IAbstractIterator
    {
        Stack<Enemy> _stack = new Stack<Enemy>();
        Vector2 currentPosition;
        Enemy currentEnemy;
        bool isLast = false;
        EnemyType root;

        public EnemyIterator(EnemyType root)
        {
            this.root = root;
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
            currentEnemy = root;
            currentPosition = currentEnemy.WorldPosition;
            if (currentEnemy.ChildrenCount() > 0)
            {
                for (int i = currentEnemy.ChildrenCount() - 1; i >= 0; i--)
                {
                    _stack.Push(currentEnemy.GetChild(i));
                }
            }
            return currentPosition;
        }

        public bool IsDone()
        {
            return _stack.Count == 0 && !isLast;
        }

        public Vector2 Next()
        {
            if (isLast)
            {
                isLast = false;
                return currentPosition;
            }
            currentEnemy = _stack.Pop();
            if (_stack.Count == 0) isLast = true;
            currentPosition = currentEnemy.LocalPosition;
            if (currentEnemy.ChildrenCount() > 0)
            {
                for (int i = currentEnemy.ChildrenCount() - 1; i >= 0; i--)
                {
                    _stack.Push(currentEnemy.GetChild(i));
                }
            }
            return currentPosition;
        }
    }
}
