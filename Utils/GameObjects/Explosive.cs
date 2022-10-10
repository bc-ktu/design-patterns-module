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

        public Explosive(Vector2 position, Vector2 size, Vector4 collider, Bitmap image) : base(position, size, collider, image)
        {
            _timeTillExplosion = Settings.InitialTimeTillExplosion;
        }

        public Explosive(int x, int y, int width, int height, int cx, int cy, int cWidth, int cHeight, Bitmap image)
            : base(x, y, width, height, cx, cy, cWidth, cHeight, image)
        {
            _timeTillExplosion = Settings.InitialTimeTillExplosion;
        }

        protected void SetExplosionDirections(Vector2[] explosionDirections)
        {
            _explosionDirections = explosionDirections;
        }

        public void SetRange(int range)
        {
            _range = range;
        }

        public void Explode(Map gameMap, int indexInLookupTable)
        {
            Vector2 thisIndex = WorldPosition / gameMap.TileSize;

            for (int i = 0; i < ExplosionDirections.Length; i++)
            {
                Vector2 index = thisIndex + ExplosionDirections[i];
                GameObject gameObject = gameMap.Tiles[index.X, index.Y].GameObject;
                int range = 1;
                while ((gameObject is EmptyGameObject || gameObject is Fire) && range < _range)
                {
                    if (gameObject is EmptyGameObject)
                    {
                        var prm = gameMap.CreateScaledGameObjectParameters(index.X, index.Y, _image);
                        gameMap.Tiles[index.X, index.Y].GameObject = new Fire(prm.Item1, prm.Item2, prm.Item3, prm.Item4);
                    }
                    index += ExplosionDirections[i];
                    gameObject = gameMap.Tiles[index.X, index.Y].GameObject;
                    range++;
                }
            }

            gameMap.ExplosivesLookupTable.Remove(indexInLookupTable);
        }

    }
}
