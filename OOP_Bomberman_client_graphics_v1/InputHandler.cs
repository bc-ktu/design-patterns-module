using Microsoft.AspNetCore.SignalR.Client;
using OOP_Bomberman_client_graphics_v1.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Bomberman_client_graphics_v1
{
    internal static class InputHandler // state machine better?
    {
        public static void HandleKey(Keys key, Character character, Map gameMap, SignalRConnection Con)
        {
            if (key == Settings.KeyUp)
            {
                character.Move(Direction.Up);
                Con.Connection.InvokeAsync("Move", Direction.Up.X, Direction.Up.Y);
            }
            else if (key == Settings.KeyDown)
            {
                character.Move(Direction.Down);
                Con.Connection.InvokeAsync("Move", Direction.Down.X, Direction.Down.Y);
            }
            else if (key == Settings.KeyRight)
            {
                character.Move(Direction.Right);
                Con.Connection.InvokeAsync("Move", Direction.Right.X, Direction.Right.Y);
            }
            else if (key == Settings.KeyLeft)
            {
                character.Move(Direction.Left);
                Con.Connection.InvokeAsync("Move", Direction.Left.X, Direction.Left.Y);
            }
            else if (key == Settings.KeyBomb)
            {
                character.PlaceBomb(gameMap);
                //Con.Connection.InvokeAsync("MapSeed"); place bomb change later
            }
        }
    }
}
