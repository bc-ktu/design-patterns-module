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
    }
}
