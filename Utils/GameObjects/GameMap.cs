using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Utils.Helpers;
using Utils.Math;

namespace Utils.GameObjects
{
    public class GameMap
    {
        public Vector2 Size { get; private set; }
        public Vector2 ViewSize { get; private set; }

        private MapTile[,] _tiles;
        public Vector2 TileSize { get; private set; }
        
        public LookupTable ExplosivesLookupTable { get; private set; }
        public LookupTable FireLookupTable { get; private set; }

        public MapTile this[Vector2 position]
        {
            get
            {
                return _tiles[position.X, position.Y];
            }
        }

        public MapTile this[int x, int y]
        {
            get
            {
                return _tiles[x, y];
            }
        }

        public GameMap(Vector2 mapSize, Vector2 viewSize)
        {
            Size = mapSize;
            ViewSize = viewSize;
            _tiles = new MapTile[mapSize.X, mapSize.Y];
            TileSize = new Vector2(viewSize.X / mapSize.X, viewSize.Y / mapSize.Y);
            ExplosivesLookupTable = new LookupTable();
            FireLookupTable = new LookupTable();
        }
        
        public void SetTile(int x, int y, Bitmap image)
        {
            int xWorld = TileSize.X * x;
            int yWorld = TileSize.Y * y;
            _tiles[x, y] = new MapTile(xWorld, yWorld, TileSize.X, TileSize.Y, image);
        }

        /// <param name="x">Index on x axis of the tile to place the new GameObject </param>
        /// <param name="y">Index on y axis of the tile to place the new GameObject </param>
        public Tuple<Vector2, Vector2, Vector4, Bitmap> CreateScaledGameObjectParameters(int x, int y, Bitmap image)
        {
            double heightToWidthRatio = image.Height / (double)image.Width;
            Vector2 position = new Vector2(_tiles[x, y].LocalPosition.X, _tiles[x, y].LocalPosition.Y - ((int)(heightToWidthRatio * TileSize.Y) - TileSize.Y));
            Vector2 size = new Vector2(TileSize.X, (int)(heightToWidthRatio * TileSize.Y));
            Vector4 collider = new Vector4(position.X, _tiles[x, y].LocalPosition.Y, position.X + TileSize.X, _tiles[x, y].LocalPosition.Y + TileSize.Y);
            return new Tuple<Vector2, Vector2, Vector4, Bitmap>(position, size, collider, image);
        }
    }
}
