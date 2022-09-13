using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_CardGame_PrototypeV1
{
    internal static class Physics
    {
        public static Interactable IsColliding(Interactable card, Vector2 mousePosition)
        {
            // if { if } - for optimization!
            if (mousePosition.x > card.Collider.x && mousePosition.y > card.Collider.y)
            {
                if (mousePosition.x <= card.Collider.z && mousePosition.y <= card.Collider.w)
                {
                    return card;
                }
            }

            return null;
        }
    }
}
