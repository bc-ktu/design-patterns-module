using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Utils.GameObjects;

namespace Utils.GameLogic
{
    public class LookupTable
    {
        public List<Character> Players { get; private set; }
        public List<GameObject> GameObjects { get; private set; }
        public int Count { get; private set; }

        public LookupTable()
        {
            Players = new List<Character>();
            GameObjects = new List<GameObject>();
        }

        public void Add(Character player, GameObject explosive)
        {
            Players.Add(player);
            GameObjects.Add(explosive);
            Count++;
        }

        public void Remove(GameObject explosive)
        {
            int index = GameObjects.IndexOf(explosive);
            //Character player = Players[index];
            //player.GiveExplosive();

            Players.RemoveAt(index);
            GameObjects.RemoveAt(index);
            Count--;
        }

        public void Remove(int index) // add Remove(Vector2 position) to solve missing fire issue
        {
            // Character player = Players[index];
            // player.GiveExplosive();

            Players.RemoveAt(index);
            GameObjects.RemoveAt(index);
            Count--;
        }

        public override string ToString()
        {
            string text = "";

            for (int i = 0; i < Count; i++)
                text += "Ch: " + Players[i].ToString() + "\nGO: " + GameObjects[i].ToString();

            return text;
        }

    }
}
