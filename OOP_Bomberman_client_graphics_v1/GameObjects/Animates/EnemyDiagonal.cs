using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using client_graphics.GameLogic;
using Utils.Math;
using client_graphics.Strategy;

namespace client_graphics.GameObjects.Animates
{
    public class EnemyDiagonal : Enemy
    {
        public EnemyDiagonal()
        {
            direction = new Vector2(-1, 0);
            movingType = new MoveLeftRigh();
        }

        public EnemyDiagonal(Vector2 position, Vector2 size, Vector4 collider, Bitmap image) : base(position, size, collider, image)
        {
            direction = new Vector2(-1, 0);
            movingType = new MoveLeftRigh();
            Initialize();
        }

        public EnemyDiagonal(Enemy p) : base(p)
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
            return new EnemyDiagonal(this);
        }

        public override void Action()
        {
            throw new NotImplementedException();
        }
    }
}
