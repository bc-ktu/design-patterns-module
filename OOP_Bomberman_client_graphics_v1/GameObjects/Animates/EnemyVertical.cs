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
        public EnemyVertical()
        {
            direction = new Vector2(0, -1);
            movingType = new MoveUpDown();
        }

        public EnemyVertical(Vector2 position, int speed, Vector2 size, Vector4 collider, Bitmap image) : base(position, size, collider, image)
        {
            MovementSpeed = speed;
            direction = new Vector2(0, -1);
            movingType = new MoveUpDown();
            Initialize();
        }

        public EnemyVertical(EnemyVertical p) : base(p)
        {
        }

        public override void Add(Enemy d)
        {
            return;
        }

        public override void Remove(Enemy d)
        {
            return;
        }

        public override GameObject Clone()
        {
            return new EnemyVertical(this);
        }

        public override void Action()
        {
            throw new NotImplementedException();
        }
    }
}
