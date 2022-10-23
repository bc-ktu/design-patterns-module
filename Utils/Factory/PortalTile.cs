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
    public class PortalTile : MapTile
    {
        public PortalTile ExitPoint { get; set; }
        public PortalTile(Vector2 position, Vector2 size, Bitmap image, PortalTile exitPoint) : base(position, size, image)
        {
            ExitPoint = exitPoint;
        }

        public PortalTile(int x, int y, int width, int height, Bitmap image, PortalTile exitPoint) : base(x, y, width, height, image)
        {
            ExitPoint = exitPoint;
        }

        public override void AffectPlayer(Character player)
        {
            return;
        }
    }
}
