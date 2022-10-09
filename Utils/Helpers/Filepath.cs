using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils.Helpers
{
    public static class Filepath
    {
        public static readonly string FolderAssets = "Assets/";
        public static readonly string FolderTextures = "Textures/";
        public static readonly string FolderSpritesheets = "Spritesheets/";
        public static readonly string FolderSprites = "Sprites/";
        public static readonly string FolderExplodables = "Explodables/";
        public static readonly string FolderWalls = "Walls/";
        public static readonly string FolderExplosives = "Explosives/";

        public static string Create(params string[] args)
        {
            string pathCreated = "";

            for (int i = 0; i < args.Length; i++)
                pathCreated += args[i].ToString();

            return pathCreated;
        }
    }
}
