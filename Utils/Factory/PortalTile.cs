using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.GameObjects.Animates;
using Utils.Map;
using Utils.Math;

namespace Utils.Factory
{
    public class PortalTile : MapTile
    {
        public MapTile ExitTile { get; set; }

        public PortalTile(Vector2 position, Vector2 size, Bitmap image) : base(position, size, image)
        {
            ExitTile = new EmptyTile();
        }

        public PortalTile(int x, int y, int width, int height, Bitmap image) : base(x, y, width, height, image)
        {
            ExitTile = new EmptyTile();
        }

        public override void AffectPlayer(Player player)
        {
            if (ExitTile is EmptyTile)
                return;

            player.Teleport(ExitTile.LocalPosition);
        }
    }
}
