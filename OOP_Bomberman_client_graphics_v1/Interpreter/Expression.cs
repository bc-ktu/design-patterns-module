using client_graphics.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client_graphics.Interpreter
{
    internal abstract class Expression
    {
        public bool Interpret(Context context, InputStack stack)
        {
            if (context.Input.Length == 0)
                return false;
            string input = context.Input.ToLower();
            if (input.StartsWith(Action()))
            {
                stack.Push(Key());
                return true;
            }
            return false;
        }
        public abstract string Action();
        public abstract int Limit();
        public abstract Keys Key();
    }
}
