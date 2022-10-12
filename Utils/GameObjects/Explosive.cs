using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Utils.Math;
using Utils.GameLogic;
using System.Timers;
using System.Reflection;

namespace Utils.GameObjects
{
    public abstract class Explosive : GameObject
    {
        private int _range;
        private int _timeTillExplosion;
        private Vector2[] _explosionDirections = new Vector2[0];
        private System.Timers.Timer _explosionTimer;
        private bool _coundownEnded;

        public int Range { get { return _range; } }
        public int TimeTillExplosion { get { return _timeTillExplosion; } }
        public Vector2[] ExplosionDirections { get { return _explosionDirections; } }

        public Bitmap FireImage { get; private set; }
        public int Damage { get; set; }

        public Explosive()
        {
            _timeTillExplosion = Settings.InitialTimeTillExplosion;
            _explosionTimer = new System.Timers.Timer();
            _explosionTimer.Elapsed += new ElapsedEventHandler(OnCountdownEnd);
            _explosionTimer.Interval = Settings.InitialTimeTillExplosion;
            _coundownEnded = false;
            Damage = Settings.InitialExplosionDamage;
        }

        public Explosive(Vector2 position, Vector2 size, Vector4 collider, Bitmap image, Bitmap fireImage) : base(position, size, collider, image)
        {
            _timeTillExplosion = Settings.InitialTimeTillExplosion;
            _explosionTimer = new System.Timers.Timer();
            _explosionTimer.Elapsed += new ElapsedEventHandler(OnCountdownEnd);
            _explosionTimer.Interval = Settings.InitialTimeTillExplosion;
            _coundownEnded = false;
            FireImage = fireImage;
            Damage = Settings.InitialExplosionDamage;
        }

        public Explosive(int x, int y, int width, int height, int cx, int cy, int cWidth, int cHeight, Bitmap image, Bitmap fireImage)
            : base(x, y, width, height, cx, cy, cWidth, cHeight, image)
        {
            _timeTillExplosion = Settings.InitialTimeTillExplosion;
            _explosionTimer = new System.Timers.Timer();
            _explosionTimer.Elapsed += new ElapsedEventHandler(OnCountdownEnd);
            _explosionTimer.Interval = Settings.InitialTimeTillExplosion;
            _coundownEnded = false;
            FireImage = fireImage;
            Damage = Settings.InitialExplosionDamage;
        }

        protected void SetExplosionDirections(Vector2[] explosionDirections)
        {
            _explosionDirections = explosionDirections;
        }

        public void SetRange(int range)
        {
            _range = range;
        }

        public void StartCountdown()
        {
            _explosionTimer.Enabled = true;
        }

        private void OnCountdownEnd(object source, ElapsedEventArgs e)
        {
            _coundownEnded = true;
        }

        public void UpdateState(Map gameMap, Character player)
        {
            if (_coundownEnded)
            {
                Explode(gameMap);
                player.GiveExplosive();
            }
        }

        private void Explode(Map gameMap)
        {
            Vector2 thisIndex = WorldPosition / gameMap.TileSize;

            for (int i = 0; i < ExplosionDirections.Length; i++)
            {
                Vector2 index = thisIndex + ExplosionDirections[i];
                GameObject gameObject = gameMap.Tiles[index.X, index.Y].GameObject;
                int range = 1;
                while ((gameObject is EmptyGameObject || gameObject is Fire) && range < _range)
                {
                    var prm = gameMap.CreateScaledGameObjectParameters(index.X, index.Y, FireImage);
                    Fire fireGO = new Fire(prm.Item1, prm.Item2, prm.Item3, prm.Item4);
                    fireGO.Damage = Damage;
                    gameMap.Tiles[index.X, index.Y].GameObject = fireGO;
                    gameMap.FireLookupTable.Set(index, fireGO);
                    fireGO.StartBurning();

                    index += ExplosionDirections[i];
                    gameObject = gameMap.Tiles[index.X, index.Y].GameObject;
                    range++;
                }
            }

            gameMap.ExplosivesLookupTable.Remove(thisIndex);
            var prms = gameMap.CreateScaledGameObjectParameters(thisIndex.X, thisIndex.Y, FireImage);
            Fire fire = new Fire(prms.Item1, prms.Item2, prms.Item3, prms.Item4);
            gameMap.Tiles[thisIndex.X, thisIndex.Y].GameObject = fire;
            gameMap.FireLookupTable.Set(thisIndex, fire);
            fire.StartBurning();
        }

    }
}
