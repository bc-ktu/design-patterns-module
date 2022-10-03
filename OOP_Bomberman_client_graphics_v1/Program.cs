using Microsoft.AspNetCore.SignalR.Client;
using OOP_Bomberman_client_graphics_v1.SignalR;

namespace OOP_Bomberman_client_graphics_v1
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
            Con.Connection.On<string, int, int>("PlayerMove", (uuid, X, Y) =>
            {
                game.UpdatePostion(uuid, X, Y);
            });

            Application.Run(game);
        }
    }
}