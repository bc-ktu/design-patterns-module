using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Math;
using client_graphics.Strategy;

namespace client_graphics.GameObjects.Animates
{
    public class EnemyHorizontal : Enemy
    {
        public EnemyHorizontal()
        {
            direction = new Vector2(1, -1);
            movingType = new MoveDi();
        }

        public EnemyHorizontal(Vector2 position, Vector2 size, Vector4 collider, Bitmap image) : base(position, size, collider, image)
        {
            direction = new Vector2(0, -1);
            movingType = new MoveUpDown();
            Initialize();
        }

        public EnemyHorizontal(Enemy p) : base(p)
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
            return new EnemyHorizontal(this);
        }

        public override void Action()
        {
            throw new NotImplementedException();
        }
    }
}
