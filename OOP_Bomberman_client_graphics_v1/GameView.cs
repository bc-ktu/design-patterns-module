using javax.print.attribute.standard;
using sun.swing;
using System.Security.AccessControl;
using System.Threading.Channels;
using System.Windows.Forms.Design;
using Microsoft.AspNetCore.SignalR.Client;
using System.Drawing.Text;

using client_graphics.SignalR;
using Utils.GameObjects;
using Utils.Math;
using Utils.Helpers;
using Utils.GUIElements;
using Utils.GameLogic;
using Utils.GameObjects.Animates;

namespace client_graphics
{
    public partial class GameView : Form
    {
        private const bool DRAW_COLLIDERS = true;
        private Color DEFAULT_COLLIDERS_COLOR = Color.LimeGreen;
        private Color COLLIDING_COLLIDERS_COLOR = Color.Red;
        private const float COLLIDERS_WIDTH = 2;

        private Vector2 GTI = new Vector2(2, 2); // Ground Tile Index = const how?
        private Vector2 MAP_SIZE = new Vector2(10, 10);

        private Vector2 GUI_POSITION = new Vector2(0, 0);
        private Vector2 GUI_SIZE = new Vector2(5 * 50, 80);
        private Brush GUI_BRUSH = Brushes.White;
        private const int GUI_FONT_SIZE = 32;

        private Bitmap[,] characterImages;

        private GameMap gameMap;
        private Character player;
        private Vector4 collider;
        private InputStack inputStack;
        List<Vector2> collisions;

        private GUI gui; // require font in constructor
        private Font guiFont;

        public List<int> Maps { get; set; }
        public SignalRConnection Con { get; set; }
        private Dictionary<string, Character> players = new Dictionary<string, Character>();


        public GameView()
        {

        }

        public void GameStartUp(List<int> GameSeed)
        {
            InitializeComponent();

            Debug.Set(ConsoleTextBox);
            Debug.Enabled(false);
            Debug.LogLine("Hello World!");
            Debug.LogLine(this.ClientSize.ToString());
            Startup(GameSeed);
        }

        public void AddPlayer (string uuid, int x, int y)
        {
            string filepath = Pather.Create(Pather.FolderAssets, Pather.FolderTextures, Pather.FolderSprites, Pather.FolderExplosives, Pather.ExplosiveImage);
            Bitmap explosiveImage = new Bitmap(filepath);
            filepath = Pather.Create(Pather.FolderAssets, Pather.FolderTextures, Pather.FolderGUI, Pather.GuiDamageIcon);
            Bitmap fireImage = new Bitmap(filepath);
            players.Add(uuid, new Character(new Vector2(x, y), gameMap.TileSize, collider, characterImages[10, 4], explosiveImage, fireImage));
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
            this.KeyPreview = true;
            this.DoubleBuffered = true;
            this.Paint += new PaintEventHandler(OnPaint);
        }

        private void Startup(List<int> GameSeed)
        {
            inputStack = new InputStack();
            collisions = new List<Vector2>();

            string filepath;
            PrivateFontCollection pfc = new PrivateFontCollection();

            filepath = Pather.Create(Pather.FolderAssets, Pather.FolderFonts, Pather.GuiFontFile);
            pfc.AddFontFile(filepath);
            guiFont = new Font(pfc.Families[0].Name, GUI_FONT_SIZE);

            gui = GameInitializer.CreateGUI(GUI_POSITION, GUI_SIZE);
            gameMap = GameInitializer.CreateMap(MAP_SIZE, new Vector2(this.ClientSize.Width, this.ClientSize.Height), GameSeed);

            filepath = Pather.Create(Pather.FolderAssets, Pather.FolderTextures, Pather.FolderSpritesheets, Pather.CharacterSpritesheet);
            Bitmap charactersSpritesheet = new Bitmap(filepath);
            characterImages = Spritesheet.ExtractAll(charactersSpritesheet, new Vector2(32, 32));
            filepath = Pather.Create(Pather.FolderAssets, Pather.FolderTextures, Pather.FolderSprites, Pather.FolderExplosives, Pather.ExplosiveImage);
            Bitmap explosiveImage = new Bitmap(filepath);
            filepath = Pather.Create(Pather.FolderAssets, Pather.FolderTextures, Pather.FolderGUI, Pather.GuiDamageIcon);
            Bitmap fireImage = new Bitmap(filepath);

            Vector2 position = new Vector2(this.ClientSize.Width, this.ClientSize.Height) / 2;
            double colliderSize = 0.75;
            int tlx = (int)(position.X + (1 - colliderSize) * gameMap.TileSize.X);
            int tly = (int)(position.Y + (1 - colliderSize) * gameMap.TileSize.Y);
            int brx = (int)(position.X + colliderSize * gameMap.TileSize.X);
            int bry = (int)(position.Y + colliderSize * gameMap.TileSize.Y);
            collider = new Vector4(tlx, tly, brx, bry);
            player = new Character(position, gameMap.TileSize, collider, characterImages[10, 4], explosiveImage, fireImage); // maybe later add not the image, but Explosive object
            
            Con.Connection.InvokeAsync("JoinGame", position.X, position.Y);
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            Graphics.DrawMap(gameMap, e);
            Graphics.DrawGameObject(player, e);

            foreach (KeyValuePair<string, Character> p in players)
                Graphics.DrawGameObject(p.Value, e);

            if (DRAW_COLLIDERS)
            {
                Graphics.DrawColliders(gameMap, collisions, DEFAULT_COLLIDERS_COLOR, COLLIDING_COLLIDERS_COLOR, COLLIDERS_WIDTH, e);
                Graphics.DrawCollider(player, DEFAULT_COLLIDERS_COLOR, COLLIDERS_WIDTH, e);
                foreach (KeyValuePair<string, Character> p in players)
                    Graphics.DrawCollider(p.Value, DEFAULT_COLLIDERS_COLOR, COLLIDERS_WIDTH, e);
            }

            Graphics.DrawGUI(gui, guiFont, GUI_BRUSH, e);
        }
        private void OnTick(object sender, EventArgs e)
        {
            GameLogic.UpdateLookupTables(player, gameMap);
            GameLogic.ApplyEffects(player, gameMap, collisions);
            GameLogic.UpdateGUI(player, gui);

            collisions = GamePhysics.GetCollisions(player, gameMap);

            InputHandler.HandleKey(inputStack.Peek(), player, gameMap, Con);

            this.Refresh();
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            inputStack.Push(e.KeyCode);
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            inputStack.Remove(e.KeyCode);
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