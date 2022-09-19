using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Bomberman_client_graphics_v1
{
    internal static class InputHandler // state machine better?
    {
        public static void HandleKey(Keys key, Character character)
        {
            if (key == Settings.KeyUp)
                character.Move(Direction.Up);
            else if (key == Settings.KeyDown)
                character.Move(Direction.Down);
            else if (key == Settings.KeyRight)
                character.Move(Direction.Right);
            else if (key == Settings.KeyLeft)
                character.Move(Direction.Left);
            else if (key == Settings.KeyBomb)
                character.PlaceBomb();
        }
    }
}
