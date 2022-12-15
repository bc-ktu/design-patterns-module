using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client_graphics.State
{
    internal class FinishedGameState : State
    {
        public override void MoveHandle(GameView gameView, bool consoleParam, Keys key)
        {
            // allow only 
        }

        public override void UpdateState()
        {
            this.gameState.TransitionTo(new WaitingGameState());
        }
    }
}
