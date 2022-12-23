using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Math;
using client_graphics.Template;
using client_graphics.Map;
using Utils.Helpers;

namespace client_graphics.GameObjects.Animates
{
    public class EnemyLR : Enemy
    {
        public EnemyLR()
        {
            Facing = Direction.Left;
            movingType = new MoveLR();
        }

        public EnemyLR(Vector2 position, Vector2 size, Vector4 collider, Bitmap image, int speed) : base(position, size, collider, image, speed)
        {
            Facing = Direction.Left;
            movingType = new MoveLR();
            Initialize(speed);
        }

        public EnemyLR(Enemy e) : base(e)
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
            return new EnemyLR(this);
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
