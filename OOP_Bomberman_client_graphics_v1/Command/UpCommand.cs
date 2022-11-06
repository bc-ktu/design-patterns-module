using client_graphics.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.GameObjects;
using Utils.GameObjects.Animates;
using Utils.Helpers;

namespace client_graphics.Command
{
    public class UpCommand : Command
    {
        protected Player character;
        protected SignalRConnection connection;
        public UpCommand(Player character, SignalRConnection connnection) 
        {
            this.character = character;
            this.connection = connnection;
        }

        public override void Execute()
        {
            character.Move(Direction.Up);
            connection.Connection.InvokeAsync("Move", Direction.Up.X, Direction.Up.Y, character.SpeedModifier, character.GetMoveSpeed());
        }

        public override void Undo()
        {
            character.Move(Direction.Down);
            connection.Connection.InvokeAsync("Move", Direction.Down.X, Direction.Down.Y, character.SpeedModifier, character.GetMoveSpeed());
        }
    }
}
