using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Utils.Math;

namespace Utils.GUIElements
{
    public class GUI
    {
        public static readonly int NumberOfElements = 5;
        private static readonly int HealthIndex = 0;
        private static readonly int SpeedIndex = 1;
        private static readonly int CapacityIndex = 2;
        private static readonly int RangeIndex = 3;
        private static readonly int DamageIndex = 4;

        private Bitmap _frameImage;
        private Vector2 _position;
        private Vector2 _size;

        private Vector2 _elementSize;

        private GUIIcon[] _icons = new GUIIcon[NumberOfElements];
        private GUIText[] _texts = new GUIText[NumberOfElements];
        private Rectangle[] _rectanges = new Rectangle[2 * NumberOfElements];

        public Bitmap FrameImage { get { return _frameImage; } }
        public Vector2 Position { get { return _position; } }
        public Vector2 Size { get { return _size; } }

        public GUIIcon HealthIcon { get { return _icons[HealthIndex]; } }
        public GUIIcon SpeedIcon { get { return _icons[SpeedIndex]; } }
        public GUIIcon CapacityIcon { get { return _icons[CapacityIndex]; } }
        public GUIIcon RangeIcon { get { return _icons[RangeIndex]; } }
        public GUIIcon DamageIcon { get { return _icons[DamageIndex]; } }

        public GUIText HealthText { get { return _texts[HealthIndex]; } }
        public GUIText SpeedText { get { return _texts[SpeedIndex]; } }
        public GUIText CapacityText { get { return _texts[CapacityIndex]; } }
        public GUIText RangeText { get { return _texts[RangeIndex]; } }
        public GUIText DamageText { get { return _texts[DamageIndex]; } }

        public Rectangle[] Rectangles { get { return _rectanges; } }
        public Color FrameColor { get; set; }
        public int FrameThickness { get; set; }

        public GUI(Vector2 position, Vector2 size, Bitmap frameImage)
        {
            _position = position;
            _size = size;
            _elementSize = new Vector2(size.X / NumberOfElements, size.Y / 2);
            _frameImage = frameImage;

            for (int i = 0; i < NumberOfElements; i++)
            {
                int x = _position.X + i * _elementSize.X;
                int y = _position.Y;
                _icons[i] = new GUIIcon();
                _icons[i].SetPosition(x, y);
                _icons[i].SetSize(_elementSize);

                y = _position.Y + _elementSize.Y;
                _texts[i] = new GUIText();
                _texts[i].SetPosition(x, y);
                _texts[i].SetSize(_elementSize);
                _texts[i].SetText("0");

                _rectanges[i * 2] = new Rectangle(x, _position.Y, _elementSize.X, _elementSize.Y);
                _rectanges[i * 2 + 1] = new Rectangle(x, y, _elementSize.X, _elementSize.Y);
            }

            FrameColor = Color.FromArgb(255, 37, 13, 8);
            FrameThickness = 4;
        }

        public void SetHealthImage(Bitmap image)
        {
            _icons[HealthIndex].SetImage(image);
        }

        public void SetSpeedImage(Bitmap image)
        {
            _icons[SpeedIndex].SetImage(image);
        }

        public void SetCapacityImage(Bitmap image)
        {
            _icons[CapacityIndex].SetImage(image);
        }

        public void SetRangeImage(Bitmap image)
        {
            _icons[RangeIndex].SetImage(image);
        }

        public void SetDamageImage(Bitmap image)
        {
            _icons[DamageIndex].SetImage(image);
        }

        public void SetHealthValue(string value)
        {
            _texts[HealthIndex].SetText(value);
        }

        public void SetSpeedValue(string value)
        {
            _texts[SpeedIndex].SetText(value);
        }

        public void SetCapacityValue(string value)
        {
            _texts[CapacityIndex].SetText(value);
        }

        public void SetRangeValue(string value)
        {
            _texts[RangeIndex].SetText(value);
        }

        public void SetDamageValue(string value)
        {
            _texts[DamageIndex].SetText(value);
        }

        public Rectangle ToRectangle()
        {
            return new Rectangle(_position.X, _position.Y, _size.X, _size.Y);
        }

    }
}
