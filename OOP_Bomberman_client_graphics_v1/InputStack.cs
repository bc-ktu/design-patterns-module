namespace client_graphics
{
    internal class InputStack
    {
        private List<Keys> _pressedKeysList;

        public InputStack()
        {
            _pressedKeysList = new List<Keys>();
        }

        public Keys Peek()
        {
            if (_pressedKeysList.Count == 0)
                return Keys.None;

            Keys key = _pressedKeysList[_pressedKeysList.Count - 1];
            return key;
        }
        public void Push(Keys key)
        {
            if (_pressedKeysList.Contains(key))
                return;

            _pressedKeysList.Add(key);
        }

        public void Remove(Keys key)
        {
            _pressedKeysList.Remove(key);
        }

    }
}
