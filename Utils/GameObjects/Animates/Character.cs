using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Utils.Math;
using Utils.GameLogic;
using System.Timers;
using Utils.GameObjects.Explosives;
using Utils.AbstractFactory;
using Utils.Observer;
using Utils.Helpers;

namespace Utils.GameObjects.Animates
{
    public class Character : GameObject
    {
        private int _explosivesPlaced;

        private System.Timers.Timer _iFramesTimer;
        private bool _isInIFrames;
        private int _movementSpeed;

        public Vector2 Facing { get; private set; }
        public int Health { get; private set; }
        public int MovementSpeed { get; set; }
        public int ExplosivesCapacity { get; private set; }
        public int ExplosivesRange { get; private set; }
        public int ExplosiveDamage { get; private set; }
        
        public int SpeedModifier { get; set; }

        public Bitmap ExplosiveImage { get; private set; }
        public Bitmap FireImage { get; private set; }

        public Explosive Explosive { get; private set; }
        public Fire Fire { get; private set; }
        public ILevelFactory LevelFactory { get; private set; }
        public Subject Subject { get; private set; }

        public Character(Vector2 position, Vector2 size, Vector4 collider, Bitmap image, Bitmap explosiveImage, Bitmap fireImage, ILevelFactory levelFactory, Subject subject)
            : base(position, size, collider, image)
        {
            this.Subject = subject;
            Initialize(explosiveImage, fireImage);
            LevelFactory = levelFactory;
        }

        public Character(Vector2 position, Vector2 size, Vector4 collider, Bitmap image, Bitmap explosiveImage, Bitmap fireImage, Subject subject)
            : base(position, size, collider, image)
        {
            this.Subject = subject;
            Initialize(explosiveImage, fireImage);
        }

        public Character(int x, int y, int width, int height, int cx, int cy, int cWidth, int cHeight, Bitmap image, Bitmap explosiveImage, Bitmap fireImage)
            : base(x, y, width, height, cx, cy, cWidth, cHeight, image)
        {
            Initialize(explosiveImage, fireImage);
        }

        private void Initialize(Bitmap explosiveImage, Bitmap fireImage)
        {
            _explosivesPlaced = 0;

            Facing = GameSettings.InitialPlayerDirection;
            Health = GameSettings.InitialPlayerHealth;
            _movementSpeed = GameSettings.InitialPlayerSpeed;
            ExplosivesCapacity = GameSettings.InitialPlayerCapacity;
            ExplosivesRange = GameSettings.InitialExplosionRange;
            ExplosiveDamage = GameSettings.InitialExplosionDamage;

            SpeedModifier = 0;

            FireImage = fireImage;
            ExplosiveImage = explosiveImage;

            _iFramesTimer = new System.Timers.Timer();
            _iFramesTimer.Elapsed += new ElapsedEventHandler(OnIFramesEnd);
            _iFramesTimer.Interval = GameSettings.InitialTimeTillExplosion;
            _isInIFrames = false;

            RegisterObserver();
        }

        private void StartIFramesTimer()
        {
            _iFramesTimer.Enabled = true;
            _isInIFrames = true;
        }

        private void OnIFramesEnd(object sender, ElapsedEventArgs e)
        {
            _iFramesTimer.Enabled = false;
            _isInIFrames = false;
        }

        public void TakeDamage(int amount) 
        {
            if (_isInIFrames)
                return;

            this.Subject.MakeSound("Damage"); 
            Health -= amount;
            StartIFramesTimer();
        }

        public void ChangeHealth(int amount)
        {
            Health += amount;
        }

        public void ChangeSpeed(int amount)
        {
            _movementSpeed += amount;
        }

        public void SetMoveSpeed(int amount)
        {
            _movementSpeed = amount;
        }

        public int GetSpeed()
        {
            return _movementSpeed + SpeedModifier;
        }

        public int GetMoveSpeed()
        {
            return _movementSpeed;
        }

        public void ChangeExplosivesCapacity(int amount)
        {
            ExplosivesCapacity += amount;
        }

        public void ChangeExplosivesRange(int amount)
        {
            ExplosivesRange += amount;
        }

        public void ChangeExplosivesDamage(int amount)
        {
            ExplosiveDamage += amount;
        }

        public bool CanPlaceExplosive()
        {
            return _explosivesPlaced < ExplosivesCapacity;
        }

        public void PlaceExplosive(GameMap gameMap)
        {
            Vector2 index = WorldPosition / gameMap.TileSize;
            if (gameMap[index].GameObject is EmptyGameObject && CanPlaceExplosive())
            {
                var prm = gameMap.CreateScaledGameObjectParameters(index.X, index.Y, ExplosiveImage);
                Explosive explosive = LevelFactory.CreateExplosive(prm.Item1, prm.Item2, prm.Item3, prm.Item4, FireImage);
                explosive.Range = ExplosivesRange;
                explosive.Damage = ExplosiveDamage;
                explosive.StartCountdown();
                gameMap[index].GameObject = explosive;
                gameMap.ExplosivesLookupTable.Set(index, explosive);
                _explosivesPlaced++;
                this.Subject.MakeSound("PlaceBomb");
            }
        }

        public void GiveExplosive()
        {
            if (_explosivesPlaced <= 0)
                return;

            _explosivesPlaced--;
        }

        /// <param name="direction">Vienetinis vektorius</param>
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

        public void Teleport(Vector2 position)
        {
            Vector2 vPtoC = new Vector2(Collider.X, Collider.Y) - LocalPosition;
            Vector2 vTLtoBR = new Vector2(Collider.Z - Collider.X, Collider.W - Collider.Y);
            LocalPosition = position;
            int tlx = LocalPosition.X + vPtoC.X;
            int tly = LocalPosition.Y + vPtoC.Y;
            int brx = tlx + vTLtoBR.X;
            int bry = tly + vTLtoBR.Y;
            Collider = new Vector4(tlx, tly, brx, bry);
        }
        public void RegisterObserver()
        {
            DamageObserver damageObserver = new();
            ExplosionObserver explosionObserver = new();
            FireObserver fireObserver = new();
            PlaceBombObserver placeBombObserver = new();

            this.Subject.RegisterObserver(damageObserver);
            this.Subject.RegisterObserver(explosionObserver);
            this.Subject.RegisterObserver(fireObserver);
            this.Subject.RegisterObserver(placeBombObserver);
        }
    }
}
