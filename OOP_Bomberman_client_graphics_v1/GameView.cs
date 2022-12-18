using Microsoft.AspNetCore.SignalR.Client;
using System.Diagnostics;

using client_graphics.SignalR;
using client_graphics.Command;
using client_graphics.Interpreter;
using Utils.Math;
using Utils.Helpers;
using Utils.GUIElements;
using Utils.GameLogic;
using Utils.AbstractFactory;
using Utils.GameObjects.Animates;
using Utils.Observer;
using Utils.Map;
using Utils.GameObjects.Explosives;
using Utils.GameObjects;
using Utils.Flyweight;
using client_graphics.Chain_of_responsibility;
using client_graphics.Manager;

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
        private ImageFlyweight characterImage;
        private CommandController commandController;
        private LookupTable collisions;

        public GameState GameState { get; private set; }

        public Subject subject { get; set; }
        private GUI gui;

        public List<int> Maps { get; set; }
        public SignalRConnection Con { get; set; }
        private Dictionary<string, Player> players = new Dictionary<string, Player>();
        private List<int> GameSeed;

        private ILevelFactory levelFactory;

        public Vector2 MapSize;

        private Process currentProcess;
        int repeats = 0;
        Keys commandKey = Input.KeyInteract;

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
            GameState = GameState.Instance;
            levelFactory = new Level2Factory();
            MapSize = new Vector2(14, 14);
            InitializeComponent();
            Startup(GameSeed);

            Level1Button.Enabled = true;
            Level2Button.Enabled = false;
            Level3Button.Enabled = true;

            Debug.Set(ConsoleTextBox);
            Debug.Enable(DEBUGGER_ENABLED);

            GameState.Logger.Log(MessageType.Network, "Gameloop started!");
        }

        private void Startup(List<int> gameSeed)
        {
            currentProcess = Process.GetCurrentProcess();
            IO.ClearFile(Pather.Create(Pather.FolderAssets, Pather.FolderTextFiles, Pather.MemoryUsageDiagnostics));
            IO.ClearFile(Pather.Create(Pather.FolderAssets, Pather.FolderTextFiles, Pather.TimeDiagnostics));

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

        private void OnTick(object sender, EventArgs e)
        {
            var currentCoordinates = player.WorldPosition;
            bool consoleCommand = false;
            bool inputZero = false;
            Debug.Enable(ConsoleCheck.Checked);
            if (Debug.Enabled)
            {
                ConsoleTextBox.ReadOnly = !CursorOnTextBox() ? true : false;
                if (ConsoleTextBox.Text.EndsWith("\n"))
                {
                    string command = ConsoleTextBox.Text.TrimEnd('\n');
                    Context context = new Context(command.Trim());
                    List<Expression> tree = new List<Expression>();
                    tree.Add(new MoveUpExpression());
                    tree.Add(new MoveDownExpression());
                    tree.Add(new MoveRightExpression());
                    tree.Add(new MoveLeftExpression());
                    tree.Add(new PlaceBombExpression());
                    foreach (Expression exp in tree)
                    {
                        consoleCommand = exp.Interpret(context, inputStack);
                        if (consoleCommand)
                        {
                            commandKey = exp.Key();
                            string[] splits = context.Input.Split(' ');
                            repeats = splits.Length > 2 ? Int32.Parse(splits[2]) : 1;
                            if (repeats > exp.Limit()) repeats = 0;
                            if (repeats < 0) repeats = 0;
                            break;
                        }
                    }
                    ConsoleTextBox.Clear();
                }
            }

            GameLogic.UpdateExplosives(player, gameMap);
            GameLogic.UpdateFires(gameMap, levelFactory);

            collisions = GamePhysics.GetCollisions(player, gameMap);

            if (currentCoordinates != player.WorldPosition)
                Con.Connection.InvokeAsync("Teleport", player.LocalPosition.X, player.LocalPosition.Y, player.WorldPosition.X, player.WorldPosition.Y);

            GameLogic.ApplyEffects(player, gameMap, collisions.GameObjects);
            GameLogic.UpdateGUI(player, gui);

            if (repeats == 0) inputZero = true;
            else if (repeats > 0)
            {
                consoleCommand = true;
                repeats--;
            }
            if(!inputZero && consoleCommand || !consoleCommand) ButtonClick(inputStack.Peek(), consoleCommand);
            inputZero = false;
            if (consoleCommand && repeats == 0) inputStack.Remove(commandKey);

            if (GameSettings.CalculateMemoryDiagnostics)
            {
                long usedMemory = currentProcess.PrivateMemorySize64;
                IO.WriteToFile(Pather.Create(Pather.FolderAssets, Pather.FolderTextFiles, Pather.MemoryUsageDiagnostics), (usedMemory >> 20).ToString());
            }

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
            if (GameSettings.CalculateTimeDiagnostics)
                IO.ClearFile(Pather.Create(Pather.FolderAssets, Pather.FolderTextFiles, Pather.TimeDiagnostics));

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
            if (GameSettings.CalculateTimeDiagnostics)
                IO.ClearFile(Pather.Create(Pather.FolderAssets, Pather.FolderTextFiles, Pather.TimeDiagnostics));

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
            if (GameSettings.CalculateTimeDiagnostics)
                IO.ClearFile(Pather.Create(Pather.FolderAssets, Pather.FolderTextFiles, Pather.TimeDiagnostics));

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

        private void ButtonClick(Keys key, bool ignoreCursor)
        {
            bool cursorOnTextBox = ignoreCursor ? false : CursorOnTextBox();
            if (!cursorOnTextBox)
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

        private bool CursorOnTextBox()
        {
            var cursor = this.PointToClient(Cursor.Position);
            int topLeftX = ConsoleTextBox.Location.X;
            int topLeftY = ConsoleTextBox.Location.Y;
            //Point topLeft = new Point(topLeftX, topLeftY);
            int bottomRightX = ConsoleTextBox.Location.X + ConsoleTextBox.Width;
            int bottomRightY = ConsoleTextBox.Location.Y + ConsoleTextBox.Height;
            //Point bottomRight = new Point(bottomRightX, bottomRightY);
            if (cursor.X >= topLeftX && cursor.X <= bottomRightX && cursor.Y >= topLeftY && cursor.Y <= bottomRightY && Debug.Enabled) return true;
            return false;
        }
    }
}