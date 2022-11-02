﻿using System;
using Utils.Factory;
using Utils.GameObjects;
using Utils.Math;

namespace Server
{
    public class MapSeedGen
    {
        private Vector2 MapSize { get; set; }
        private List<int> Values { get; set; }

        public MapSeedGen(int x, int y)
        {
            MapSize = new Vector2(x, y);
            Values = new List<int>();
            GenerateSeed();
        }

        public void GenerateSeed()
        {
            Random random = new Random();
            for (int i = 0; i < (MapSize.X - 2) * (MapSize.Y - 2); i++)
            {
                var randomNumber = random.Next(0, 6);
                Values.Add(randomNumber);
                if (randomNumber == 0)
                {
                    Values.Add(random.Next(0, 4));
                }
            }
            for (int j = 0; j < (MapSize.X - 2) * (MapSize.Y - 2); j++)
            {
                Values.Add(random.Next(0, 10));
            }
            for (int i = 0; i < 5; i++)
            {
                int rx = random.Next(1, MapSize.X - 1);
                int ry = random.Next(1, MapSize.Y - 1);
                Values.Add(rx);
                Values.Add(ry);
            }
        }

        public List<int> getValues() // why non-capital method?
        {
            return Values;
        }
    }
}
