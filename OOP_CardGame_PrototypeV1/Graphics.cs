using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_CardGame_PrototypeV1
{
    internal static class Graphics
    {
        public static void DrawCard(GameCard card, PaintEventArgs e)
        {
            e.Graphics.DrawImage(card.BackImage, card.ToRectangle());
            e.Graphics.DrawImage(card.MonsterImage, card.ToRectangle());
            e.Graphics.DrawImage(card.FrontImage, card.ToRectangle());
        }

        public static void DrawHand(Hand hand, PaintEventArgs e)
        {
            for (int i = 0; i < hand.Size; i++)
                DrawCard(hand.Cards[i], e);
        }

        public static void DrawTable(GameTable table, PaintEventArgs e)
        {
            e.Graphics.DrawImage(table.Image, table.ToRectangle());

            for (int i = 0; i < table.RivalTilesetSize; i++)
            {
                GameTile tile = table.CardTiles[GameTable.Rival, i];
                e.Graphics.DrawImage(tile.Image, tile.ToRectangle());
            }

            for (int i = 0; i < table.PlayerTilesetSize; i++)
            {
                GameTile tile = table.CardTiles[GameTable.Player, i];
                e.Graphics.DrawImage(tile.Image, tile.ToRectangle());
            }
        }
    }
}
