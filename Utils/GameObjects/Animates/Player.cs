﻿using Utils.Math;
using Utils.GameLogic;
using System.Timers;
using Utils.GameObjects.Explosives;
using Utils.AbstractFactory;
using Utils.Observer;
using Utils.Map;
using Utils.Decorator;

namespace Utils.GameObjects.Animates
{
    public class Player : TriggerGameObject, IGraphicsDecorator
    {
        public IGraphicsDecorator wrappee { get; private set; }

        private int _explosivesPlaced;

        private System.Timers.Timer _iFramesTimer;
        private bool _isInIFrames;
        private int _movementSpeed;

        public Vector2 Facing { get; private set; }
        public int Health { get; private set; }
        public int SpeedModifier { get; set; }
        public int MovementSpeed { get; set; }
        public int ExplosivesCapacity { get; private set; }
        public int ExplosiveRange { get; private set; }
        public int ExplosiveFireDamage { get; private set; }
        public Explosive Explosive { get; private set; }
        
        public Subject Subject { get; private set; }

        public Player() { }

        public Player(Player p) : base(p) 
        {
            _explosivesPlaced = p._explosivesPlaced;
            _isInIFrames = p._isInIFrames;
            _movementSpeed = p._movementSpeed;
            Facing = p.Facing.Clone();
            Health = p.Health;
            Subject = p.Subject;
            MovementSpeed = p.MovementSpeed;
            ExplosivesCapacity = p.ExplosivesCapacity;
            SpeedModifier = p.SpeedModifier;
            Explosive = (Explosive)p.Explosive.Clone();
        }

        public Player(Vector2 position, Vector2 size, Vector4 collider, Bitmap image, Explosive explosive, Subject subject)
            : base(position, size, collider, image)
        {
            Initialize(explosive, subject);
        }

        public Player(int Heatlh, int Speed, int ExplosivesCapacit)
        {
            Health = Heatlh;
            MovementSpeed = Speed;
            ExplosivesCapacity = ExplosivesCapacit;
            ExplosiveRange = GameSettings.InitialExplosionRange; //2
            ExplosiveFireDamage = GameSettings.InitialExplosionDamage; //1
        }

        public Player(int x, int y, int width, int height, int cx, int cy, int cWidth, int cHeight, Bitmap image, Explosive explosive, Subject subject)
            : base(x, y, width, height, cx, cy, cWidth, cHeight, image)
        {
            Initialize(explosive, subject);
        }

        public Player(Explosive explosive, Subject subject)
        {
            Initialize(explosive, subject);
        }

        public bool IsPlayerDead(int health)
        {
            if (health <= 0) return true;
            else return false;
        }

        private void Initialize(Explosive explosive, Subject subject)
        {
            wrappee = null;
            Explosive = explosive;

            _explosivesPlaced = 0;
            SpeedModifier = 0;

            Facing = GameSettings.InitialPlayerDirection;
            Health = GameSettings.InitialPlayerHealth;
            _movementSpeed = GameSettings.InitialPlayerSpeed;
            ExplosivesCapacity = GameSettings.InitialPlayerCapacity;
            //Explosive.Range = GameSettings.InitialExplosionRange;
            //Explosive.Fire.Damage = GameSettings.InitialExplosionDamage;
            
            _iFramesTimer = new System.Timers.Timer();
            _iFramesTimer.Elapsed += new ElapsedEventHandler(OnIFramesEnd);
            _iFramesTimer.Interval = GameSettings.InitialIFramesTime;
            _isInIFrames = false;

            this.Subject = subject;
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

            //this.Subject.MakeSound("Damage"); 
            Health -= amount;
            //StartIFramesTimer();
        }

        public void ChangeHealth(int amount)
        {
            Health += amount;
        }

        public void ChangeSpeed(int amount)
        {
            if (IsSpeedLimit(amount))
            {
                MovementSpeed = 10;
            }
            else MovementSpeed += amount;
        }

        public bool IsSpeedLimit(int movementSpeed)
        {
            int speedLimit = 10;
            if (MovementSpeed + movementSpeed > speedLimit)
            {
                return true;
            }
            else return false;
        }

        public void SetMoveSpeed(int amount)
        {
            MovementSpeed = amount;
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

        public void ChangeExplosiveRange(int amount)
        {
            ExplosiveRange += amount;
        }

        public void ChangeExplosiveDamage(int amount)
        {
            ExplosiveFireDamage += amount;
        }

        public bool CanPlaceExplosive()
        {
            return _explosivesPlaced < ExplosivesCapacity;
        }

        public void PlaceExplosive(GameMap gameMap, ILevelFactory levelFactory)
        {
            Vector2 index = WorldPosition / gameMap.TileSize;
            if (gameMap[index].IsEmpty && CanPlaceExplosive())
            {
                _explosivesPlaced++;

                Explosive explosive = (Explosive)Explosive.Clone();
                Vector2 position = index * gameMap.TileSize;
                explosive.Teleport(position);
                explosive.StartCountdown();

                gameMap[index].GameObjects.Add(explosive);
                gameMap.ExplosivesLookupTable.Add(index, explosive);

                this.Subject.MakeSound("PlaceBomb");
            }
        }

        public void GiveExplosive()
        {
            if (_explosivesPlaced <= 0)
                return;

            _explosivesPlaced--;
        }

        /// <param name="direction">unit vector</param>
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

        public override GameObject Clone()
        {
            return new Player(this);
        }

    }
}
