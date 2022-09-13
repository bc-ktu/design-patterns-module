using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_CardGame_PrototypeV1
{
    internal class GameCard : Interactable
    {
        private Bitmap _frontImage;
        private Bitmap _monsterImage;
        private Bitmap _backImage;

        public Bitmap FrontImage { get { return _frontImage; } }
        public Bitmap MonsterImage { get { return _monsterImage; } }
        public Bitmap BackImage { get { return _backImage; } }

        public GameCard(Bitmap frontImage, Bitmap monsterImage, Bitmap backImage, Vector2 position, Vector2 size, bool upsideDown = false)
        {
            _frontImage = frontImage;
            _monsterImage = monsterImage;
            _backImage = backImage;
            
            _size = size;
            _rotation = upsideDown ? -1 : 1;  // branchless possible?
            
            _position = position;
        }

        public GameCard(Bitmap frontImage, Bitmap monsterImage, Bitmap backImage, int x, int y, int width, int height, bool upsideDown = false)
        {
            _frontImage = frontImage;
            _monsterImage = monsterImage;
            _backImage = backImage;
            
            _size = new Vector2(width, height);
            _rotation = upsideDown ? -1 : 1;
            
            _position = new Vector2(x, y);
        }
    }
}
