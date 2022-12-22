using client_graphics.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using client_graphics.AbstractFactory;
using client_graphics.GameObjects;
using client_graphics.GameObjects.Animates;
using Utils.Helpers;
using client_graphics.Map;
using Microsoft.AspNetCore.SignalR.Client;

namespace client_graphics.Command
{
    internal class PlaceBombCommand : Command
    {
        protected Player character;
        protected GameMap gameMap;
        protected SignalRConnection connection;
        protected ILevelFactory levelFactory;

        public PlaceBombCommand (GameMap gameMap, Player character, SignalRConnection connnection, ILevelFactory levelFactory) 
        {
            this.gameMap = gameMap;
            this.character = character;
            this.connection = connnection;
            this.levelFactory = levelFactory;
        }

        public override void Execute()
        {
            var position = character.PlaceExplosive(gameMap, levelFactory);
            if (position.X != 0 && position.Y != 0)
            {
                connection.Connection.InvokeAsync("PlaceBomb", character.Explosive.Fire.Damage, position.X, position.Y);
            }
        }

        public override void Undo()
        {

        }
    }
}
