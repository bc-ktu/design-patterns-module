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
using Utils.Map;
using Utils.GameObjects.Walls;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Utils.GameObjects.Explosives
{
    public abstract class Explosive : SolidGameObject
    {
        private System.Timers.Timer _explosionTimer;
        public bool CountdownEnded { get; private set; }

        public int Damage { get; set; }
        public int Range { get; set; }
        public Vector2[] ExplosionDirections { get; protected set; }

        public Bitmap FireImage { get; private set; }

        public Explosive()
        {
            Initialize(null);
        }

        public Explosive(Explosive e) : base(e)
        {
            _explosionTimer = e._explosionTimer;
            CountdownEnded = e.CountdownEnded;
            Damage = e.Damage;
            Range = e.Range;
            ExplosionDirections = new Vector2[e.ExplosionDirections.Length];
            for (int i = 0; i < e.ExplosionDirections.Length; i++)
                ExplosionDirections[i] = new Vector2(e.ExplosionDirections[i].X, e.ExplosionDirections[i].Y);
            FireImage = e.FireImage;
        }

        public Explosive(Vector2 position, Vector2 size, Vector4 collider, Bitmap image, Bitmap fireImage) 
            : base(position, size, collider, image)
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
            CountdownEnded = false;

            FireImage = fireImage;
        }

        public void StartCountdown()
        {
            _explosionTimer.Enabled = true;
        }

        private void OnCountdownEnd(object source, ElapsedEventArgs e)
        {
            CountdownEnded = true;
        }

        public void UpdateState(GameMap gameMap, Player player)
        {
            if (CountdownEnded)
            {
                player.Subject.MakeSound("Explode");
                Explode(gameMap, player);
                player.GiveExplosive();
            }
        }

        private void Explode(GameMap gameMap, Player player)
        {
            CountdownEnded = true;
            Vector2 thisIndex = WorldPosition / gameMap.TileSize;

            for (int i = 0; i < ExplosionDirections.Length; i++)
            {
                Vector2 index = thisIndex + ExplosionDirections[i];
                bool indexInBounds = index.X >= 0 && index.X < gameMap.Size.X && index.Y >= 0 && index.Y < gameMap.Size.Y;
                if (!indexInBounds)
                    continue;

                int range = 1;
                bool hasSolidObject = gameMap.Has<SolidGameObject>(index);
                while (range < Range && indexInBounds && !hasSolidObject)
                {
                    CreateFire(gameMap, index);

                    index += ExplosionDirections[i];
                    indexInBounds = index.X >= 0 && index.X < gameMap.Size.X && index.Y >= 0 && index.Y < gameMap.Size.Y;
                    if (!indexInBounds)
                        break;

                    range++;
                    hasSolidObject = gameMap.Has<SolidGameObject>(index);
                }

                if (!indexInBounds || range >= Range || gameMap.Has<IndestructableWall>(index))
                    continue;

                CreateFire(gameMap, index);

                if (!gameMap.Has<Explosive>(index))
                    continue;

                TriggerExplosive(gameMap, index, player);
            }

            gameMap.ExplosivesLookupTable.Remove(thisIndex, this);
            gameMap[thisIndex].GameObjects.Remove(this);

            CreateFire(gameMap, thisIndex);
        }

        private void CreateFire(GameMap gameMap, Vector2 index)
        {
            var prm = gameMap.CreateScaledGameObjectParameters(index.X, index.Y, FireImage);
            Fire fireGO = new Fire(prm.Item1, prm.Item2, prm.Item3, prm.Item4);
            fireGO.Damage = Damage;
            gameMap[index].GameObjects.Add(fireGO);
            gameMap.FireLookupTable.Add(index, fireGO);
            fireGO.StartBurning();
        }

        private void TriggerExplosive(GameMap gameMap, Vector2 index, Player player)
        {
            for (int i = 0; i < gameMap[index].GameObjects.Count; i++)
            {
                if (gameMap[index].GameObjects[i] is not Explosive)
                    continue;

                Explosive explosive = (Explosive)gameMap[index].GameObjects[i];

                if (explosive.CountdownEnded)
                    continue;

                explosive.Explode(gameMap, player);
                player.GiveExplosive();
                i--;
            }
        }

    }
}
