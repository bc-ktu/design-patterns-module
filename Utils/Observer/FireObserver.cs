using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using Utils.GameObjects.Explosives;
using Utils.GameObjects;
using Utils.Helpers;

namespace Utils.Observer
{
    public class FireObserver : IObserver
    {
        readonly SoundPlayer FireSound;

        public FireObserver()
        {
            string filepath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName + "\\OOP_Bomberman_client_graphics_v1\\Assets\\SoundEffects\\fire.wav";
            this.FireSound = new(filepath);
            FireSound.Load();
        }

        public void Update(string sound)
        {
            if (sound.Contains("Fire"))
            {
                FireSound.Play();
                return;
            }
        }
    }
}
