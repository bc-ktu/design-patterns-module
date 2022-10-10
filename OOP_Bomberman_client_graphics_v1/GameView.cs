using javax.print.attribute.standard;
using sun.swing;
using System.Security.AccessControl;
using System.Threading.Channels;
using System.Windows.Forms.Design;
using Microsoft.AspNetCore.SignalR.Client;

using client_graphics.SignalR;
using Utils.GameObjects;
using Utils.Math;
using Utils.Helpers;
using Utils.GUIElements;
using System.Drawing.Text;

namespace client_graphics
{
    public partial class GameView : Form
    {
        private const bool DRAW_COLLIDERS = false;
        private Color COLLIDERS_COLOR = Color.LimeGreen;
        private const float COLLIDERS_WIDTH = 2;

        private const string MAP_SPRITESHEET = "TX_Tileset_Grass.png";
        private const string FNAF_CHARACTERS_SPRITESHEET = "fnaf_characters.png";

        private const string EXPLODABLE_IMAGE = "Explodable000.png";
        private const string WALL_IMAGE = "Wall000.png";
        private const string OUTER_WALL_IMAGE = "Wall001.png";
        private const string EXPLOSIVE_IMAGE = "bomb32ign.png";

        private const string GUI_FRAME_IMAGE = "panel001.png";
        private const string GUI_HEALTH_ICON = "6-pixel-heart-4.png";
        private const string GUI_SPEED_ICON = "boots_01b.png";
        private const string GUI_CAPACITY_ICON = "bag.png";
        private const string GUI_RANGE_ICON = "arrow_01f.png";
        private const string GUI_DAMAGE_ICON = "fire_skull.png";
        private const string GUI_FONT = "Minecraft.ttf";

        private Vector2 GTI = new Vector2(2, 2); // Ground Tile Index = const how?
        private Vector2 MAP_SIZE = new Vector2(10, 10); // const how?

        private Vector2 GUI_SIZE = new Vector2(5 * 50, 80);
        private Brush GUI_BRUSH = Brushes.White;
        private const int GUI_FONT_SIZE = 32;

        private Bitmap[,] mapTileImages;
        private Map gameMap;

        private Bitmap[,] characterImages;
        private Vector4 collider;

        private Keys keyPressed;

        private Character player;
        private Dictionary<string, Character> players = new Dictionary<string, Character>();
        //private SignalRConnection Con;
        public List<int> Maps { get; set; }
        public SignalRConnection Con { get; set; }

        private GUI gui;
        private Font guiFont;
        private PrivateFontCollection fontCollection = new PrivateFontCollection();

        public GameView()
        {

        }

        public void GameStartUp(List<int> GameSeed)
        {
            InitializeComponent();

            Debug.Set(ConsoleTextBox);
            Debug.Enabled(true);
            Debug.LogLine("Hello World!");
            Debug.LogLine(this.ClientSize.ToString());
            Startup(GameSeed);
        }

        public void AddPlayer (string uuid, int x, int y)
        {
            string path = Filepath.Create(Filepath.FolderAssets, Filepath.FolderTextures, Filepath.FolderSprites, Filepath.FolderExplosives, EXPLOSIVE_IMAGE);
            Bitmap image = new Bitmap(path);
            players.Add(uuid, new Character(new Vector2(x, y), gameMap.TileSize, collider, characterImages[10, 4], image));
        }

        public void UpdatePostion(string uuid, int X, int Y)
        {
            Character p;
            if (!players.TryGetValue(uuid, out p))
                return;

            p.Move(new Vector2(X,Y));
        }

        private void GameView_Load(object sender, EventArgs e)
        {
            this.DoubleBuffered = true;
            this.Paint += new PaintEventHandler(OnPaint);

            this.KeyPreview = true;
        }

        private void Startup(List<int> GameSeed)
        {
            int index = 0;
            string filepath = Filepath.Create(Filepath.FolderAssets, Filepath.FolderTextures, Filepath.FolderSpritesheets, MAP_SPRITESHEET);
            Bitmap mapSpritesheet = new Bitmap(filepath);
            mapTileImages = Spritesheet.ExtractAll(mapSpritesheet, new Vector2(32, 32));

            filepath = Filepath.Create(Filepath.FolderAssets, Filepath.FolderTextures, Filepath.FolderSpritesheets, FNAF_CHARACTERS_SPRITESHEET);
            Bitmap charactersSpritesheet = new Bitmap(filepath);
            characterImages = Spritesheet.ExtractAll(charactersSpritesheet, new Vector2(32, 32));

            gameMap = new Map(MAP_SIZE.X, MAP_SIZE.Y, this.ClientSize.Width, this.ClientSize.Height);
            //for (int y = 1; y < MAP_SIZE.Y - 1; y++)
            //{
            //    for (int x = 1; x < MAP_SIZE.X - 1; x++)
            //    {
            //        gameMap.SetTile(x, y, mapTileImages[GTI.X, GTI.Y]);

            //        GameObject go = new EmptyGameObject();
            //        int isEmpty = GameSeed[index];
            //        index++;
            //        if (isEmpty == 0)
            //        {
            //            int rndIndex = GameSeed[index];
            //            index++;
            //            var prm = gameMap.CreateScaledGameObjectParameters(x, y, characterImages[rndIndex * 3, 0]);
            //            go = new Character(prm.Item1, prm.Item2, prm.Item3, prm.Item4, prm.Item4);
            //        }

            //        gameMap.Tiles[x, y].GameObject = go;
            //    }
            //}

            filepath = Filepath.Create(Filepath.FolderAssets, Filepath.FolderTextures, Filepath.FolderSprites, Filepath.FolderExplodables, EXPLODABLE_IMAGE);
            Bitmap explodableImage = new Bitmap(filepath);
            filepath = Filepath.Create(Filepath.FolderAssets, Filepath.FolderTextures, Filepath.FolderSprites, Filepath.FolderWalls, WALL_IMAGE);
            Bitmap wallImage = new Bitmap(filepath);

            for (int y = 1; y < MAP_SIZE.Y - 1; y++)
            {
                for (int x = 1; x < MAP_SIZE.X - 1; x++)
                {
                    gameMap.SetTile(x, y, mapTileImages[GTI.X, GTI.Y]);
  
                    //if (gameMap.Tiles[x, y].GameObject is not EmptyGameObject)
                    //    continue;

                    GameObject go = new EmptyGameObject();
                    
                    int isEmpty = GameSeed[index];
                    index++;
                    if (isEmpty >= 7 && isEmpty <= 8) // 0.75
                    {
                        var prm = gameMap.CreateScaledGameObjectParameters(x, y, explodableImage);
                        go = new DestructableObject(prm.Item1, prm.Item2, prm.Item3, prm.Item4);
                    }
                    else if (isEmpty >= 9)
                    {
                        var prm = gameMap.CreateScaledGameObjectParameters(x, y, wallImage);
                        go = new IndestructableWall(prm.Item1, prm.Item2, prm.Item3, prm.Item4);
                    }

                    gameMap.Tiles[x, y].GameObject = go;
                }
            }

            filepath = Filepath.Create(Filepath.FolderAssets, Filepath.FolderTextures, Filepath.FolderSprites, Filepath.FolderWalls, OUTER_WALL_IMAGE);
            Bitmap outerWallImage = new Bitmap(filepath);

            for (int i = 0; i < MAP_SIZE.X; i++)
            {
                gameMap.SetTile(i, 0, mapTileImages[GTI.X, GTI.Y]);
                var prm = gameMap.CreateScaledGameObjectParameters(i, 0, outerWallImage);
                gameMap.Tiles[i, 0].GameObject = new IndestructableWall(prm.Item1, prm.Item2, prm.Item3, prm.Item4);

                gameMap.SetTile(i, MAP_SIZE.Y - 1, mapTileImages[GTI.X, GTI.Y]);
                prm = gameMap.CreateScaledGameObjectParameters(i, MAP_SIZE.Y - 1, outerWallImage);
                gameMap.Tiles[i, MAP_SIZE.Y - 1].GameObject = new IndestructableWall(prm.Item1, prm.Item2, prm.Item3, prm.Item4);
            }

            for (int i = 1; i < MAP_SIZE.Y - 1; i++)
            {
                gameMap.SetTile(0, i, mapTileImages[GTI.X, GTI.Y]);
                var prm = gameMap.CreateScaledGameObjectParameters(0, i, outerWallImage);
                gameMap.Tiles[0, i].GameObject = new IndestructableWall(prm.Item1, prm.Item2, prm.Item3, prm.Item4);

                gameMap.SetTile(MAP_SIZE.X - 1, i, mapTileImages[GTI.X, GTI.Y]);
                prm = gameMap.CreateScaledGameObjectParameters(MAP_SIZE.X - 1, i, outerWallImage);
                gameMap.Tiles[MAP_SIZE.X - 1, i].GameObject = new IndestructableWall(prm.Item1, prm.Item2, prm.Item3, prm.Item4);
            }

            filepath = Filepath.Create(Filepath.FolderAssets, Filepath.FolderTextures, Filepath.FolderSprites, Filepath.FolderExplosives, EXPLOSIVE_IMAGE);
            Bitmap explosiveImage = new Bitmap(filepath);

            Vector2 position = new Vector2(this.ClientSize.Width / 2, this.ClientSize.Height / 2);
            double colliderSize = 0.75;
            int tlx = (int)(position.X + (1 - colliderSize) * gameMap.TileSize.X);
            int tly = (int)(position.Y + (1 - colliderSize) * gameMap.TileSize.Y);
            int brx = (int)(position.X + colliderSize * gameMap.TileSize.X);
            int bry = (int)(position.Y + colliderSize * gameMap.TileSize.Y);
            collider = new Vector4(tlx, tly, brx, bry);           
            player = new Character(position, gameMap.TileSize, collider, characterImages[10, 4], explosiveImage); // maybe later add not the image, but Explosive object
            Con.Connection.InvokeAsync("JoinGame", position.X, position.Y);

            filepath = Filepath.Create(Filepath.FolderAssets, Filepath.FolderTextures, Filepath.FolderGUI, GUI_FRAME_IMAGE);
            Bitmap frameImage = new Bitmap(filepath);
            filepath = Filepath.Create(Filepath.FolderAssets, Filepath.FolderTextures, Filepath.FolderGUI, GUI_HEALTH_ICON);
            Bitmap healthIcon = new Bitmap(filepath);
            filepath = Filepath.Create(Filepath.FolderAssets, Filepath.FolderTextures, Filepath.FolderGUI, GUI_SPEED_ICON);
            Bitmap speedIcon = new Bitmap(filepath);
            filepath = Filepath.Create(Filepath.FolderAssets, Filepath.FolderTextures, Filepath.FolderGUI, GUI_CAPACITY_ICON);
            Bitmap capacityIcon = new Bitmap(filepath);
            filepath = Filepath.Create(Filepath.FolderAssets, Filepath.FolderTextures, Filepath.FolderGUI, GUI_RANGE_ICON);
            Bitmap rangeIcon = new Bitmap(filepath);
            filepath = Filepath.Create(Filepath.FolderAssets, Filepath.FolderTextures, Filepath.FolderGUI, GUI_DAMAGE_ICON);
            Bitmap damageIcon = new Bitmap(filepath);

            Vector2 guiPosition = new Vector2(0, 0);
            gui = new GUI(guiPosition, GUI_SIZE, frameImage);
            gui.SetHealthImage(healthIcon);
            gui.SetSpeedImage(speedIcon);
            gui.SetCapacityImage(capacityIcon);
            gui.SetRangeImage(rangeIcon);
            gui.SetDamageImage(damageIcon);

            filepath = Filepath.Create(Filepath.FolderAssets, Filepath.FolderFonts, GUI_FONT);
            fontCollection.AddFontFile(filepath);
            guiFont = new Font(fontCollection.Families[0].Name, GUI_FONT_SIZE);

            gui.SetHealthValue(player.Health.ToString());
            gui.SetSpeedValue(player.MovementSpeed.ToString());
            gui.SetCapacityValue(player.ExplosivesCapacity.ToString());
            gui.SetRangeValue(player.ExplosivesRange.ToString());
            gui.SetDamageValue(player.ExplosiveDamage.ToString());
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            Graphics.DrawMap(gameMap, e);
            Graphics.DrawGameObject(player, e);

            foreach (KeyValuePair<string, Character> p in players)
                Graphics.DrawGameObject(p.Value, e);

            if (DRAW_COLLIDERS)
            {
                Graphics.DrawColliders(gameMap, COLLIDERS_COLOR, COLLIDERS_WIDTH, e);
                Graphics.DrawCollider(player, COLLIDERS_COLOR, COLLIDERS_WIDTH, e);
                foreach (KeyValuePair<string, Character> p in players)
                    Graphics.DrawCollider(p.Value, COLLIDERS_COLOR, COLLIDERS_WIDTH, e);
            }

            Graphics.DrawGUI(gui, guiFont, GUI_BRUSH, e);
        }
        private void OnTick(object sender, EventArgs e)
        {
            for (int i = 0; i < gameMap.ExplosivesLookupTable.Count; i++)
            {
                Explosive explosive = gameMap.ExplosivesLookupTable.GameObjects[i] as Explosive;
                explosive.Explode(gameMap, i);
            }

            InputHandler.HandleKey(keyPressed, player, gameMap, Con);

            this.Refresh();
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            keyPressed = e.KeyCode;
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == keyPressed)
                keyPressed = Keys.None;
        }

        private void Level1Button_Click(object sender, EventArgs e)
        {

        }

        private void Level2Button_Click(object sender, EventArgs e)
        {

        }

        private void Level3Button_Click(object sender, EventArgs e)
        {

        }
    }
}