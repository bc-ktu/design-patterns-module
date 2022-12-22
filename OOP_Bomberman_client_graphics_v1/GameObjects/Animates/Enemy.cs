using Utils.Math;
using client_graphics.GameLogic;
using System.Timers;
using client_graphics.GameObjects.Explosives;
using client_graphics.AbstractFactory;
using Utils.Observer;
using client_graphics.Map;
using client_graphics.Strategy;
using Utils.Decorator;
using client_graphics.GameLogic;

namespace client_graphics.GameObjects.Animates
{
    public class Enemy : TriggerGameObject, IGraphicsDecorator
    {
        public IGraphicsDecorator wrappee { get; private set; }

        private System.Timers.Timer _iFramesTimer;
        private bool _isInIFrames;
        private int _movementSpeed;

        public Moves movingType;

        public int Health { get; private set; }
        public int MovementSpeed { get; set; }

        public Enemy() { }

        public Enemy(Enemy p) : base(p)
        {
            _isInIFrames = p._isInIFrames;
            _movementSpeed = p._movementSpeed;
            Health = p.Health;
            MovementSpeed = p.MovementSpeed;
        }

        public Enemy(Vector2 position, Vector2 size, Vector4 collider, Bitmap image, Explosive explosive, Subject subject)
            : base(position, size, collider, image)
        {
            Initialize();
        }

        private void Initialize()
        {
            wrappee = null;

            Health = GameSettings.InitialPlayerHealth;
            _movementSpeed = GameSettings.InitialPlayerSpeed;
            _iFramesTimer = new System.Timers.Timer();
            _iFramesTimer.Elapsed += new ElapsedEventHandler(OnIFramesEnd);
            _iFramesTimer.Interval = GameSettings.InitialIFramesTime;
            _isInIFrames = false;
        }

        private void OnIFramesEnd(object sender, ElapsedEventArgs e)
        {
            _iFramesTimer.Enabled = false;
            _isInIFrames = false;
        }

        public void Moving(Vector2 direction, int speed, Vector4 Collider, Vector2 LocalPosition, GameMap gameMap, Enemy player)
        {
            movingType.Move(direction, speed, Collider, LocalPosition, gameMap, player);
        }

        public void SetMovingAbility(Moves newMovingType)
        {
            movingType = newMovingType;
        }

        public int GetSpeed()
        {
            return _movementSpeed;
        }

        public override GameObject Clone()
        {
            return new Enemy(this);
        }
    }
}
