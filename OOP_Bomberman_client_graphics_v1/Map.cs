using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Bomberman_client_graphics_v1
{
    internal class Map
    {
        private Vector2 _size;
        private Vector2 _viewSize;
        
        private MapTile[,] _tiles;
        private Vector2 _tileSize;

        public Vector2 Size { get { return _size; } }
        public MapTile[,] Tiles { get { return _tiles; } }

        public Map(Vector2 mapSize, Vector2 viewSize)
        {
            _size = mapSize;
            _viewSize = viewSize;
            _tiles = new MapTile[mapSize.X, mapSize.Y];
            _tileSize = new Vector2(viewSize.X / mapSize.X, viewSize.Y / mapSize.Y);
        }

        public Map(int mapWidth, int mapHeight, int viewWidth, int viewHeight)
        {
            _size = new Vector2(mapWidth, mapHeight);
            _viewSize = new Vector2(viewWidth, viewHeight);
            _tiles = new MapTile[mapWidth, mapHeight];
            _tileSize = new Vector2(viewWidth / mapHeight, viewHeight / mapHeight);
        }

        public void SetTile(int x, int y, Bitmap image)
        {
            int xWorld = _tileSize.X * x;
            int yWorld = _tileSize.Y * y;
            _tiles[x, y] = new MapTile(xWorld, yWorld, _tileSize.X, _tileSize.Y, image);
        }
    }
}
