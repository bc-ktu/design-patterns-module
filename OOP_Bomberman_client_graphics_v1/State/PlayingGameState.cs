using client_graphics.Command;
using client_graphics.GameObjects.Animates;
using client_graphics.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Helpers;

namespace client_graphics.State
{
    internal class PlayingGameState : State
    {
        public override void MoveHandle(GameView gameView, bool consoleParam, Keys key)
        {
            gameView.ButtonClick(key, consoleParam);
        }

        public override void UpdateState()
        {
            this.gameState.TransitionTo(new FinishedGameState());
        }
    }
}
