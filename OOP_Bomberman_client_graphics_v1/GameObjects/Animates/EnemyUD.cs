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
            Initialize(speed);
        }

        public EnemyUD(EnemyUD e) : base(e)
        {
        }

        public override void Add(Enemy e)
        {
            return;
        }

        public override void Remove(Enemy e)
        {
            return;
        }

        public override GameObject Clone()
        {
            return new EnemyUD(this);
        }

        public override void Action(GameMap gameMap)
        {
            Move(gameMap);
        }

        public override Enemy GetChild(int i)
        {
            return null;
        }

        public override int ChildrenCount()
        {
            return 0;
        }
    }
}
