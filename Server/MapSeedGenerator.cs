
using Utils.Math;

namespace Server
{
    public class MapSeedGenerator
    {
        private List<int>? values { get; set; }
        private Vector2 _mapSize { get; set; }
        private static MapSeedGenerator _instance = new MapSeedGenerator();

        public void GenerateSeed(Vector2 mapSize, int level)
        {
            values = new List<int>();
            values.Add(level);
            _mapSize = mapSize;
            Random random = new Random();
            for (int i = 0; i < (mapSize.X - 2) * (mapSize.Y - 2); i++)
            {
                var randomNumber = random.Next(0, 6);
                values.Add(randomNumber);
                if (randomNumber == 0)
                {
                    values.Add(random.Next(0, 4));
                }
            }
            for (int j = 0; j < (mapSize.X - 2) * (mapSize.Y - 2); j++)
            {
                values.Add(random.Next(0, 10));
            }
            for (int i = 0; i < 5; i++)
            {
                int rx = random.Next(1, mapSize.X - 1);
                int ry = random.Next(1, mapSize.Y - 1);
                values.Add(rx);
                values.Add(ry);
            }
        }
        public List<int> GetValues()
        {
            return values;
        }
        public static MapSeedGenerator GetInstance()
        {
            return _instance;
        }
        public Vector2 GetMapSize()
        {
            return _mapSize;
        }
    }
}
