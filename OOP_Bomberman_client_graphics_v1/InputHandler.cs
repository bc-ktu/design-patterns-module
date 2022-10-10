using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

using client_graphics.SignalR;
using Utils.GameObjects;
using Utils.Helpers;

namespace client_graphics
{
    internal static class InputHandler
    {
        public static void HandleKey(Keys key, Character character, Map gameMap, SignalRConnection Con)
        {
            if (key == Input.KeyUp)
            {
                character.Move(Direction.Up);
                Con.Connection.InvokeAsync("Move", Direction.Up.X, Direction.Up.Y);
            }
            else if (key == Input.KeyDown)
            {
                character.Move(Direction.Down);
                Con.Connection.InvokeAsync("Move", Direction.Down.X, Direction.Down.Y);
            }
            else if (key == Input.KeyRight)
            {
                character.Move(Direction.Right);
                Con.Connection.InvokeAsync("Move", Direction.Right.X, Direction.Right.Y);
            }
            else if (key == Input.KeyLeft)
            {
                character.Move(Direction.Left);
                Con.Connection.InvokeAsync("Move", Direction.Left.X, Direction.Left.Y);
            }
            else if (key == Input.KeyBomb)
            {
                character.PlaceBomb(gameMap);
                //Con.Connection.InvokeAsync("MapSeed"); place bomb change later
            }
        }
    }
}
