using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_CardGame_PrototypeV1
{
    internal class GameTable : IDrawable
    {
        private Vector2 _size;
        private Bitmap _image;
        private GameTile[,] _cardTiles; // 0 - player, 1 - rival
        private int _playerTilesetSize;
        private int _rivalTilsetSize;
        private int _maxSize;

        public Vector2 Size { get { return _size; } }
        public Bitmap Image { get { return _image; } }
        public GameTile[,] CardTiles { get { return _cardTiles; } } 
        public int PlayerTilesetSize { get { return _playerTilesetSize; } }
        public int RivalTilesetSize { get { return _rivalTilsetSize; } }
        public int MaxTilesetSize { get { return _maxSize; } }

        public static readonly int Rival = 1;
        public static readonly int Player = 0;

        public GameTable(Bitmap image, Vector2 size, int tilesetSize)
        {
            _size = size;
            _image = image;
            _cardTiles = new GameTile[2, tilesetSize];
            _maxSize = tilesetSize;
            _playerTilesetSize = _rivalTilsetSize = 0;
        }

        public GameTable(Bitmap image, int width, int height, int tilesetSize)
        {
            _size = new Vector2(width, height);
            _image = image;
            _cardTiles = new GameTile[2, tilesetSize];
            _maxSize = tilesetSize;
            _playerTilesetSize = _rivalTilsetSize = 0;
        }

        public void AddTile(GameTile tile, int user)
        {
            if (user == Player)
            {
                CardTiles[Player, _playerTilesetSize] = tile;
                _playerTilesetSize++;
            }
            else
            {
                CardTiles[Rival, _rivalTilsetSize] = tile;
                _rivalTilsetSize++;
            }
        }

        public Rectangle ToRectangle()
        {
            return new Rectangle(0, 0, _size.x, _size.y);
        }
    }
}
