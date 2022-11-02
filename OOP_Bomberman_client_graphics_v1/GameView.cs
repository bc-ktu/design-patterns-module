using Microsoft.AspNetCore.SignalR.Client;

using client_graphics.SignalR;
using Utils.Math;
using Utils.Helpers;
using Utils.GUIElements;
using Utils.GameLogic;
using Utils.AbstractFactory;
using Utils.GameObjects.Animates;
using Utils.Observer;
using Utils.Map;

namespace client_graphics
{
    public partial class GameView : Form
    {
        private readonly bool DRAW_COLLIDERS = true;
        private readonly Color DEFAULT_COLLIDERS_COLOR = Color.LimeGreen;
        private readonly Color COLLIDING_COLLIDERS_COLOR = Color.Red;
        private readonly float COLLIDERS_WIDTH = 2;

        private GameMap gameMap;
        private Player player;
        private Vector4 collider;
        private InputStack inputStack; 
        private Bitmap characterImage;
        private LookupTable collisions;

        public Subject subject { get; set; }
        private GUI gui;

        public List<int> Maps { get; set; }
        public SignalRConnection Con { get; set; }
        private Dictionary<string, Player> players = new Dictionary<string, Player>();

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
            players.Add(uuid, new Player(new Vector2(x, y), gameMap.TileSize, collider, characterImage, explosiveImage, fireImage, subject));
        }

        public void UpdatePosition(string uuid, int X, int Y, int speedMod, int speed)
        {
            Player p;
            if (!players.TryGetValue(uuid, out p))
                return;
            p.SpeedModifier = speedMod;
            p.SetMoveSpeed(speed);
            p.Move(new Vector2(X, Y));
        }

        private void GameView_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
            this.DoubleBuffered = true;
            this.Paint += new PaintEventHandler(OnPaint);
        }

        private void Startup(List<int> GameSeed)
        {
            levelFactory = new Level1Factory();

            inputStack = new InputStack();
            collisions = new LookupTable();

            gui = GameInitializer.CreateGUI(GameSettings.GUIPosition, GameSettings.GUISize, GameSettings.GUIFontColor, GameSettings.GUIFontSize);
            gameMap = GameInitializer.CreateMap(levelFactory, GameSettings.MapSize, Vector2.FromSize(ClientSize), GameSeed, GameSettings.GroundSpritesheetIndex);
            subject = new Subject();
            player = GameInitializer.CreatePlayer(levelFactory, gameMap, GameSettings.PlayerSpritesheetIndex, subject);

            string filepath = Pather.Create(Pather.FolderAssets, Pather.FolderTextures, Pather.FolderSprites, Pather.FolderPowerups, Pather.SpeedPowerupImage);
            Bitmap powerupImage = new Bitmap(filepath);
            GameLogic.GeneratePowerups(levelFactory, gameMap, 14, powerupImage);

            Vector2 position = gameMap.ViewSize / 2;
            Con.Connection.InvokeAsync("JoinGame", position.X, position.Y);

            collider = player.Collider;
            characterImage = player.Image;
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            Graphics.DrawMap(gameMap, e);
            Graphics.DrawGameObject(player, e);

            foreach (KeyValuePair<string, Player> p in players)
                Graphics.DrawGameObject(p.Value, e);

            if (DRAW_COLLIDERS)
            {
                Graphics.DrawColliders(gameMap, collisions, DEFAULT_COLLIDERS_COLOR, COLLIDING_COLLIDERS_COLOR, COLLIDERS_WIDTH, e);
                Graphics.DrawCollider(player, DEFAULT_COLLIDERS_COLOR, COLLIDERS_WIDTH, e);
                foreach (KeyValuePair<string, Player> p in players)
                    Graphics.DrawCollider(p.Value, DEFAULT_COLLIDERS_COLOR, COLLIDERS_WIDTH, e);
            }

            Graphics.DrawGUI(gui, e);
        }

        private void OnTick(object sender, EventArgs e)
        {
            GameLogic.UpdateExplosives(player, gameMap);
            GameLogic.UpdateFires(gameMap, levelFactory);

            collisions = GamePhysics.GetCollisions(player, gameMap);

            GameLogic.ApplyEffects(player, gameMap, collisions.GameObjects);
            GameLogic.UpdateGUI(player, gui);

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