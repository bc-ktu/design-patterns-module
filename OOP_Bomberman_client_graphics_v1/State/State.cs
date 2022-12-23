using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client_graphics.State
{
    public abstract class State
    {
        protected GameState gameState;
        public void SetGameState(GameState game)
        {
            gameState = game;
        }
        public abstract void MoveHandle(GameView gameView, bool consoleParam, Keys key);
        public abstract void UpdateState();
        public abstract void PanelHandle(GameView gameView);
    }
}
