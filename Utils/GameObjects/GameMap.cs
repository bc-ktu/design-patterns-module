using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Helpers;
using Utils.Math;

namespace Utils.GameObjects
{
    public class GameMap
    {
        private Vector2 _size;
        private Vector2 _viewSize;
        
        private MapTile[,] _tiles;
        private Vector2 _tileSize;

        private LookupTable _explosivesLookupTable;

        public Vector2 Size { get { return _size; } }
        public MapTile[,] Tiles { get { return _tiles; } }
        public Vector2 TileSize { get { return _tileSize; } }
        
        public LookupTable ExplosivesLookupTable { get { return _explosivesLookupTable; } }
        public LookupTable FireLookupTable { get; private set; }

        public GameMap(Vector2 mapSize, Vector2 viewSize)
        {
            _size = mapSize;
            _viewSize = viewSize;
            _tiles = new MapTile[mapSize.X, mapSize.Y];
            _tileSize = new Vector2(viewSize.X / mapSize.X, viewSize.Y / mapSize.Y);
            _explosivesLookupTable = new LookupTable();
            FireLookupTable = new LookupTable();
        }

        public void SetTile(int x, int y, Bitmap image)
        {
            int xWorld = _tileSize.X * x;
            int yWorld = _tileSize.Y * y;
            _tiles[x, y] = new MapTile(xWorld, yWorld, _tileSize.X, _tileSize.Y, image);
        }

        /// <param name="x">Index on x axis of the tile to place the new GameObject </param>
        /// <param name="y">Index on y axis of the tile to place the new GameObject </param>
        public Tuple<Vector2, Vector2, Vector4, Bitmap> CreateScaledGameObjectParameters(int x, int y, Bitmap image)
        {
            double heightToWidthRatio = image.Height / (double)image.Width;
            Vector2 position = new Vector2(Tiles[x, y].LocalPosition.X, Tiles[x, y].LocalPosition.Y - ((int)(heightToWidthRatio * TileSize.Y) - TileSize.Y));
            Vector2 size = new Vector2(TileSize.X, (int)(heightToWidthRatio * TileSize.Y));
            Vector4 collider = new Vector4(position.X, Tiles[x, y].LocalPosition.Y, position.X + TileSize.X, Tiles[x, y].LocalPosition.Y + TileSize.Y);
            return new Tuple<Vector2, Vector2, Vector4, Bitmap>(position, size, collider, image);
        }
    }
}
