namespace client_graphics
{
    internal static class Debug
    {
        private static readonly int _maxLines = 100;

        private static RichTextBox _debugTextBox = new RichTextBox();
        public static bool Enabled { get; private set; }

        private static void Clear()
        {
            if (_debugTextBox.Lines.Length >= _maxLines)
                _debugTextBox.Clear();
        }

        public static void Set(RichTextBox richTextBox)
        {
            _debugTextBox = richTextBox;
        }

        public static void Enable(bool arg)
        {
            _debugTextBox.Visible = arg;
            Enabled = arg;
        }

        public static void Log(params object[] args)
        {
            Clear();
            for (int i = 0; i < args.Length; i++)
                _debugTextBox.AppendText(args[i].ToString() + " ");
        }

        public static void LogLine(params object[] args)
        {
            Clear();
            for (int i = 0; i < args.Length; i++)
                _debugTextBox.AppendText(args[i].ToString() + " ");
            _debugTextBox.AppendText("\n");
        }

    }
}
