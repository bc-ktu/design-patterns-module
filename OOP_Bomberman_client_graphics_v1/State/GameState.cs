using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client_graphics.State
{
    // Context
    public class GameState
    {
        private State state = null;
        private GameView gameView;

        public GameState(State state, GameView gameView)
        {
            this.TransitionTo(state);
            this.gameView = gameView;
        }

        public void TransitionTo(State state)
        {
            this.state = state;
            this.state.SetGameState(this);
        }

        public void MoveRequest(bool consoleCommand, Keys key)
        {
            this.state.MoveHandle(gameView, consoleCommand, key);
        }

        public void ChangePanel()
        {
            this.state.PanelHandle(gameView);
        }

        public void UpdateGameState()
        {
            state.UpdateState();
        }
    }
}
