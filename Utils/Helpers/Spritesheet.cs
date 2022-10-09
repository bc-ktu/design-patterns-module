using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

using Utils.Math;

namespace Utils.Helpers
{
    public static class Spritesheet
    {
        public static Bitmap ExtractSprite(Bitmap spritesheet, Vector2 tileSize, Vector2 tileToExtract)
        {
            Bitmap image = new Bitmap(tileSize.X, tileSize.Y);

            for (int y = tileToExtract.Y * tileSize.Y; y < tileToExtract.Y * tileSize.Y + tileSize.Y; y++)
            {
                for (int x = tileToExtract.X * tileSize.X; x < tileToExtract.X * tileSize.X + tileSize.X; x++)
                {
                    int xPixel = x - tileToExtract.X * tileSize.X;
                    int yPixel = y - tileToExtract.Y * tileSize.Y;
                    Color color = spritesheet.GetPixel(x, y);
                    image.SetPixel(xPixel, yPixel, color);
                }
            }

            return image;
        }

        public static Bitmap[,] ExtractAll(Bitmap spritesheet, Vector2 tileSize)
        {
            Vector2 tileMapSize = GetTileMapSize(spritesheet, tileSize);
            Bitmap[,] images = new Bitmap[tileMapSize.X, tileMapSize.Y]; 

            for (int y = 0; y < tileMapSize.Y; y++)
            {
                for (int x = 0; x < tileMapSize.X; x++)
                {
                    images[x, y] = ExtractSprite(spritesheet, tileSize, new Vector2(x, y));
                }
            }

            return images;
        }

        public static Vector2 GetTileMapSize(Bitmap spritesheet, Vector2 tileSize)
        {
            return new Vector2(spritesheet.Width / tileSize.X, spritesheet.Height / tileSize.Y);
        }
    }
}
