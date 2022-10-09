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
                    Values.Add(random.Next(0,4));
                }
            }
            for (int j = 0; j < (MapSize.X - 2) * (MapSize.Y - 2); j++)
            {
                Values.Add(random.Next(0, 10));
            }
        }
        public List<int> getValues()
        {
            return Values;
        }
    }
}
