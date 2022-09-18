﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Bomberman_client_graphics_v1
{
    internal static class Graphics
    {
        public static void DrawBitmap(Bitmap image, Vector2 position, PaintEventArgs e)
        {
            e.Graphics.DrawImage(image, position.ToPoint());
        }

        public static void DrawMap(Map gameMap, PaintEventArgs e)
        {
            for (int y = 0; y < gameMap.Size.Y; y++)
            {
                for (int x = 0; x < gameMap.Size.X; x++)
                {
                    e.Graphics.DrawImage(gameMap.Tiles[x, y].Image, gameMap.Tiles[x, y].ToRectangle());
                    if (!(gameMap.Tiles[x, y].GameObject is EmptyGameObject)) 
                    {
                        e.Graphics.DrawImage(gameMap.Tiles[x, y].GameObject.Image, gameMap.Tiles[x, y].GameObject.ToRectangle());
                    }
                }
            }
        }

        public static void DrawColliders(Map gameMap, Color color, float width, PaintEventArgs e)
        {
            Pen pen = new Pen(color, width);

            for (int y = 0; y < gameMap.Size.Y; y++)
            {
                for (int x = 0; x < gameMap.Size.X; x++)
                {
                    if (!(gameMap.Tiles[x, y].GameObject is EmptyGameObject))
                    {
                        int xGO = gameMap.Tiles[x, y].GameObject.Collider.X;
                        int yGO = gameMap.Tiles[x, y].GameObject.Collider.Y;
                        int widthGO = gameMap.Tiles[x, y].GameObject.Collider.Z - xGO;
                        int heightGO = gameMap.Tiles[x, y].GameObject.Collider.W - yGO;
                        Rectangle rect = new Rectangle(xGO, yGO, widthGO, heightGO);
                        e.Graphics.DrawRectangle(pen, rect);
                    }
                }
            }
        }

        //public static void DrawCard(GameCard card, PaintEventArgs e)
        //{
        //    e.Graphics.DrawImage(card.BackImage, card.ToRectangle());
        //    e.Graphics.DrawImage(card.MonsterImage, card.ToRectangle());
        //    e.Graphics.DrawImage(card.FrontImage, card.ToRectangle());
        //}

        //public static void DrawHand(Hand hand, PaintEventArgs e)
        //{
        //    for (int i = 0; i < hand.Size; i++)
        //        DrawCard(hand.Cards[i], e);
        //}

        //public static void DrawTable(GameTable table, PaintEventArgs e)
        //{
        //    e.Graphics.DrawImage(table.Image, table.ToRectangle());

        //    for (int i = 0; i < table.RivalTilesetSize; i++)
        //    {
        //        GameTile tile = table.CardTiles[GameTable.Rival, i];
        //        e.Graphics.DrawImage(tile.Image, tile.ToRectangle());
        //    }

        //    for (int i = 0; i < table.PlayerTilesetSize; i++)
        //    {
        //        GameTile tile = table.CardTiles[GameTable.Player, i];
        //        e.Graphics.DrawImage(tile.Image, tile.ToRectangle());
        //    }
        //}
    }
}
