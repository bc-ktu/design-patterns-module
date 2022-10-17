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
using Utils.AbstractFactory;

namespace client_graphics
{
    public partial class GameView : Form
    {
        private readonly bool DRAW_COLLIDERS = true;
        private readonly Color DEFAULT_COLLIDERS_COLOR = Color.LimeGreen;
        private readonly Color COLLIDING_COLLIDERS_COLOR = Color.Red;
        private readonly float COLLIDERS_WIDTH = 2;

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

        private ILevelFactory levelFactory;

        public GameView()
        {

        }

        public void GameStartUp(List<int> GameSeed)
        {
            InitializeComponent();
            Startup(GameSeed);

            Debug.Set(ConsoleTextBox);
            Debug.Enabled(false);
            Debug.LogLine("Hello World!");
        }

        public void AddPlayer (string uuid, int x, int y) // use GameInitialize.CreatePlayer() method
        {
            string filepath = Pather.Create(Pather.FolderAssets, Pather.FolderTextures, Pather.FolderSprites, Pather.FolderExplosives, Pather.ExplosiveImage);
            Bitmap explosiveImage = new Bitmap(filepath);
            filepath = Pather.Create(Pather.FolderAssets, Pather.FolderTextures, Pather.FolderGUI, Pather.GuiDamageIcon);
            Bitmap fireImage = new Bitmap(filepath);
            int px = GameSettings.PlayerSpritesheetIndex.X;
            int py = GameSettings.PlayerSpritesheetIndex.Y;
            players.Add(uuid, new Character(new Vector2(x, y), gameMap.TileSize, collider, characterImages[px, py], explosiveImage, fireImage));
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
            guiFont = new Font(pfc.Families[0].Name, GameSettings.GUIFontSize);

            gui = GameInitializer.CreateGUI(GameSettings.GUIPosition, GameSettings.GUISize);
            gameMap = GameInitializer.CreateMap(GameSettings.MapSize, Vector2.FromSize(ClientSize), GameSeed, GameSettings.GroundSpritesheetIndex);
            player = GameInitializer.CreatePlayer(gameMap, GameSettings.PlayerSpritesheetIndex);

            Vector2 position = gameMap.ViewSize / 2;
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

            Graphics.DrawGUI(gui, guiFont, GameSettings.GUIBrushColor, e);
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