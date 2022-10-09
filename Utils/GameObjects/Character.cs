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
    public class Character : GameObject
    {
        private Vector2 _facing;
        private int _movementSpeed;
        private int _explosivesCapacity;
        private int _explosivesPlaced;
        private int _explosiveRange;
        private int _health;

        private Bitmap _explosiveImage;

        public Vector2 Facing { get { return _facing; } }
        public int MovementSpeed { get { return _movementSpeed; } }
        public int ExplosivesCapacity { get { return _explosivesCapacity; } }
        public int ExplosivesRange { get { return _explosiveRange; } }
        public int Health { get { return _health; } }

        public Character(Vector2 position, Vector2 size, Vector4 collider, Bitmap image, Bitmap explosiveImage) : base(position, size, collider, image)
        {
            _facing = Settings.InitialPlayerDirection;
            _movementSpeed = Settings.InitialPlayerSpeed;
            _explosivesCapacity = Settings.InitialPlayerCapacity;
            _explosivesPlaced = 0;
            _explosiveRange = Settings.InitialPlayerRange;
            _health = Settings.InitialPlayerHealth;
            _explosiveImage = explosiveImage;
        }

        public Character(int x, int y, int width, int height, int cx, int cy, int cWidth, int cHeight, Bitmap image, Bitmap explosiveImage) 
            : base(x, y, width, height, cx, cy, cWidth, cHeight, image)
        {
            _facing = Settings.InitialPlayerDirection;
            _movementSpeed = Settings.InitialPlayerSpeed;
            _explosivesCapacity = Settings.InitialPlayerCapacity;
            _explosivesPlaced = 0;
            _explosiveRange = Settings.InitialPlayerRange;
            _health = Settings.InitialPlayerHealth;
            _explosiveImage = explosiveImage;
        }

        public void ChangeSpeed(int amount)
        {
            _movementSpeed += amount;
        }

        public void ChangeBombCapacity(int amount)
        {
            _explosivesCapacity += amount;
        }

        public void ChangeBombRange(int amount)
        {
            _explosiveRange += amount;
        }

        public void ChangeHealth(int amount)
        {
            _health += amount;
        }

        public bool CanPlaceBomb()
        {
            return _explosivesPlaced <= _explosivesCapacity;
        }

        public void PlaceBomb(Map gameMap)
        {
            Vector2 index = _position / gameMap.TileSize;
            if (gameMap.Tiles[index.X, index.Y].GameObject is EmptyGameObject)
            {
                var prm = gameMap.CreateScaledGameObjectParameters(index.X, index.Y, _explosiveImage);
                GameObject explosive = new ExplosiveHV(); // Give parameters!
                gameMap.Tiles[index.X, index.Y].GameObject = explosive;
            }
        }

        public void Move(Vector2 direction)
        {
            Vector2 vPtoC = new Vector2(_collider.X, _collider.Y) - _position;
            Vector2 vTLtoBR = new Vector2(_collider.Z - _collider.X, _collider.W - _collider.Y);
            _facing = direction;
            _position += _movementSpeed * _facing;
            int tlx = _position.X + vPtoC.X;
            int tly = _position.Y + vPtoC.Y;
            int brx = tlx + vTLtoBR.X;
            int bry = tly + vTLtoBR.Y;
            _collider = new Vector4(tlx, tly, brx, bry);
        }

    }
}
