using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils.Builder
{
    public class Director
    {
        public void Construct(MapBuilder builder)
        {
            builder.AddCrates();
            builder.AddOuterRing();
            builder.AddSpecialTiles();
        }
    }
}
