using System.IO;
using System.Runtime.CompilerServices;

namespace client_graphics.Chain_of_responsibility
{
    public enum MessageType
    {
        Default,
        Warning,
        Network,
        Error
    }

    public abstract class Logger
    {
        protected Logger _next;
        protected StreamWriter _streamWriter;

        public Logger SetNext(Logger nextLogger)
        {
            _next = nextLogger;
            return nextLogger;
        }
        public abstract void Log(MessageType type, string message, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0);
    }
}
