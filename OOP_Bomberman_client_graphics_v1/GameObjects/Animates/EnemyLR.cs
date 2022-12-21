using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Math;
using client_graphics.Template;
using Utils.Helpers;

namespace client_graphics.GameObjects.Animates
{
    public class EnemyLR : Enemy
    {
        public EnemyLR()
        {
            Facing = Direction.Left;
            movingType = new MoveDi();
        }

        public EnemyLR(Vector2 position, Vector2 size, Vector4 collider, Bitmap image, int speed) : base(position, size, collider, image, speed)
        {
            Facing = Direction.Left;
            movingType = new MoveLR();
        }

        public EnemyLR(Enemy p) : base(p)
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
            return new EnemyLR(this);
        }

        public override void Action()
        {
            throw new NotImplementedException();
        }
    }
}
