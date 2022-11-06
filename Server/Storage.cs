using Utils.Math;

namespace Server
{
    public static class Storage
    {
        public static int UserCount { get; set; } = 0;
        public static Dictionary<string, Vector2> Players = new();
        public static MapSeedGenerator generator = MapSeedGenerator.GetInstance();
    }
}