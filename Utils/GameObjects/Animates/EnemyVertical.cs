using Utils.Math;
using Utils.GameLogic;
using System.Timers;
using Utils.GameObjects.Explosives;
using Utils.AbstractFactory;
using Utils.Observer;
using Utils.Map;
using Utils.Decorator;
using Utils.Strategy;

namespace Utils.GameObjects.Animates
{
    public class EnemyVertical : Enemy
    {
        public Vector2 direction { get; private set; }
        public EnemyVertical()
        {
            direction = new Vector2(0, -1);
            movingType = new MoveUpDown();
        }
    }
}
