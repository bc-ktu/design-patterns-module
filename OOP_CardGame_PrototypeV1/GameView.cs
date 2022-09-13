using System.Drawing.Drawing2D;

namespace OOP_CardGame_PrototypeV1
{
    public partial class GameView : Form
    {
        private const string MapImageFilename = "Map000.png";
        private const string FrontImageFilename = "CardFrontFrame000.png";
        private const string BackImageFilename = "CardBackFrame000.png";
        private const string MonsterImageFilename = "CardMonster000.png";
        private const string CardTileImageFilename = "CardPlacement007.png";

        private const double HoverEnlarge = 1.2;
        
        private GameTable gameTable;
        private Hand hand;

        private Interactable objectHover;
        private Interactable objectClicked;
        private Vector2 vectorMouseToCard;

        private GameCard cardBigBlueSnake;

        public GameView()
        {
            InitializeComponent();
            Setup();

            Debug.Set(DebugTextbox);
            Debug.Enabled(true);
            Debug.LogLine("Text", 15, 1.54, 'c');
            Debug.LogLine();
            Debug.LogLine("END");
        }

        private void Setup()
        {
            // Dummy card
            Bitmap frontImage = new Bitmap(PathUtils.CreatePath(PathUtils.FolderAssets, PathUtils.FolderFrontFrames, FrontImageFilename));
            Bitmap backImage = new Bitmap(PathUtils.CreatePath(PathUtils.FolderAssets, PathUtils.FolderBackFrames, BackImageFilename));
            Bitmap monsterImage = new Bitmap(PathUtils.CreatePath(PathUtils.FolderAssets, PathUtils.FolderMonsters, MonsterImageFilename));
            int x = this.ClientSize.Width / 2;
            int y = this.ClientSize.Height / 2;
            int width = (int)(frontImage.Width * 1.0);
            int height = (int)(frontImage.Height * 1.0);
            cardBigBlueSnake = new GameCard(frontImage, monsterImage, backImage, x, y, width, height, true);

            int n = 4;
            Bitmap mapImage = new Bitmap(PathUtils.CreatePath(PathUtils.FolderAssets, PathUtils.FolderMaps, MapImageFilename));
            gameTable = new GameTable(mapImage, this.ClientSize.Width, this.ClientSize.Height, n);

            int offset = 10;
            int marginLeft = 270;
            int marginTop = 350;
            Bitmap cardTileImage = new Bitmap(PathUtils.CreatePath(PathUtils.FolderAssets, PathUtils.FolderPlacements, CardTileImageFilename));
            for (int i = 0; i < n; i++)
            {
                x = marginLeft + i * (width + offset);
                y = marginTop;
                GameTile tile = new GameTile(cardTileImage, x, y, width, height);
                gameTable.AddTile(tile, GameTable.Rival);
            }
            for (int i = 0; i < n; i++)
            {
                x = marginLeft + i * (width + offset);
                y = marginTop + height + offset;
                GameTile tile = new GameTile(cardTileImage, x, y, width, height);
                gameTable.AddTile(tile, GameTable.Player);
            }

            offset = 10;
            int margin = 80;
            hand = new Hand(5);
            for (int i = 0; i < hand.MaxSize; i++)
            {
                x = margin + i * (width + offset);
                y = this.ClientSize.Height - offset - height / 2;
                GameCard card = new GameCard(frontImage, monsterImage, backImage, x, y, width, height);
                hand.AddCard(card);
            }
        }

        private void GameView_Load(object sender, EventArgs e)
        {
            this.DoubleBuffered = true;
            this.Paint += new PaintEventHandler(GameView_OnPaint);
        }

        private void GameView_OnPaint(object sender, PaintEventArgs e)
        {
            Graphics.DrawTable(gameTable, e);
            Graphics.DrawCard(cardBigBlueSnake, e);
            Graphics.DrawHand(hand, e);
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            Vector2 mousePosition = new Vector2(PointToClient(Cursor.Position));

            if (objectClicked != null)
                objectClicked.SetPosition(mousePosition + vectorMouseToCard);

            if (objectHover != null)
            {
                Interactable cardCollision = Physics.IsColliding(objectHover, mousePosition);
                if (cardCollision == null)
                {
                    objectHover.SetSize(objectHover.WorldSpaceSize / HoverEnlarge);
                    objectHover = null;
                }
            }

            if (Physics.IsColliding(cardBigBlueSnake, mousePosition) != null)
            {
                if (objectHover == null)
                {
                    objectHover = cardBigBlueSnake;
                    objectHover.SetSize(HoverEnlarge * objectHover.WorldSpaceSize);
                }
            }

            for (int i = 0; i < hand.Size; i++) 
            {
                Interactable cardCollision = Physics.IsColliding(hand.Cards[i], mousePosition);
                if (cardCollision != null)
                {
                    if (objectHover == null)
                    {
                        objectHover = cardCollision;
                        objectHover.SetSize(HoverEnlarge * objectHover.WorldSpaceSize);
                    }
                }
            }

            this.Refresh();
        }

        private void GameView_MouseDown(object sender, MouseEventArgs e)
        {
            Vector2 mousePosition = new Vector2(PointToClient(Cursor.Position));

            objectClicked = Physics.IsColliding(cardBigBlueSnake, mousePosition);

            if (objectClicked == null)
            {
                for (int i = 0; i < hand.Size; i++)
                {
                    objectClicked = Physics.IsColliding(hand.Cards[i], mousePosition);
                    if (objectClicked != null)
                        break;
                }
            }

            if (objectClicked != null)
                vectorMouseToCard = objectClicked.WorldSpacePosition - mousePosition;
        }

        private void GameView_MouseUp(object sender, MouseEventArgs e)
        {
            Vector2 mousePosition = new Vector2(PointToClient(Cursor.Position));

            if (objectClicked != null)
            {
                for (int i = 0; i < gameTable.PlayerTilesetSize; i++)
                {
                    GameTile tile = gameTable.CardTiles[GameTable.Player, i];
                    Interactable tileCollision = Physics.IsColliding(tile, mousePosition);

                    if (tileCollision != null)
                    {
                        objectClicked.SetPosition(tileCollision.WorldSpacePosition.x, tileCollision.WorldSpacePosition.y);
                        break;
                    }
                }

                for (int i = 0; i < gameTable.RivalTilesetSize; i++)
                {
                    GameTile tile = gameTable.CardTiles[GameTable.Rival, i];
                    Interactable tileCollision = Physics.IsColliding(tile, mousePosition);

                    if (tileCollision != null)
                    {
                        objectClicked.SetPosition(tileCollision.WorldSpacePosition.x, tileCollision.WorldSpacePosition.y);
                        break;
                    }
                }
            }

            objectClicked = null;
        }

    }
}