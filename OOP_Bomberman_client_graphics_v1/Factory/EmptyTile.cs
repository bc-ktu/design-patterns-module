using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using client_graphics.GameObjects.Animates;
using client_graphics.Map;
using Utils.Math;

namespace client_graphics.Factory
{
    public class EmptyTile : MapTile
    {
        public EmptyTile() : base(null, null, null)
        {

        }

        public override void AffectPlayer(Player player)
        {
            return;
        }
    }
}
