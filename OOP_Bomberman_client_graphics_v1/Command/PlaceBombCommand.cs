using client_graphics.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.AbstractFactory;
using Utils.GameObjects;
using Utils.GameObjects.Animates;
using Utils.Helpers;
using Utils.Map;

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
            character.PlaceExplosive(gameMap, levelFactory);
        }

        public override void Undo()
        {

        }
    }
}
