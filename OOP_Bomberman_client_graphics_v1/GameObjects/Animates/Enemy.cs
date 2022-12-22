using Utils.Math;
using client_graphics.GameLogic;
using System.Timers;
using client_graphics.GameObjects.Explosives;
using client_graphics.AbstractFactory;
using Utils.Observer;
using client_graphics.Map;
using client_graphics.Template;
using Utils.Decorator;
using client_graphics.GameLogic;

namespace client_graphics.GameObjects.Animates
{
    public abstract class Enemy : TriggerGameObject, IGraphicsDecorator
    {
        public IGraphicsDecorator wrappee { get; private set; }

        protected System.Timers.Timer _iFramesTimer;
        protected bool _isInIFrames;
        protected int _movementSpeed;

        public MoveAlgorithm movingType;

        public int Health { get; private set; }
        public int MovementSpeed { get; set; }
        public Vector2 Facing { get; protected set; }

        public Enemy() { }

        public Enemy(Enemy e) : base(e)
        {
            _isInIFrames = e._isInIFrames;
            _movementSpeed = e._movementSpeed;
            Health = e.Health;
            MovementSpeed = e.MovementSpeed;
        }

        public Enemy(Vector2 position, Vector2 size, Vector4 collider, Bitmap image, int speed)
            : base(position, size, collider, image)
        {
            Initialize(speed);
        }

        protected void Initialize(int speed)
        {
            wrappee = null;

            Health = GameSettings.InitialPlayerHealth;
            _movementSpeed = GameSettings.InitialPlayerSpeed;
            _iFramesTimer = new System.Timers.Timer();
            _iFramesTimer.Elapsed += new ElapsedEventHandler(OnIFramesEnd);
            _iFramesTimer.Interval = GameSettings.InitialIFramesTime;
            _isInIFrames = false;

            MovementSpeed = speed;
        }

        protected void OnIFramesEnd(object sender, ElapsedEventArgs e)
        {
            _iFramesTimer.Enabled = false;
            _isInIFrames = false;
        }

        public void Move(Vector2 direction)
        {
            Vector2 vPtoC = new Vector2(Collider.X, Collider.Y) - LocalPosition;
            Vector2 vTLtoBR = new Vector2(Collider.Z - Collider.X, Collider.W - Collider.Y);
            Facing = direction;
            LocalPosition += GetSpeed() * Facing;
            int tlx = LocalPosition.X + vPtoC.X;
            int tly = LocalPosition.Y + vPtoC.Y;
            int brx = tlx + vTLtoBR.X;
            int bry = tly + vTLtoBR.Y;
            Collider = new Vector4(tlx, tly, brx, bry);
        }

        protected void Move(GameMap gameMap)
        {
            movingType.Move(Facing, this, gameMap);
        }

        public void SetMovingAbility(MoveAlgorithm newMovingType)
        {
            movingType = newMovingType;
        }

        public int GetSpeed()
        {
            return _movementSpeed;
        }

        public abstract void Add(Enemy e);
        public abstract void Remove(Enemy e);
        public abstract void Action(GameMap gameMap);
    }
}
