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
    public class ExplosionObserver : IObserver
    {
        readonly SoundPlayer ExplosionSound;

        public ExplosionObserver()
        {
            string filepath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName + "\\OOP_Bomberman_client_graphics_v1\\Assets\\SoundEffects\\explosion.wav";
            this.ExplosionSound = new(filepath);
            ExplosionSound.Load();
        }

        public void Update(string sound)
        {
            if (sound.Contains("Explode"))
            {
                ExplosionSound.Play();
                return;
            }
        }
    }
}
