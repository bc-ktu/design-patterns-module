using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Bomberman_client_graphics_v1
{
    internal class Explosive : GameObject
    {
        private int _range;
        private int _timeTillExplosion;

        public int Range { get { return _range; } }
        public int TimeTillExplosion { get { return _timeTillExplosion; } }

        public Explosive(Vector2 position, Vector2 size, Vector4 collider, Bitmap image, int range) : base(position, size, collider, image)
        {
            _range = range;
            _timeTillExplosion = Settings.InitialTimeTillExplosion;
        }

        public Explosive(int x, int y, int width, int height, int cx, int cy, int cWidth, int cHeight, Bitmap image, int range) 
            : base(x, y, width, height, cx, cy, cWidth, cHeight, image)
        {
            _range = range;
            _timeTillExplosion = Settings.InitialTimeTillExplosion;
        }
    }
}
