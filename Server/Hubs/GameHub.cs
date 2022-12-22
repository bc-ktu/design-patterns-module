using Microsoft.AspNetCore.SignalR;
using Utils.Math;

namespace Server.Hubs
{
    public class GameHub : Hub
    {
        public async Task SendMessage(string player, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", player, message);
        }

        public async Task MapSeed(int x, int y, int level)
        {
            if (Storage.generator.GetValues()?.Any() != true || Storage.UserCount == 0)
            {
                Storage.generator.GenerateSeed(new Vector2(x, y), level);
                Console.WriteLine("Any value");
                await Clients.Client(Context.ConnectionId).SendAsync("GenMap", Storage.generator.GetValues());
            }
            else
            if (Storage.generator.GetMapSize().X != x && Storage.generator.GetMapSize().Y != y && Storage.UserCount == 1)
            {
                Storage.generator.GenerateSeed(new Vector2(x, y), level);
                Console.WriteLine("1st player");
                await Clients.Client(Context.ConnectionId).SendAsync("RegenMap", Storage.generator.GetValues());
            } else if (Storage.UserCount == 2)
            {
                await Clients.Client(Context.ConnectionId).SendAsync("GenMap", Storage.generator.GetValues());
            }
            Console.WriteLine("ELSE");
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


        public async Task Teleport(int localX, int localY, int worldX, int worldY)
        {
            Vector2 temp;

            if (Storage.Players.TryGetValue(Context.ConnectionId, out temp))
            {
                temp.X = worldX;
                temp.Y = worldY;
                Storage.Players[Context.ConnectionId] = temp;
                await Clients.Others.SendAsync("PlayerTeleport", Context.ConnectionId, localX, localY);
            };
        }

        public async Task JoinGame(int X, int Y)
        {
            Storage.Players[Context.ConnectionId] = new Vector2(X, Y);
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
        public async Task PlaceBomb(int fireDamage, int positionX, int positionY)
        {
            foreach (var player in Storage.Players)
            {
                if (player.Key == Context.ConnectionId)
                {
                    continue;
                }
                await Clients.Others.SendAsync("BombPlaced", Context.ConnectionId, fireDamage, positionX, positionY);
            }
        }
        public async Task ChangeStats(int health, int damage)
        {
            foreach (var player in Storage.Players)
            {
                if (player.Key == Context.ConnectionId)
                {
                    continue;
                }
                await Clients.Others.SendAsync("UpdateStats", player.Key, health, damage);
            }
        }
        public async Task GetPlayerCount()
        {
            await Clients.Client(Context.ConnectionId).SendAsync("PlayerCount", Storage.UserCount);
            
        }

        public override async Task OnConnectedAsync()
        {
            Storage.UserCount++;
            Storage.Players.Add(Context.ConnectionId, new Vector2(0, 0));
            /*if (Storage.UserCount == 2)
                Storage.Players.Add(Context.ConnectionId, new Vector2(Storage.generator.GetMapSize().X - 2, Storage.generator.GetMapSize().X - 2));
           */ 
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
