using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using client_graphics.GameLogic;
using Utils.Math;
using client_graphics.Template;
using client_graphics.Map;
using Utils.Helpers;

namespace client_graphics.GameObjects.Animates
{
    public class EnemyDi : Enemy
    {
        public EnemyDi()
        {
            Facing = Direction.UpRight;
            movingType = new MoveDi();
        }
        public EnemyDi(Vector2 position, Vector2 size, Vector4 collider, Bitmap image, int speed) : base(position, size, collider, image, speed)
        {
            Facing = Direction.UpRight;
            movingType = new MoveDi();
            Initialize(speed);
        }

        public EnemyDi(Enemy e) : base(e)
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
            return new EnemyDi(this);
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
