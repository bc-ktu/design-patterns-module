using System.Media;

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
