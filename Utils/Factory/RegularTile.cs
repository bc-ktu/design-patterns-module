using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Utils.GameLogic;
using Utils.GameObjects.Animates;
using Utils.Map;
using Utils.Math;

namespace Utils.Factory
{
    internal class RegularTile : MapTile
    {
        public RegularTile(Vector2 localPosition, Vector2 size, Bitmap image) : base(localPosition, size, image)
        {

        }

        public RegularTile(int x, int y, int width, int height, Bitmap image) : base(x, y, width, height, image)
        {

        }

        public override void AffectPlayer(Player player)
        {
            player.SpeedModifier = 0;
        }
    }
}
