using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils.Helpers
{
    public static class Pather
    {
        // Folders
        public static readonly string FolderAssets = "Assets/";
        public static readonly string FolderTextures = "Textures/";
        public static readonly string FolderSpritesheets = "Spritesheets/";
        public static readonly string FolderSprites = "Sprites/";
        public static readonly string FolderExplodables = "Explodables/";
        public static readonly string FolderWalls = "Walls/";
        public static readonly string FolderExplosives = "Explosives/";
        public static readonly string FolderGUI = "GUI/";
        public static readonly string FolderFonts = "Fonts/";
        public static readonly string FolderPowerups = "Powerups/";
        public static readonly string FolderSoundEffects = "SoundEffects/";
        public static readonly string FolderTiles = "Tiles/";

        // Files
        public static readonly string MapSpritesheet = "TX_Tileset_Grass.png";
        public static readonly string CharacterSpritesheet = "fnaf_characters.png";

        public static readonly string PortalInTile = "po.png";
        public static readonly string PortalOutTile = "pb.png";
        public static readonly string MudTile = "sand.png";

        public static readonly string CrateImage = "Explodable000.png";
        public static readonly string ExplosiveImage = "bomb32ign.png";
        public static readonly string SpeedPowerupImage = "SpeedPowerup.png";
        public static readonly string CapacityPowerupImage = "CapacityPowerup.png";
        public static readonly string DamagePowerupImage = "DamagePowerup.png";
        public static readonly string RangePowerupImage = "RangePowerup.png";

        public static readonly string PaperWallImage = "Wall004.png";
        public static readonly string WoodenWallImage = "Wall005.png";
        public static readonly string StoneWallImage = "Wall001.png";

        public static readonly string OuterWallImage = "Wall006.png";
        public static readonly string MudTileImage = "MudTile.png";
        public static readonly string IceTileImage = "IceTile.png";
        public static readonly string PortalTileImage = "PortalTile.png";

        public static readonly string GUIFrameImage = "panel001.png";
        public static readonly string GUIHealthIcon = "6-pixel-heart-4.png";
        public static readonly string GUISpeedIcon = "boots_01b.png";
        public static readonly string GUICapacityIcon = "bag.png";
        public static readonly string GuiRangeIcon = "arrow_01f.png";
        public static readonly string GuiDamageIcon = "fire_skull.png";

        public static readonly string GuiFontFile = "Minecraft.ttf";

        public static string Create(params string[] args)
        {
            string pathCreated = "";

            for (int i = 0; i < args.Length; i++)
                pathCreated += args[i].ToString();

            return pathCreated;
        }
    }
}
