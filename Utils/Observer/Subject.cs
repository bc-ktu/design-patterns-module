namespace Utils.Observer
{
    public class Subject : ISubject
    {
        private List<IObserver> observers;
        private string sound;

        public Subject()
        {
            this.observers = new();
        }

        public void NotifyObservers()
        {
            observers.ForEach(observers => observers.Update(sound));
        }

        public void RegisterObserver(IObserver o)
        {
            observers.Add(o);
        }

        public void RemoveObserver(IObserver o)
        {
            observers.Remove(o);
        }

        public void MakeSound(string sound)
        {
            this.sound = sound;
            observers.ForEach(observers => observers.Update(sound));
        }
    }
}
