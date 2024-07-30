using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace MessageBroker
{
    public class MessageHub : Hub
    {
        private static ConcurrentDictionary<string, string> ConnectedClients = new();

        public override Task OnConnectedAsync()
        {
            ConnectedClients.TryAdd(Context.ConnectionId, Context.ConnectionId);
            Clients.All.SendAsync("ClientList", ConnectedClients.Values);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            ConnectedClients.TryRemove(Context.ConnectionId, out _);
            Clients.All.SendAsync("ClientList", ConnectedClients.Values);
            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("Message", message);
        }

        public async Task SendMessageTo(string message, string to)
        {
            await Clients.All.SendAsync(to, message);
        }
    }
}
