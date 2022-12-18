using client_graphics.Chain_of_responsibility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client_graphics.Manager
{
    public class GameState
    {
        public static string LogFolder { get; } = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, "OOP_Bomberman_client_graphics_v1\\logs");
        private static readonly GameState _instance = new();
        public Logger Logger { get; private set; }

        static GameState() { }
        private GameState()
        {
            if (!Directory.Exists(LogFolder))
                Directory.CreateDirectory(LogFolder);

            var logPath = Path.Combine(LogFolder, $"{DateTime.Now:D}_log.txt");
            var logger = new StreamWriter(logPath, true);
            Logger = new DefaultLogger(logger);
            Logger.SetNext(new WarningLogger(logger))
                  .SetNext(new ErrorLogger(logger))
                  .SetNext(new NetworkLogger(logger));
        }

        public static GameState Instance
        {
            get
            {
                return _instance;
            }
        }
    }
}
