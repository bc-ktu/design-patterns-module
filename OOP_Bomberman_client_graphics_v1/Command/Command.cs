using client_graphics.SignalR;
using com.sun.tools.corba.se.idl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.GameObjects;
using Utils.GameObjects.Animates;

namespace client_graphics.Command
{
    public abstract class Command
    {
        public abstract void Execute();
        public abstract void Undo();

    }
}
