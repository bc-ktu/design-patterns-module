using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Utils.Math;
using Utils.GameLogic;
using System.Timers;
using Utils.GameObjects.Animates;

namespace Utils.GameObjects.Explosives
{
    public abstract class Explosive : GameObject
    {
        private System.Timers.Timer _explosionTimer;
        private bool _coundownEnded;

        public int Damage { get; set; }
        public int Range { get; set; }
        public Vector2[] ExplosionDirections { get; protected set; }

        public Bitmap FireImage { get; private set; }

        public Explosive()
        {
            Initialize(null);
        }

        public Explosive(Vector2 position, Vector2 size, Vector4 collider, Bitmap image, Bitmap fireImage) : base(position, size, collider, image)
        {
            Initialize(fireImage);
        }

        public Explosive(int x, int y, int width, int height, int cx, int cy, int cWidth, int cHeight, Bitmap image, Bitmap fireImage)
            : base(x, y, width, height, cx, cy, cWidth, cHeight, image)
        {
            Initialize(fireImage);
        }

        private void Initialize(Bitmap fireImage)
        {
            Damage = GameSettings.InitialExplosionDamage;
            Range = GameSettings.InitialExplosionRange;

            _explosionTimer = new System.Timers.Timer();
            _explosionTimer.Elapsed += new ElapsedEventHandler(OnCountdownEnd);
            _explosionTimer.Interval = GameSettings.InitialTimeTillExplosion;
            _coundownEnded = false;

            FireImage = fireImage;
        }

        public void StartCountdown()
        {
            _explosionTimer.Enabled = true;
        }

        private void OnCountdownEnd(object source, ElapsedEventArgs e)
        {
            _coundownEnded = true;
        }

        public void UpdateState(GameMap gameMap, Character player)
        {
            if (_coundownEnded)
            {
                Explode(gameMap);
                player.GiveExplosive();
            }
        }

        private void Explode(GameMap gameMap) // add index out of bounds handling (fake object)
        {
            Vector2 thisIndex = WorldPosition / gameMap.TileSize;

            for (int i = 0; i < ExplosionDirections.Length; i++)
            {
                Vector2 index = thisIndex + ExplosionDirections[i];
                GameObject gameObject = gameMap.Tiles[index.X, index.Y].GameObject;
                int range = 1;
                while ((gameObject is EmptyGameObject || gameObject is Fire) && range < Range)
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
