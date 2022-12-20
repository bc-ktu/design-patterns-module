using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client_graphics.State
{
    internal class WaitingGameState : State
    {
        public override void MoveHandle(GameView gameView, bool consoleParam, Keys key)
        {
            //throw new NotImplementedException();
        }

        public override void UpdateState()
        {
            this.gameState.TransitionTo(new PlayingGameState());
        }
    }
}
