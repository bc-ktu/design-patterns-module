using Microsoft.AspNetCore.SignalR.Client;

using client_graphics.SignalR;
using Utils.GameObjects.Animates;
using Microsoft.AspNetCore.SignalR.Protocol;

namespace client_graphics
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.

            GameView game;
            SignalRConnection Con = new SignalRConnection("http://localhost:5016/GameHub");
            Con.ConnectToServer();
            List<int> GameSeed = new List<int>();

            Con.Connection.InvokeAsync("SendMessage", "player", "message");
            Con.Connection.On<string, string>("ReceiveMessage", (a1, a2) => { });
            Con.Connection.On<HubMessage[]>("welcome", (messages) => {
                return;
            });


            Con.Connection.InvokeAsync("MapSeed");
            Con.Connection.On<List<int>>("GenMap", (seed) =>
            {
                GameSeed = seed;
            });


            //Wait for result, fix later.
            while (GameSeed.Count == 0)
            {
                Thread.Sleep(10);
                if (GameSeed.Count > 0)
                {
                    break; 
                }
            }
            ApplicationConfiguration.Initialize();

            game = new GameView();
            game.Con = Con;
            game.GameStartUp(GameSeed);

            Con.Connection.InvokeAsync("GetPlayingPlayers");

            Con.Connection.On<string, int, int>("NewPlayer", (uuid, X, Y) =>
            {
                game.AddPlayer(uuid, X, Y);
            });
            Con.Connection.On<string, int, int, int, int>("PlayerMove", (uuid, X, Y, speedMod, speed) =>
            {
                game.UpdatePosition(uuid, X, Y, speedMod, speed);
            });

            Application.Run(game);
        }
    }
}