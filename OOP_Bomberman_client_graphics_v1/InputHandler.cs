using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

using client_graphics.SignalR;
using Utils.Helpers;
using Utils.GameObjects.Animates;
using Utils.Map;

namespace client_graphics
{
    internal static class InputHandler
    {
        public static void HandleKey(Keys key, Player character, GameMap gameMap, SignalRConnection Con)
        {
            if (key == Input.KeyUp)
            {
                character.Move(Direction.Up);
                Con.Connection.InvokeAsync("Move", Direction.Up.X, Direction.Up.Y, character.SpeedModifier, character.GetMoveSpeed());
            }
            else if (key == Input.KeyDown)
            {
                character.Move(Direction.Down);
                Con.Connection.InvokeAsync("Move", Direction.Down.X, Direction.Down.Y, character.SpeedModifier, character.GetMoveSpeed());
            }
            else if (key == Input.KeyRight)
            {
                character.Move(Direction.Right);
                Con.Connection.InvokeAsync("Move", Direction.Right.X, Direction.Right.Y, character.SpeedModifier, character.GetMoveSpeed());
            }
            else if (key == Input.KeyLeft)
            {
                character.Move(Direction.Left);
                Con.Connection.InvokeAsync("Move", Direction.Left.X, Direction.Left.Y, character.SpeedModifier, character.GetMoveSpeed());
            }
            else if (key == Input.KeyBomb)
            {
                character.PlaceExplosive(gameMap);
                //Con.Connection.InvokeAsync("MapSeed"); place bomb change later
            }
        }
    }
}
