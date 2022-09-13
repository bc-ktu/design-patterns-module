using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_CardGame_PrototypeV1
{
    internal static class PathUtils
    {
        public static string FolderAssets = "Assets/";

        public static string FolderAttributes = "CardAttributes/";
        public static string FolderBackFrames = "CardBackFrames/";
        public static string FolderFrontFrames = "CardFrontFrames/";
        public static string FolderCovers = "CardCovers/";
        public static string FolderMonsters = "CardMonsters/";
        public static string FolderPlacements = "CardPlacements/";
        public static string FolderMaps = "Maps/";

        public static string CreatePath(params string[] args)
        {
            string pathCreated = "";

            for (int i = 0; i < args.Length; i++)
                pathCreated += args[i].ToString();

            return pathCreated;
        }

    }
}
