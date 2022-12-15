using client_graphics.AbstractFactory;
using client_graphics.Command;
using client_graphics.GameLogic;
using client_graphics.GameObjects;
using client_graphics.GameObjects.Animates;
using client_graphics.GameObjects.Explosives;
using client_graphics.Helpers;
using client_graphics.Interpreter;
using client_graphics.Map;
using client_graphics.SignalR;
using client_graphics.State;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using Utils.GUIElements;
using Utils.Helpers;
using Utils.Math;
using Utils.Observer;

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

        private GameState gameState;

        public List<int> Maps { get; set; }
        public SignalRConnection Con { get; set; }
        private Dictionary<string, Player> players = new Dictionary<string, Player>();
        private List<int> GameSeed;

        private ILevelFactory levelFactory;

        public Vector2 MapSize;

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
            bool level1 = true;
            bool level2 = true;
            bool level3 = true;
            switch (GameSeed[0])
            {
                case 1:
                    levelFactory = new Level1Factory();
                    MapSize = new Vector2(10, 10);
                    level1 = false;
                    break;
                case 2:
                    levelFactory = new Level2Factory();
                    MapSize = new Vector2(14, 14);
                    level2 = false;
                    break;
                case 3:
                    levelFactory = new Level3Factory();
                    MapSize = new Vector2(24, 24);
                    level3 = false;
                    break;
                default:
                    levelFactory = new Level2Factory();
                    MapSize = new Vector2(14, 14);
                    level2 = false;
                    break;
            }
            InitializeComponent();
            Startup(GameSeed);

            Level1Button.Enabled = level1;
            Level2Button.Enabled = level2;
            Level3Button.Enabled = level3;

            Debug.Set(ConsoleTextBox);
            Debug.Enable(false);
        }
        private void Startup(List<int> gameSeed)
        {
            GameSeed = gameSeed.GetRange(1, gameSeed.Count - 1);

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
            gameState = new GameState(new WaitingGameState(), this);

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

            gameState.UpdateGameState();
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
                ConsoleTextBox.ReadOnly = !CursorOnTextBox();
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

            GameLogic.GameLogic.UpdateExplosives(player, gameMap);
            GameLogic.GameLogic.UpdateFires(gameMap, levelFactory);

            collisions = GamePhysics.GetCollisions(player, gameMap);

            if (currentCoordinates != player.WorldPosition)
                Con.Connection.InvokeAsync("Teleport", player.LocalPosition.X, player.LocalPosition.Y, player.WorldPosition.X, player.WorldPosition.Y);


            string damageValue = player.Explosive.Fire.Damage.ToString();
            string healthValue = player.Health.ToString();
            GameLogic.GameLogic.ApplyEffects(player, gameMap, collisions.GameObjects);
            if(player.Health.ToString() != healthValue || player.Explosive.Fire.Damage.ToString() != damageValue)
            {
                Con.Connection.InvokeAsync("ChangeStats", player.Health, player.Explosive.Fire.Damage);
            }
            GameLogic.GameLogic.UpdateGUI(player, gui);

            if (repeats == 0) inputZero = true;
            else if (repeats > 0)
            {
                consoleCommand = true;
                repeats--;
            }
            if (!inputZero && consoleCommand || !consoleCommand) gameState.MoveRequest(consoleCommand, inputStack.Peek()); //ButtonClick(inputStack.Peek(), consoleCommand);
            inputZero = false;
            if (consoleCommand && repeats == 0) inputStack.Remove(commandKey);

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
            Con.Connection.InvokeAsync("MapSeed", 10, 10, 1);
            MapSize = new Vector2(10, 10);
            Con.Connection.On<List<int>>("RegenMap", (seed) =>
            {
                Startup(seed);
            });
            Level1Button.Enabled = false;
            Level2Button.Enabled = true;
            Level3Button.Enabled = true;
        }

        private void Level2Button_MouseClick(object sender, MouseEventArgs e)
        {
            levelFactory = new Level2Factory();
            GameSeed.Clear();
            Con.Connection.InvokeAsync("MapSeed", 14, 14, 2);
            MapSize = new Vector2(14, 14);
            Con.Connection.On<List<int>>("RegenMap", (seed) =>
            {
                Startup(seed);
            });
            Level1Button.Enabled = true;
            Level2Button.Enabled = false;
            Level3Button.Enabled = true;
        }

        private void Level3Button_MouseClick(object sender, MouseEventArgs e)
        {
            levelFactory = new Level3Factory();
            GameSeed.Clear();
            Con.Connection.InvokeAsync("MapSeed", 24, 24, 3);
            MapSize = new Vector2(24, 24);
            Con.Connection.On<List<int>>("RegenMap", (seed) =>
            {
                Startup(seed);
            });
            Level1Button.Enabled = true;
            Level2Button.Enabled = true;
            Level3Button.Enabled = false;
        }

        public void ButtonClick(Keys key, bool ignoreCursor)
        {
            bool cursorOnTextBox = !ignoreCursor && CursorOnTextBox();
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
        // TODO: get other p
        public void BombPlaced(int fireDamage, int x, int y)
        {
            Vector2 position = new Vector2(x, y);
            Vector2 index = position / gameMap.TileSize;
            Explosive explosive = (Explosive)this.player.Explosive.Clone();
            explosive.Fire.Damage = fireDamage;
            explosive.Teleport(position);
            explosive.StartCountdown();

            gameMap[index].GameObjects.Add(explosive);
            gameMap.ExplosivesLookupTable.Add(index, explosive);
        }
        public void UpdateOtherPlayerStats(string uuid, int health, int damage)
        {
            players[uuid].Health = health;
            players[uuid].Explosive.Fire.Damage = damage;
        }

    }
}