using client_graphics.SignalR;
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
    internal class PlaceBombCommand : Command
    {
        protected Character character;
        protected GameMap gameMap;
        protected SignalRConnection connection;

        public PlaceBombCommand (GameMap gameMap, Character character, SignalRConnection connnection) 
        {
            this.gameMap = gameMap;
            this.character = character;
            this.connection = connnection;
        }

        public override void Execute()
        {
            character.PlaceExplosive(gameMap);
        }

        public override void Undo()
        {

        }
    }
}
