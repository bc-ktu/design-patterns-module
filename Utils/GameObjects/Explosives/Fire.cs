using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

using Utils.AbstractFactory;
using Utils.GameLogic;
using Utils.GameObjects.Crates;
using Utils.Map;
using Utils.Math;

namespace Utils.GameObjects.Explosives
{
    public class Fire : TriggerGameObject
    {
        private System.Timers.Timer _burnTimer;
        private bool _isBurning;

        public int Damage { get; set; }

        public Fire(Vector2 position, Vector2 size, Vector4 collider, Bitmap image) 
            : base(position, size, collider, image)
        {
            Initialize();
        }

        public Fire(int x, int y, int width, int height, int cx, int cy, int cWidth, int cHeight, Bitmap image)
            : base(x, y, width, height, cx, cy, cWidth, cHeight, image)
        {
            Initialize();
        }

        private void Initialize()
        {
            _burnTimer = new System.Timers.Timer();
            _burnTimer.Elapsed += new ElapsedEventHandler(OnBurnEnd);
            _burnTimer.Interval = GameSettings.InitialFireBurnTime;
            _isBurning = true;
        }

        public void StartBurning()
        {
            _burnTimer.Enabled = true;
        }

        private void OnBurnEnd(object sender, ElapsedEventArgs e)
        {
            _isBurning = false;
        }

        public void UpdateState(GameMap gameMap, ILevelFactory levelFactory)
        {
            if (!_isBurning)
                Dissapear(gameMap, levelFactory);
        }

        public void Dissapear(GameMap gameMap, ILevelFactory levelFactory)
        {
            Vector2 thisIndex = WorldPosition / gameMap.TileSize;
            gameMap[thisIndex].GameObjects.Remove(this);
            gameMap.FireLookupTable.Remove(thisIndex, this);
            DestroyGameObjects(gameMap, levelFactory);
        }

        private void DestroyGameObjects(GameMap gameMap, ILevelFactory levelFactory)
        {
            Vector2 thisIndex = WorldPosition / gameMap.TileSize;
            
            for (int i = 0; i < gameMap[thisIndex].GameObjects.Count; i++)
            {
                if (gameMap[thisIndex].GameObjects[i] is not DestructableGameObject)
                    return;

                DestructableGameObject dgo = (DestructableGameObject)gameMap[thisIndex].GameObjects[i];

                dgo.DecreaseDurability();
                if (dgo.Durability > 0)
                    return;

                gameMap[thisIndex].GameObjects.Remove(dgo);
                i--;
                        
                if (dgo is not Crate)
                    return;

                Crate crate = (Crate)dgo;
                crate.CreatePowerup(gameMap, levelFactory);
            }
        }
    }
}
