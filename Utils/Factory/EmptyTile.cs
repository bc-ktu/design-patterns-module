using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Utils.GameObjects;
using Utils.GameObjects.Animates;
using Utils.Math;

namespace Utils.Factory
{
    public class EmptyTile : MapTile
    {
        public EmptyTile() : base(null, null, null)
        {

        }

        public override void AffectPlayer(Character player)
        {
            throw new NotImplementedException();
        }
    }
}
