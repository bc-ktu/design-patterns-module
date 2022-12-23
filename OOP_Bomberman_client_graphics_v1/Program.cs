using Microsoft.AspNetCore.SignalR.Client;

using client_graphics.SignalR;
using Utils.Math;

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
            int playerCount = 0;
            SignalRConnection Con = new SignalRConnection("http://localhost:5016/GameHub");
            Con.ConnectToServer();
            List<int> GameSeed = new List<int>();
            Con.Connection.InvokeAsync("MapSeed", 14, 14, 2);
            Con.Connection.On<List<int>>("GenMap", (seed) =>
            {
                GameSeed = seed;
            });

            while (GameSeed.Count == 0)
            {
                Thread.Sleep(10);
                if (GameSeed.Count > 0)
                {
                    break; 
                }
            }
            Con.Connection.InvokeAsync("GetPlayerCount");
            Con.Connection.On<int>("PlayerCount", (count) =>
            {
                playerCount = count;
            });

            while (playerCount == 0)
            {
                Thread.Sleep(10);
                if (playerCount > 0)
                {
                    break; 
                }
            }
            ApplicationConfiguration.Initialize();

            game = new GameView();
            game.Con = Con;
            game.playersCount = playerCount;
            game.GameStartUp(GameSeed);

            Con.Connection.InvokeAsync("GetPlayingPlayers");

            Con.Connection.On<string, int, int>("NewPlayer", (uuid, X, Y)=>
            {
                game.AddPlayer(uuid, X, Y);
            });
            Con.Connection.On<string, int, int, int, int>("PlayerMove", (uuid, X, Y, speedMod, speed) =>
            {
                game.UpdatePosition(uuid, X, Y, speedMod, speed);
            });
            Con.Connection.On<string, int, int>("PlayerTeleport", (uuid, X, Y) =>
            {
                game.TeleportPlayer(uuid, X, Y);
            });
            Con.Connection.On<string, int, int, int>("BombPlaced", (uuid, fireDamage, X, Y) =>
            {
                game.BombPlaced(uuid, fireDamage, X, Y);
            });
            Con.Connection.On<string, int, int>("UpdateStats", (uuid, health, damage) =>
            {
                game.UpdateOtherPlayerStats(uuid, health, damage);
            });
            Con.Connection.On<string, int, int>("UpdatePowerups", (uuid, x, y) =>
            {
                game.UpdatePowerups(x, y);
            });
            Con.Connection.On<string>("Death", (uuid) =>
            {
                game.PlayerDied(uuid);
            });

            Application.Run(game);
        }
    }
}