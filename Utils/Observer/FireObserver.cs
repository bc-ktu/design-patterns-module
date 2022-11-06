using System.Media;

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
