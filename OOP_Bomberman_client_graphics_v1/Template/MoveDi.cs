using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using client_graphics.GameLogic;
using client_graphics.GameObjects;
using client_graphics.GameObjects.Animates;
using client_graphics.Map;
using Utils.Math;
using client_graphics.Helpers;
using Utils.Helpers;

namespace client_graphics.Template
{
    public sealed class MoveDi : MoveAlgorithm
    {
        public override bool ShouldChangeToDirection1(int collisionCount, Vector2 direction)
        {
            return collisionCount >= 1 && direction == Direction.UpRight;
        }

        public override Vector2 GetDirection1()
        {
            return Direction.DownLeft;
        }

        public override bool ShouldChangeToDirection2(int collisionCount, Vector2 direction)
        {
            return collisionCount >= 1 && direction == Direction.DownLeft;
        }
        
        public override Vector2 GetDirection2()
        {
            return Direction.UpRight;
        }

    }
}
