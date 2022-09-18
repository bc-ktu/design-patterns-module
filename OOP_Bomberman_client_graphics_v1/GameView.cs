using OOP_CardGame_PrototypeV1;
using System.Security.AccessControl;
using System.Windows.Forms.Design;

namespace OOP_Bomberman_client_graphics_v1
{
    public partial class GameView : Form
    {
        private const string MAP_SPRITESHEET = "TX_Tileset_Grass.png";
        private const string PINK_CHARACTER_SPRITESHEET = "2 idle.png";
        private const string GREEN_CHARACTER_SPRITESHEET = "3 idle.png";
        private const string WHITE_CHARACTER_SPRITESHEET = "7 idle.png";
        private const string PURPLE_CHARACTER_SPRITESHEET = "9 idle.png";
        private Vector2 GTI = new Vector2(2, 2); // Ground Tile Index = const how?
        private Vector2 MAP_SIZE = new Vector2(16, 16); // const how?

        private Bitmap[,] mapTileImages;
        private Map gameMap;

        public GameView()
        {
            InitializeComponent();

            Debug.Set(ConsoleTextBox);
            Debug.Enabled(true);
            Debug.LogLine("Hello World!");
            Debug.LogLine(this.ClientSize.ToString());

            Startup();
        }
        private void GameView_Load(object sender, EventArgs e)
        {
            this.DoubleBuffered = true;
            this.Paint += new PaintEventHandler(OnPaint);
        }

        private void Startup()
        {
            string filepath = Path.Create(Path.FolderAssets, Path.FolderTextures, Path.FolderSpritesheets, MAP_SPRITESHEET);
            Bitmap mapSpritesheet = new Bitmap(filepath);
            mapTileImages = Spritesheet.ExtractAll(mapSpritesheet, new Vector2(32, 32));

            Bitmap characterSpriteSheetImage;
            Bitmap[] characterImages = new Bitmap[4];
            characterSpriteSheetImage = new Bitmap(Path.Create(Path.FolderAssets, Path.FolderTextures, Path.FolderSpritesheets, PINK_CHARACTER_SPRITESHEET));
            characterImages[0] = Spritesheet.ExtractSprite(characterSpriteSheetImage, new Vector2(16, 16), new Vector2(0, 0));
            characterSpriteSheetImage = new Bitmap(Path.Create(Path.FolderAssets, Path.FolderTextures, Path.FolderSpritesheets, GREEN_CHARACTER_SPRITESHEET));
            characterImages[1] = Spritesheet.ExtractSprite(characterSpriteSheetImage, new Vector2(16, 16), new Vector2(0, 0));
            characterSpriteSheetImage = new Bitmap(Path.Create(Path.FolderAssets, Path.FolderTextures, Path.FolderSpritesheets, WHITE_CHARACTER_SPRITESHEET));
            characterImages[2] = Spritesheet.ExtractSprite(characterSpriteSheetImage, new Vector2(16, 16), new Vector2(0, 0));
            characterSpriteSheetImage = new Bitmap(Path.Create(Path.FolderAssets, Path.FolderTextures, Path.FolderSpritesheets, PURPLE_CHARACTER_SPRITESHEET));
            characterImages[3] = Spritesheet.ExtractSprite(characterSpriteSheetImage, new Vector2(16, 16), new Vector2(0, 0));

            Random rnd = new Random();
            Vector2 tileWorldSize = new Vector2(this.ClientSize.Width / MAP_SIZE.X, this.ClientSize.Height / MAP_SIZE.Y);
            gameMap = new Map(MAP_SIZE.X, MAP_SIZE.Y, this.ClientSize.Width, this.ClientSize.Height);
            for (int y = 0; y < MAP_SIZE.Y; y++)
            {
                for (int x = 0; x < MAP_SIZE.X; x++)
                {
                    gameMap.SetTile(x, y, mapTileImages[GTI.X, GTI.Y]);

                    GameObject go = new EmptyGameObject();
                    int isEmpty = rnd.Next(0, 3);
                    if (isEmpty == 1)
                    {
                        Vector2 position = new Vector2(gameMap.Tiles[x, y].LocalPosition.X, gameMap.Tiles[x, y].LocalPosition.Y - tileWorldSize.Y);
                        Vector2 size = new Vector2(gameMap.Tiles[x, y].Size.X, 2 * gameMap.Tiles[x, y].Size.Y);
                        Vector4 collider = new Vector4(position.X + 16, position.Y + 16, position.X + tileWorldSize.X - 16, position.Y + tileWorldSize.Y - 16); // make it proportional to ClientSize!
                        int rndIndex = rnd.Next(0, 4);
                        go = new Character(position, size, collider, characterImages[rndIndex]);
                    }

                    gameMap.Tiles[x, y].GameObject = go;
                }
            }
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            Graphics.DrawMap(gameMap, e);
        }

        private void OnTick(object sender, EventArgs e)
        {
            this.Refresh();
        }

    }
}