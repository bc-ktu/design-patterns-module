using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client_graphics.Interpreter
{
    internal class MoveUpExpression : Expression
    {
        public override string Action()
        {
            return "move up";
        }

        public override Keys Key()
        {
            return Input.KeyUp;
        }

        public override int Limit()
        {
            return int.MaxValue;
        }
    }
    internal class MoveDownExpression : Expression
    {
        public override string Action()
        {
            return "move down";
        }

        public override Keys Key()
        {
            return Input.KeyDown;
        }

        public override int Limit()
        {
            return int.MaxValue;
        }
    }
    internal class MoveRightExpression : Expression
    {
        public override string Action()
        {
            return "move right";
        }

        public override Keys Key()
        {
            return Input.KeyRight;
        }

        public override int Limit()
        {
            return int.MaxValue;
        }
    }
    internal class MoveLeftExpression : Expression
    {
        public override string Action()
        {
            return "move left";
        }

        public override Keys Key()
        {
            return Input.KeyLeft;
        }

        public override int Limit()
        {
            return int.MaxValue;
        }
    }
    internal class PlaceBombExpression : Expression
    {
        public override string Action()
        {
            return "place bomb";
        }

        public override Keys Key()
        {
            return Input.KeyBomb;
        }

        public override int Limit()
        {
            return 1;
        }
    }
}
