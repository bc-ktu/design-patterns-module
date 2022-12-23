using client_graphics.Helpers;
using com.sun.org.apache.xml.@internal.resolver.helpers;
using Utils.Decorator;
using client_graphics.GameLogic;
using client_graphics.GameObjects;
using client_graphics.GameObjects.Animates;
using Utils.GUIElements;
using Utils.Helpers;
using client_graphics.Map;
using Utils.Math;
using client_graphics.Decorator;
using client_graphics.Composite;
using client_graphics.Iterator;

namespace client_graphics
{
    internal static class Graphics
    {
        public static void DrawMap(GameMap gameMap, Player player, EnemyType enemies, List<Player> otherPlayers, PaintEventArgs e)
        {
            List<Vector2> playerIndexes = new List<Vector2>();
            List<IGraphicsDecorator> players = new List<IGraphicsDecorator>();
            Vector2 check = new Vector2(0, 0);
            EnemyIterator iterator = new EnemyIterator(enemies);

            string filepath;
            for (int i = 0; i < otherPlayers.Count; i++)
            {
                filepath = Pather.Create(Pather.FolderAssets, Pather.FolderTextures, Pather.FolderGUI, Pather.GUIHealthIcon);
                Bitmap healthIcon = new Bitmap(filepath);
                filepath = Pather.Create(Pather.FolderAssets, Pather.FolderTextures, Pather.FolderGUI, Pather.GuiDamageIcon);
                Bitmap damageIcon = new Bitmap(filepath);
                Vector2 size = new Vector2(32, 32);
                string damageValue = otherPlayers[i].Explosive.Fire.Damage.ToString();
                string healthValue = otherPlayers[i].Health.ToString();

                IconDecorator healthIconDecorator = new IconDecorator(otherPlayers[i], healthIcon, new Vector2(-10, -25), size);
                TextDecorator healthTextDecorator = new TextDecorator(healthIconDecorator, healthValue, GameSettings.DecoratorFont, GameSettings.GUIFontColor, new Vector2(20, 0), size);
                IconDecorator damageIconDecorator = new IconDecorator(healthTextDecorator, damageIcon, new Vector2(20, 0), size);
                TextDecorator damageTextDecorator = new TextDecorator(damageIconDecorator, damageValue, GameSettings.DecoratorFont, GameSettings.GUIFontColor, new Vector2(20, 0), size);

                playerIndexes.Add(otherPlayers[i].WorldPosition / gameMap.TileSize);
                players.Add(damageTextDecorator);
            }

            // draw tiles
            for (int y = 0; y < gameMap.Size.Y; y++)
                for (int x = 0; x < gameMap.Size.X; x++)
                    gameMap[x, y].Draw(e);

            // draw game objects
            for (int y = 0; y < gameMap.Size.Y; y++)
            {
                for (int x = 0; x < gameMap.Size.X; x++)
                {
                    Vector2 index = new Vector2(x, y);

                    foreach (GameObject go in gameMap[x, y].GameObjects)
                        go.Draw(e);

                    if (player.GetPositionOnMap(gameMap) == index)
                        player.Draw(e);

                    for (iterator.First(); !iterator.IsDone(); iterator.Next())
                    {
                        if (!iterator.CurrentItem().Equals(check) && iterator.CurrentEnemy().GetPositionOnMap(gameMap) == index)
                        {
                            iterator.CurrentEnemy().Draw(e);
                        }
                    }
                    /*if (enemyIndex == index)
                        enemy.Draw(e);*/

                    for (int i = 0; i < playerIndexes.Count; i++) // other players are drawn incorrectly
                        if (playerIndexes[i] == index)
                            players[i].Draw(e);
                }
            }
        }

        public static void DrawGUI(GUI gui, PaintEventArgs e)
        {
            e.Graphics.DrawImage(gui.FrameImage, gui.ToRectangle());

            e.Graphics.DrawImage(gui.HealthIcon.Image, gui.HealthIcon.ToRectangle());
            e.Graphics.DrawImage(gui.SpeedIcon.Image, gui.SpeedIcon.ToRectangle());
            e.Graphics.DrawImage(gui.CapacityIcon.Image, gui.CapacityIcon.ToRectangle());
            e.Graphics.DrawImage(gui.RangeIcon.Image, gui.RangeIcon.ToRectangle());
            e.Graphics.DrawImage(gui.DamageIcon.Image, gui.DamageIcon.ToRectangle());

            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;
            e.Graphics.DrawString(gui.HealthText.Text, gui.Font, gui.FontColor, gui.HealthText.ToRectangle(), stringFormat);
            e.Graphics.DrawString(gui.SpeedText.Text, gui.Font, gui.FontColor, gui.SpeedText.ToRectangle(), stringFormat);
            e.Graphics.DrawString(gui.CapacityText.Text, gui.Font, gui.FontColor, gui.CapacityText.ToRectangle(), stringFormat);
            e.Graphics.DrawString(gui.RangeText.Text, gui.Font, gui.FontColor, gui.RangeText.ToRectangle(), stringFormat);
            e.Graphics.DrawString(gui.DamageText.Text, gui.Font, gui.FontColor, gui.DamageText.ToRectangle(), stringFormat);
        }

        public static void DrawColliders(GameMap gameMap, LookupTable collisions, Color color, Color collisionColor, float width, PaintEventArgs e)
        {
            Pen pen = new Pen(color, width);
            Pen collisionsPen = new Pen(collisionColor, width);

            for (int y = 0; y < gameMap.Size.Y; y++)
            {
                for (int x = 0; x < gameMap.Size.X; x++)
                {
                    MapTile mapTile = gameMap[x, y];
                    foreach (GameObject go in mapTile.GameObjects)
                    {
                        int xGO = go.Collider.X;
                        int yGO = go.Collider.Y;
                        int widthGO = go.Collider.Z - xGO;
                        int heightGO = go.Collider.W - yGO;
                        Rectangle rect = new Rectangle(xGO, yGO, widthGO, heightGO);
                        e.Graphics.DrawRectangle(pen, rect);
                    }
                }
            }

            for (int i = 0; i < collisions.Positions.Length; i++)
            {
                MapTile mapTile = gameMap[collisions.Positions[i]];
                foreach (GameObject go in mapTile.GameObjects)
                {
                    if (!collisions.Contains(collisions.Positions[i], go))
                        continue;

                    int xGO = go.Collider.X;
                    int yGO = go.Collider.Y;
                    int widthGO = go.Collider.Z - xGO;
                    int heightGO = go.Collider.W - yGO;
                    Rectangle rect = new Rectangle(xGO, yGO, widthGO, heightGO);
                    e.Graphics.DrawRectangle(collisionsPen, rect);
                }
            }
        }

        public static void DrawCollider(GameObject go, Color color, float width, PaintEventArgs e)
        {
            Pen pen = new Pen(color, width);
            int xGO = go.Collider.X;
            int yGO = go.Collider.Y;
            int widthGO = go.Collider.Z - xGO;
            int heightGO = go.Collider.W - yGO;
            Rectangle rect = new Rectangle(xGO, yGO, widthGO, heightGO);
            e.Graphics.DrawRectangle(pen, rect);
        }
    }
}
