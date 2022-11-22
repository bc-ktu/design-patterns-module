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
    internal class LeftCommand : Command
    {
        protected Player character;
        protected SignalRConnection connection;
        public LeftCommand (Player character, SignalRConnection connnection)
        {
            this.character = character;
            this.connection = connnection; 
        }

        public override void Execute()
        {
            character.Move(Direction.Left);
            connection.Connection.InvokeAsync("Move", Direction.Left.X, Direction.Left.Y, character.SpeedModifier, character.GetMoveSpeed());
        }

        public override void Undo()
        {
            character.Move(Direction.Right);
            connection.Connection.InvokeAsync("Move", Direction.Right.X, Direction.Right.Y, character.SpeedModifier, character.GetMoveSpeed());
        }
    }
}
