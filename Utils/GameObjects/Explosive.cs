using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Utils.Math;
using Utils.GameLogic;

namespace Utils.GameObjects
{
    public abstract class Explosive : GameObject
    {
        private int _range;
        private int _timeTillExplosion;
        private Vector2[] _explosionDirections = new Vector2[0];

        public int Range { get { return _range; } }
        public int TimeTillExplosion { get { return _timeTillExplosion; } }
        public Vector2[] ExplosionDirections { get { return _explosionDirections; } }

        public Explosive()
        {

        }

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

        protected void SetExplosionDirections(Vector2[] explosionDirections)
        {
            _explosionDirections = explosionDirections;
        }
    }
}
