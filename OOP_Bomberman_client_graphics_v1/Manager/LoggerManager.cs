using client_graphics.Chain_of_responsibility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client_graphics.Manager
{
    public class LoggerManager
    {
        public static string LogFolder { get; } = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, "OOP_Bomberman_client_graphics_v1\\logs");
        public Logger Logger { get; set; }

        static LoggerManager() { }
        public LoggerManager(string id)
        {
            if (!Directory.Exists(LogFolder))
                Directory.CreateDirectory(LogFolder);

            var logPath = Path.Combine(LogFolder, $"{id}{DateTime.Now:D}_log.txt");
            var logger = new StreamWriter(logPath, true);
            Logger = new NetworkLogger(logger);
            Logger.SetNext(new WarningLogger(logger))
                  .SetNext(new ErrorLogger(logger))
                  .SetNext(new DefaultLogger(logger));
        }
    }
}
