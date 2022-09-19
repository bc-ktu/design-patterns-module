using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Bomberman_client_graphics_v1
{
    internal static class Debug
    {
        private static readonly int _maxLines = 7;

        private static RichTextBox _debugTextBox = new RichTextBox();

        private static void Clear()
        {
            if (_debugTextBox.Lines.Length >= _maxLines)
                _debugTextBox.Clear();
        }

        public static void Set(RichTextBox richTextBox)
        {
            _debugTextBox = richTextBox;
        }

        public static void Enabled(bool arg)
        {
            _debugTextBox.Visible = arg;
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
