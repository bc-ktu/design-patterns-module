using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using client_graphics.Helpers;
using Utils.Helpers;
using Utils.Math;

namespace client_graphics.GameLogic
{
    public static class GameSettings
    {
        // ******************** LOGIC SETTINGS **********************

        public static Vector2 InitialPlayerDirection = Direction.Down;
        public static int InitialPlayerHealth = 7;
        public static int InitialPlayerSpeed = 14;
        public static int InitialPlayerCapacity = 3;
        public static int InitialExplosionRange = 2;
        public static int InitialExplosionDamage = 1;

        public static int InitialTimeTillExplosion = 2000;  // ms
        public static int InitialFireBurnTime = 1500;       // ms
        public static int InitialIFramesTime = 1000;        // ms

        // ******************** GUI SETTINGS **********************

        public static Vector2 GUIPosition = new Vector2(0, 0);
        public static Vector2 GUISize = new Vector2(5 * 50, 80);
        public static Brush GUIFontColor = Brushes.White;
        public static int GUIFontSize = 24;
        public static Font DecoratorFont = new Font(FontFamily.GenericSerif, 14);

        // ******************** TEXTURES SETTINGS ********************

        public static Vector2 GroundSpritesheetIndex = new Vector2(0, 0);
        public static Vector2 PlayerSpritesheetIndex = new Vector2(11, 4);
        public static Vector2 EnemySpritesheetIndex = new Vector2(11, 0);

        // ******************** PHYSICS SETTINGS **********************

        public static double ExplosiveColliderScale = 0.8;
        public static double PowerupColliderScale = 0.8;
        public static double PlayerColliderScale = 0.7;

        // ******************** PHYSICS SETTINGS **********************

        public static double Level1RangePowerupChance = 0.14;
        public static double Level2RangePowerupChance = 0.07;
        public static double Level3RangePowerupChance = 0.03;

        public static double CrateDropRate = 0.75;

    }
}
