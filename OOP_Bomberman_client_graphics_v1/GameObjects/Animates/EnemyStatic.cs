using client_graphics.Map;
using client_graphics.Strategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Math;

namespace client_graphics.GameObjects.Animates
{
    internal class EnemyStatic : Enemy
    {
        public EnemyStatic(Vector2 position, int speed, Vector2 size, Vector4 collider, Bitmap image) : base(position, size, collider, image, speed)
        {
            Initialize(speed);
        }

        public EnemyStatic()
        {

        }

        public EnemyStatic(Enemy e) : base(e)
        {
        }

        public override void Action(GameMap gameMap)
        {
            return;
        }

        public override void Add(Enemy e)
        {
            return;
        }

        public override GameObject Clone()
        {
            return new EnemyStatic(this);
        }

        public override void Remove(Enemy e)
        {
            return;
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
