using client_graphics.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client_graphics.Visitor
{
    public interface IVisitor
    {
        public abstract void Visit(RegularTile tile);
        public abstract void Visit(PortalTile tile);
        public abstract void Visit(EmptyTile tile);
        public abstract void Visit(IceTile tile);
        public abstract void Visit(MudTile tile);
    }
}
