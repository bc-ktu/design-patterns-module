using Utils.Math;
using client_graphics.GameLogic;
using System.Timers;
using client_graphics.GameObjects.Explosives;
using client_graphics.AbstractFactory;
using Utils.Observer;
using client_graphics.Map;
using Utils.Decorator;
using client_graphics.Template;
using Utils.Helpers;

namespace client_graphics.GameObjects.Animates
{
    public class EnemyUD : Enemy
    {
        public EnemyUD()
        {
            Facing = Direction.Up;
            movingType = new MoveUD();
        }

        public EnemyUD(Vector2 position, Vector2 size, Vector4 collider, Bitmap image, int speed) : base(position, size, collider, image, speed)
        {
            Facing = Direction.Up;
            movingType = new MoveUD();
        }

        public EnemyUD(EnemyUD p) : base(p)
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
            return new EnemyUD(this);
        }

        public override void Action()
        {
            throw new NotImplementedException();
        }
    }
}
