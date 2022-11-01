using Microsoft.AspNetCore.SignalR;
using Utils.GameLogic;
using Utils.Math;

namespace Server.Hubs
{
    public static class Storage
    {
        public static int UserCount { get; set; } = 0;
        public static Dictionary<string, Vector2> Players = new();
        public static MapSeedGen generator; //kaip si rysi atvaizduoti?
    }

    public class GameHub : Hub
    {
        public async Task SendMessage(string player, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", player, message);
        }

        public async Task MapSeed()
        {
            await Clients.Client(Context.ConnectionId).SendAsync("GenMap", Storage.generator.getValues());
        }

        public async Task Move(int x, int y, int speedMod, int speed)
        {
            Vector2 temp;

            if (Storage.Players.TryGetValue(Context.ConnectionId, out temp))
            {
                temp.X = temp.X + (speed + speedMod) * x;
                temp.Y = temp.Y + (speed + speedMod) * y;
                Storage.Players[Context.ConnectionId] = temp;
                await Clients.Others.SendAsync("PlayerMove", Context.ConnectionId, x, y, speedMod, speed);
            };         
        }

        public async Task PlaceExplosive(int x, int y)
        {

        }

        public async Task JoinGame(int X, int Y)
        {
            Storage.Players[Context.ConnectionId] = new Vector2(X,Y);
            await Clients.Others.SendAsync("NewPlayer", Context.ConnectionId, X, Y);
        }

        public async Task GetPlayingPlayers()
        {
            foreach (var player in Storage.Players)
            {
                if (player.Key == Context.ConnectionId)
                {
                    continue;
                }
                await Clients.Client(Context.ConnectionId).SendAsync("NewPlayer", player.Key, player.Value.X, player.Value.Y);
            }
        }

        public override async Task OnConnectedAsync()
        {
            Storage.Players.Add(Context.ConnectionId, new Vector2(0,0));
            Storage.UserCount++;
            //await this.SendMessage($"Player {Storage.UserCount} ", $"Connected {Context.ConnectionId}");
            if (Storage.generator == null)
            {
                Storage.generator = new MapSeedGen(10, 10);
            }
            //possible place 
            Console.WriteLine($"Player connected with ID {Context.ConnectionId}");
            await base.OnConnectedAsync();
        }

        public async override Task OnDisconnectedAsync(Exception exception)
        {
            Storage.UserCount--;
            Storage.Players.Remove(Context.ConnectionId);
            Console.WriteLine($"Player disconnected with ID {Context.ConnectionId}");
            await base.OnDisconnectedAsync(new Exception("Disconnected player"));
        }
    }
}
