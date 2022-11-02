using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Utils.GameObjects;
using Utils.GUIElements;
using Utils.Helpers;
using Utils.Map;
using Utils.Math;

namespace client_graphics
{
    internal static class Graphics
    {
        public static void DrawBitmap(Bitmap image, Vector2 position, PaintEventArgs e)
        {
            e.Graphics.DrawImage(image, position.ToPoint());
        }

        public static void DrawMap(GameMap gameMap, PaintEventArgs e)
        {
            for (int y = 0; y < gameMap.Size.Y; y++)
            {
                for (int x = 0; x < gameMap.Size.X; x++)
                {
                    e.Graphics.DrawImage(gameMap[x, y].Image, gameMap[x, y].ToRectangle());

                    foreach (GameObject go in gameMap[x, y].GameObjects)
                        e.Graphics.DrawImage(go.Image, go.ToRectangle());
                }
            }
        }

        public static void DrawColliders(GameMap gameMap, LookupTable collisions, Color color, Color collisionColor, float width, PaintEventArgs e)
        {
            Pen pen = new Pen(color, width);
            Pen collisionsPen = new Pen(collisionColor, width);
            Pen currentPen;

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

        public static void DrawGameObject(GameObject go, PaintEventArgs e)
        {
            e.Graphics.DrawImage(go.Image, go.ToRectangle());
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

    }
}
