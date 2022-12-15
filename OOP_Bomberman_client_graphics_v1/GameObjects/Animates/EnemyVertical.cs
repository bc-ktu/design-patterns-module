using Utils.Math;
using client_graphics.GameLogic;
using System.Timers;
using client_graphics.GameObjects.Explosives;
using client_graphics.AbstractFactory;
using Utils.Observer;
using client_graphics.Map;
using Utils.Decorator;
using client_graphics.Strategy;

namespace client_graphics.GameObjects.Animates
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
