﻿using client_graphics.GameObjects;
using client_graphics.GameObjects.Animates;
using client_graphics.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client_graphics.Composite
{
    public class EnemyType : Enemy
    {
        private List<Enemy> _enemies = new List<Enemy>();

        public EnemyType()
        {

        }

        public EnemyType(Enemy e) : base(e)
        {
        }

        public override void Add(Enemy e)
        {
            _enemies.Add(e);
        }

        public override GameObject Clone()
        {
            return new EnemyType(this);
        }

        public override void Remove(Enemy e)
        {
            _enemies.Remove(e);
        }

        public override void Action(GameMap gameMap)
        {
            foreach (var item in _enemies)
            {
                item.Action(gameMap);
            }
        }
    }
}
