using System.Media;

namespace Utils.Observer
{
    public class DamageObserver : IObserver
    {
        readonly SoundPlayer DamageSound;

        public DamageObserver()
        {
            string filepath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName + "\\OOP_Bomberman_client_graphics_v1\\Assets\\SoundEffects\\damage.wav";
            this.DamageSound = new(filepath);
            DamageSound.Load();
        }
        public void Update(string sound)
        {
            if (sound.Contains("Damage"))
            {
                DamageSound.Play();
                return;
            }
        }
    }
}
