using javax.print.attribute.standard;
using Microsoft.AspNetCore.SignalR.Client;
using OOP_Bomberman_client_graphics_v1.SignalR;
using sun.swing;
using System.Numerics;
using System.Security.AccessControl;
using System.Threading.Channels;
using System.Windows.Forms.Design;

namespace OOP_Bomberman_client_graphics_v1
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
        private const string EXPLOSIVE_IMAGE = "da_bomb.png";

        private Vector2 GTI = new Vector2(2, 2); // Ground Tile Index = const how?
        private Vector2 MAP_SIZE = new Vector2(10, 10); // const how?

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
        public void AddPlayer (string uuid, int X, int Y)
        {
            players.Add(uuid, new Character(new Vector2(X,Y), gameMap.TileSize,
                                            collider, characterImages[10, 4],
                                            new Bitmap(Path.Create(Path.FolderAssets, Path.FolderTextures, Path.FolderSprites, Path.FolderExplosives, EXPLOSIVE_IMAGE))));
        }
        public void UpdatePostion(string uuid, int X, int Y)
        {
            Character p;
            if (!players.TryGetValue(uuid, out p))
            {
                return;
            }
            p.Move(new Vector2(X,Y));
        }
        private void GameView_Load(object sender, EventArgs e)
        {
            this.DoubleBuffered = true;
            this.Paint += new PaintEventHandler(OnPaint);
        }

        private void Startup(List<int> GameSeed)
        {
            int index = 0;
            string filepath = Path.Create(Path.FolderAssets, Path.FolderTextures, Path.FolderSpritesheets, MAP_SPRITESHEET);
            Bitmap mapSpritesheet = new Bitmap(filepath);
            mapTileImages = Spritesheet.ExtractAll(mapSpritesheet, new Vector2(32, 32));

            filepath = Path.Create(Path.FolderAssets, Path.FolderTextures, Path.FolderSpritesheets, FNAF_CHARACTERS_SPRITESHEET);
            Bitmap charactersSpritesheet = new Bitmap(filepath);
            characterImages = Spritesheet.ExtractAll(charactersSpritesheet, new Vector2(32, 32));

            gameMap = new Map(MAP_SIZE.X, MAP_SIZE.Y, this.ClientSize.Width, this.ClientSize.Height);
            for (int y = 1; y < MAP_SIZE.Y - 1; y++)
            {
                for (int x = 1; x < MAP_SIZE.X - 1; x++)
                {
                    gameMap.SetTile(x, y, mapTileImages[GTI.X, GTI.Y]);

                    GameObject go = new EmptyGameObject();
                    int isEmpty = GameSeed[index];
                    index++;
                    if (isEmpty == 0)
                    {
                        int rndIndex = GameSeed[index];
                        index++;
                        go = gameMap.CreateScaledGameObject(x, y, characterImages[rndIndex * 3, 0]);
                    }

                    gameMap.Tiles[x, y].GameObject = go;
                }
            }

            filepath = Path.Create(Path.FolderAssets, Path.FolderTextures, Path.FolderSprites, Path.FolderExplodables, EXPLODABLE_IMAGE);
            Bitmap explodableImage = new Bitmap(filepath);
            filepath = Path.Create(Path.FolderAssets, Path.FolderTextures, Path.FolderSprites, Path.FolderWalls, WALL_IMAGE);
            Bitmap wallImage = new Bitmap(filepath);

            for (int y = 1; y < MAP_SIZE.Y - 1; y++)
            {
                for (int x = 1; x < MAP_SIZE.X - 1; x++)
                {
                    if (!(gameMap.Tiles[x, y].GameObject is EmptyGameObject))
                        continue;

                    GameObject go = new EmptyGameObject();
                    
                    int isEmpty = GameSeed[index];
                    index++;
                    if (isEmpty >= 7 && isEmpty <= 8)
                        go = gameMap.CreateScaledGameObject(x, y, explodableImage);
                    else if (isEmpty >= 9)
                        go = gameMap.CreateScaledGameObject(x, y, wallImage);

                    gameMap.Tiles[x, y].GameObject = go;
                }
            }

            filepath = Path.Create(Path.FolderAssets, Path.FolderTextures, Path.FolderSprites, Path.FolderWalls, OUTER_WALL_IMAGE);
            Bitmap outerWallImage = new Bitmap(filepath);

            for (int i = 0; i < MAP_SIZE.X; i++)
            {
                gameMap.SetTile(i, 0, mapTileImages[GTI.X, GTI.Y]);
                gameMap.Tiles[i, 0].GameObject = gameMap.CreateScaledGameObject(i, 0, outerWallImage);

                gameMap.SetTile(i, MAP_SIZE.Y - 1, mapTileImages[GTI.X, GTI.Y]);
                gameMap.Tiles[i, MAP_SIZE.Y - 1].GameObject = gameMap.CreateScaledGameObject(i, MAP_SIZE.Y - 1, outerWallImage);
            }

            for (int i = 1; i < MAP_SIZE.Y - 1; i++)
            {
                gameMap.SetTile(0, i, mapTileImages[GTI.X, GTI.Y]);
                gameMap.Tiles[0, i].GameObject = gameMap.CreateScaledGameObject(0, i, outerWallImage);

                gameMap.SetTile(MAP_SIZE.X - 1, i, mapTileImages[GTI.X, GTI.Y]);
                gameMap.Tiles[MAP_SIZE.X - 1, i].GameObject = gameMap.CreateScaledGameObject(MAP_SIZE.X - 1, i, outerWallImage);
            }

            filepath = Path.Create(Path.FolderAssets, Path.FolderTextures, Path.FolderSprites, Path.FolderExplosives, EXPLOSIVE_IMAGE);
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
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            Graphics.DrawMap(gameMap, e);
            Graphics.DrawGameObject(player, e);
            foreach (KeyValuePair<string, Character> p in players)
            {
                Graphics.DrawGameObject(p.Value, e);
            }
            if (DRAW_COLLIDERS)
            {
                Graphics.DrawColliders(gameMap, COLLIDERS_COLOR, COLLIDERS_WIDTH, e);
                Graphics.DrawCollider(player, COLLIDERS_COLOR, COLLIDERS_WIDTH, e);
                foreach (KeyValuePair<string, Character> p in players)
                {
                    Graphics.DrawCollider(p.Value, COLLIDERS_COLOR, COLLIDERS_WIDTH, e);
                }
            }
        }
        private void OnTick(object sender, EventArgs e)
        {
            InputHandler.HandleKey(keyPressed, player, gameMap, Con);
            // Debug.LogLine(player.ToString(), "\n");

            this.Refresh();
        }

        private void OnKeyDown(object sender, KeyEventArgs e) // RichTextBox bust be disabled
        {
            keyPressed = e.KeyCode;
            // Debug.LogLine("F " + e.KeyCode.ToString());
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == keyPressed)
                keyPressed = Keys.None;
            // Debug.LogLine("F " + Keys.None.ToString());
        }
    }
}