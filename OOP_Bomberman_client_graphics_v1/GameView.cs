using Microsoft.AspNetCore.SignalR.Client;

using client_graphics.SignalR;
using Utils.Math;
using Utils.Helpers;
using Utils.GUIElements;
using Utils.GameLogic;
using Utils.AbstractFactory;
using Utils.GameObjects.Animates;
using Utils.Observer;
using client_graphics.Command;
using Utils.Map;
using Utils.GameObjects.Explosives;
using Utils.GameObjects;
using Utils.Builder;
using System.Diagnostics;
using java.util;

namespace client_graphics
{
    public partial class GameView : Form
    {
        private readonly bool DEBUGGER_ENABLED = false;

        private readonly bool DRAW_COLLIDERS = false;
        private readonly Color DEFAULT_COLLIDERS_COLOR = Color.LimeGreen;
        private readonly Color COLLIDING_COLLIDERS_COLOR = Color.Red;
        private readonly float COLLIDERS_WIDTH = 2;

        private GameMap gameMap;
        private Player player;
        private Vector4 collider;
        private InputStack inputStack;
        private Bitmap characterImage;
        private CommandController commandController;
        private LookupTable collisions;

        public Subject subject { get; set; }
        private GUI gui;

        public List<int> Maps { get; set; }
        public SignalRConnection Con { get; set; }
        private Dictionary<string, Player> players = new Dictionary<string, Player>();
        private List<int> GameSeed;

        private ILevelFactory levelFactory;

        public Vector2 MapSize;

        public GameView()
        {

        }
        private void GameView_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
            this.DoubleBuffered = true;
            this.Paint += new PaintEventHandler(OnPaint);
        }
        public void GameStartUp(List<int> GameSeed)
        {
            levelFactory = new Level2Factory();
            MapSize = new Vector2(14, 14);
            InitializeComponent();
            Startup(GameSeed);

            Level1Button.Enabled = true;
            Level2Button.Enabled = false;
            Level3Button.Enabled = true;

            Debug.Set(ConsoleTextBox);
            Debug.Enabled(false);
        }
        private void Startup(List<int> gameSeed)
        {
            GameSeed = gameSeed;

            inputStack = new InputStack();
            collisions = new LookupTable();

            Vector2 position;

            gui = GameInitializer.CreateGUI(GameSettings.GUIPosition, GameSettings.GUISize, GameSettings.GUIFontColor, GameSettings.GUIFontSize);
            gameMap = GameInitializer.CreateMap(levelFactory, MapSize, Vector2.FromSize(ClientSize), GameSeed, GameSettings.GroundSpritesheetIndex);
            subject = new Subject();
            if (players.Count == 2)
                position = new Vector2(MapSize.X - 1, MapSize.Y - 1) * gameMap.TileSize;
            else
                position = new Vector2(1, 1) * gameMap.TileSize;

            player = GameInitializer.CreatePlayer(levelFactory, gameMap, position, GameSettings.PlayerSpritesheetIndex, subject);

            commandController = new CommandController();

            Con.Connection.InvokeAsync("JoinGame", position.X, position.Y);

            collider = player.Collider;
            characterImage = player.Image;
        }

        public void AddPlayer(string uuid, int x, int y) 
        {
            Vector2 index = new Vector2(x, y) / gameMap.TileSize;
            Explosive explosive = levelFactory.CreateExplosive(gameMap, index);
            players.Add(uuid, new Player(new Vector2(x, y), gameMap.TileSize, collider, characterImage, explosive, subject));

            Level1Button.Enabled = false;
            Level2Button.Enabled = false;
            Level3Button.Enabled = false;
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

        public void TeleportPlayer(string uuid, int localX, int localY)
        {
            Player p;
            if (!players.TryGetValue(uuid, out p))
                return;
            p.Teleport(new Vector2(localX, localY));
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            Graphics.DrawMap(gameMap, player, players.Values.ToList(), e);
            // Graphics.DrawGameObject(player, e);

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

        int repeats = 0;
        Keys commandKey = Input.KeyInteract;
        Stopwatch timer = new Stopwatch();
        bool drawTest = false;
        private void OnTick(object sender, EventArgs e)
        {
            var currentCoordinates = player.WorldPosition;

            GameLogic.UpdateExplosives(player, gameMap);
            GameLogic.UpdateFires(gameMap, levelFactory);

            collisions = GamePhysics.GetCollisions(player, gameMap);

            if (currentCoordinates != player.WorldPosition)
                Con.Connection.InvokeAsync("Teleport", player.LocalPosition.X, player.LocalPosition.Y, player.WorldPosition.X, player.WorldPosition.Y);

            GameLogic.ApplyEffects(player, gameMap, collisions.GameObjects);
            GameLogic.UpdateGUI(player, gui);

            ButtonClick(inputStack.Peek());
            if (repeats == 0 && drawTest)
            {
                timer.Stop();
                inputStack.Remove(commandKey);
                Debug.Enabled(true);
                Debug.LogLine(timer.Elapsed.ToString());
                drawTest = false;
            }
            if (repeats > 0) repeats--;

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

        private void Level1Button_MouseClick(object sender, MouseEventArgs e)
        {
            levelFactory = new Level1Factory(); 
            GameSeed.Clear();
            Con.Connection.InvokeAsync("MapSeed", 10, 10);
            MapSize = new Vector2(10, 10);
            Con.Connection.On<List<int>>("GenMap", (seed) =>
            {
                GameSeed = seed;
            });
            Startup(GameSeed);
            Level1Button.Enabled = false;
            Level2Button.Enabled = true;
            Level3Button.Enabled = true;
        }

        private void Level2Button_MouseClick(object sender, MouseEventArgs e)
        {
            levelFactory = new Level2Factory(); 
            GameSeed.Clear();
            Con.Connection.InvokeAsync("MapSeed", 14, 14);
            MapSize = new Vector2(14, 14);
            Con.Connection.On<List<int>>("GenMap", (seed) =>
            {
                GameSeed = seed;
            });
            Startup(GameSeed);
            Level1Button.Enabled = true;
            Level2Button.Enabled = false;
            Level3Button.Enabled = true;
        }

        private void Level3Button_MouseClick(object sender, MouseEventArgs e)
        {
            levelFactory = new Level3Factory();
            GameSeed.Clear();
            Con.Connection.InvokeAsync("MapSeed", 24, 24);
            MapSize = new Vector2(24, 24);
            Con.Connection.On<List<int>>("GenMap", (seed) =>
            {
                GameSeed = seed;
            });
            Startup(GameSeed);
            Level1Button.Enabled = true;
            Level2Button.Enabled = true;
            Level3Button.Enabled = false;
        }

        //TODO: convert to commands from the design pattern
        private void ButtonClick(Keys key)
        {
            if (key == Input.KeyUp)
            {
                if (IsDummyColliding(Direction.Up, player, gameMap))
                    return;
                commandController.ExecuteCommand(new UpCommand(player, Con));
            }
            else if (key == Input.KeyDown)
            {
                if (IsDummyColliding(Direction.Down, player, gameMap))
                    return;
                commandController.ExecuteCommand(new DownCommand(player, Con));
            }
            else if (key == Input.KeyRight)
            {
                if (IsDummyColliding(Direction.Right, player, gameMap))
                    return;
                commandController.ExecuteCommand(new RightCommand(player, Con));
            }
            else if (key == Input.KeyLeft)
            {
                if (IsDummyColliding(Direction.Left, player, gameMap))
                    return;
                commandController.ExecuteCommand(new LeftCommand(player, Con));
            }
            else if (key == Input.KeyBomb)
            {
                commandController.ExecuteCommand(new PlaceBombCommand(gameMap, player, Con, levelFactory));
            }
        }

        private static bool IsDummyColliding(Vector2 direction, Player player, GameMap gameMap)
        {
            LookupTable playerCollisions = GamePhysics.GetCollisions(player, gameMap);
            bool playerIsOnExplosive = playerCollisions.Has<Explosive>();

            Player dummy = (Player)player.Clone();
            dummy.Move(direction);
            LookupTable dummyCollisions = GamePhysics.GetCollisions(dummy, gameMap);
            int dummyCollisionCount = dummyCollisions.Get<SolidGameObject>().Count;

            if (playerIsOnExplosive && dummyCollisionCount >= 2 || !playerIsOnExplosive && dummyCollisionCount >= 1)
                return true;

            return false;
        }

        int test = 1;
        private void button1_Click(object sender, EventArgs e)
        {
            Debug.Enabled(true);
            int num = 0;
            DateTime end = DateTime.Now.AddSeconds(5);
            while(DateTime.Now < end)
            {
                ButtonClick(Input.KeyRight);
                num++;
            }
            Debug.LogLine(test, "Operations:", num);
            test++;
        }

        private void DrawTest_Click(object sender, EventArgs e)
        {
            commandKey = Input.KeyRight;
            inputStack.Push(commandKey);
            repeats = 10;
            drawTest = true;
            timer.Start();
        }
    }
}