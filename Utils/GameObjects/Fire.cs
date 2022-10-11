using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Utils.GameLogic;
using Utils.Math;

namespace Utils.GameObjects
{
    public class Fire : GameObject
    {
        private System.Timers.Timer _burnTimer;
        private bool _isBurning;

        public int Damage { get; private set; }

        public Fire(Vector2 position, Vector2 size, Vector4 collider, Bitmap image) : base(position, size, collider, image)
        {
            _burnTimer = new System.Timers.Timer();
            _burnTimer.Elapsed += new ElapsedEventHandler(OnBurnEnd);
            _burnTimer.Interval = Settings.InitialFireBurnTime;
            _isBurning = true;
        }

        public Fire(int x, int y, int width, int height, int cx, int cy, int cWidth, int cHeight, Bitmap image)
            : base(x, y, width, height, cx, cy, cWidth, cHeight, image)
        {
            _burnTimer = new System.Timers.Timer();
            _burnTimer.Elapsed += new ElapsedEventHandler(OnBurnEnd);
            _burnTimer.Interval = Settings.InitialFireBurnTime;
            _isBurning = true;
        }

        public void SetDamage(int damage)
        {
            Damage = damage;
        }

        public void StartBurning()
        {
            _burnTimer.Enabled = true;
        }

        private void OnBurnEnd(object sender, ElapsedEventArgs e)
        {
            _isBurning = false;
        }

        public void UpdateState(Map gameMap, int indexInLookupTable)
        {
            if (!_isBurning)
                Dissapear(gameMap, indexInLookupTable);
        }

        public void Dissapear(Map gameMap, int indexInLookupTable)
        {
            Vector2 thisIndex = WorldPosition / gameMap.TileSize;
            gameMap.Tiles[thisIndex.X, thisIndex.Y].GameObject = new EmptyGameObject();
            gameMap.FireLookupTable.Remove(indexInLookupTable);
        }

    }
}
