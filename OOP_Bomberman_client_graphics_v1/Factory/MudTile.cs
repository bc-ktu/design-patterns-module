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
    public class MudTile : MapTile
    {
        private readonly int speed = -7;

        public MudTile(Vector2 position, Vector2 size, Bitmap image) : base(position, size, image)
        {
        }

        public MudTile(int x, int y, int width, int height, Bitmap image) : base(x, y, width, height, image)
        {
        }

        public override void AffectPlayer(Player player)
        {
            player.SpeedModifier = speed;
        }
    }
}
