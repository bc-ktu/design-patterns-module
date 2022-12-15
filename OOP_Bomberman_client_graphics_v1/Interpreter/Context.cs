using client_graphics.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using client_graphics.AbstractFactory;
using client_graphics.GameObjects.Animates;
using Utils.Helpers;
using client_graphics.Map;
using Utils.Math;

namespace client_graphics.Interpreter
{
    internal class Context
    {
        string _input;

        public Context(string input)
        {
            _input = input;
        }
        public string Input
        {
            get { return _input; }
            set { _input = value; }
        }
    }
}
