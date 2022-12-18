using System.IO;
using System.Runtime.CompilerServices;

namespace client_graphics.Chain_of_responsibility
{
    internal class NetworkLogger : Logger
    {
        public NetworkLogger(StreamWriter streamWriter)
        {
            _streamWriter = streamWriter;
        }
        public override void Log(MessageType type, string message, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0)
        {
            if (type != MessageType.Network)
                _next?.Log(type, message, file, member, line);
            else
            {
                _streamWriter.WriteLine($"NETWORK ACTIVITY: {message}");
                _streamWriter.Flush();
            }
        }
    }
}
