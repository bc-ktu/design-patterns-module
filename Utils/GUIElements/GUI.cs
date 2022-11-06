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

        private Vector2 _elementSize;

        private GUIIcon[] _icons = new GUIIcon[NumberOfElements];
        private GUIText[] _texts = new GUIText[NumberOfElements];

        public Bitmap FrameImage { get; private set; }
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }

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

        public Rectangle[] Rectangles { get; private set; }

        public Font Font { get; private set; }
        public Brush FontColor { get; private set; }
        public int FontSize { get; private set; }

        public GUI(Vector2 position, Vector2 size, Bitmap frameImage, Font font, Brush fontColor)
        {
            Rectangles = new Rectangle[2 * NumberOfElements];
            Position = position;
            Size = size;
            _elementSize = new Vector2(size.X / NumberOfElements, size.Y / 2);
            FrameImage = frameImage;
            FontColor = fontColor;
            Font = font;

            for (int i = 0; i < NumberOfElements; i++)
            {
                int x = Position.X + i * _elementSize.X;
                int y = Position.Y;
                _icons[i] = new GUIIcon();
                _icons[i].SetPosition(x, y);
                _icons[i].SetSize(_elementSize);

                y = Position.Y + _elementSize.Y;
                _texts[i] = new GUIText();
                _texts[i].SetPosition(x, y);
                _texts[i].SetSize(_elementSize);
                _texts[i].SetText("0");

                Rectangles[i * 2] = new Rectangle(x, Position.Y, _elementSize.X, _elementSize.Y);
                Rectangles[i * 2 + 1] = new Rectangle(x, y, _elementSize.X, _elementSize.Y);
            }
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

        public void SetHealthValue(object value)
        {
            _texts[HealthIndex].SetText(value.ToString());
        }

        public void SetSpeedValue(object value)
        {
            _texts[SpeedIndex].SetText(value.ToString());
        }

        public void SetCapacityValue(object value)
        {
            _texts[CapacityIndex].SetText(value.ToString());
        }

        public void SetRangeValue(object value)
        {
            _texts[RangeIndex].SetText(value.ToString());
        }

        public void SetDamageValue(object value)
        {
            _texts[DamageIndex].SetText(value.ToString());
        }

        public Rectangle ToRectangle()
        {
            return new Rectangle(Position.X, Position.Y, Size.X, Size.Y);
        }

    }
}
