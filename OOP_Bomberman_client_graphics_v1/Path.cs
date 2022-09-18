using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Bomberman_client_graphics_v1
{
    internal static class Path
    {
        public static string FolderAssets = "Assets/";
        public static string FolderTextures = "Textures/";
        public static string FolderSpritesheets = "Spritesheets/";
        public static string FolderSprites = "Sprites/";

        public static string Create(params string[] args)
        {
            string pathCreated = "";

            for (int i = 0; i < args.Length; i++)
                pathCreated += args[i].ToString();

            return pathCreated;
        }
    }
}
