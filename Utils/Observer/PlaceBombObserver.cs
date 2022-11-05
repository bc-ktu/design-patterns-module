using System.Media;

namespace Utils.Observer
{
    public class PlaceBombObserver : IObserver
    {
        readonly SoundPlayer PlaceBombSound;
        public PlaceBombObserver()
        {
            string filepath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName + "\\OOP_Bomberman_client_graphics_v1\\Assets\\SoundEffects\\placeBomb.wav";
            this.PlaceBombSound = new(@filepath);
            PlaceBombSound.Load();
        }
        public void Update(string sound)
        {
            if (sound.Contains("PlaceBomb"))
            {
                PlaceBombSound.Play();
                return;
            }
        }
    }
}
