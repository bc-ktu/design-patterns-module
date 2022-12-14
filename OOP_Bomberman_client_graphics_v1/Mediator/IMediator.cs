using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using client_graphics.GameObjects.Interactables;
using client_graphics.Map;
using Utils.Enum;
using Utils.Math;

namespace client_graphics.Mediator
{
    public interface IMediator
    {
        Powerup Send(PowerupType type, GameMap gameMap, Vector2 index);
    }
}
