using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text;

namespace Backend.WebSockets
{
    public class WebSocketConnectionManager
    {
        private readonly ConcurrentDictionary<string, WebSocket> _connections = new();

        public void CreateConnection(string id, WebSocket socket)
        {
            _connections.TryAdd(id, socket);
        }

        public void RemoveConnection(string id)
        {
            _connections.TryRemove(id, out _);
        }

        public async Task SendMessage(string message)
        {
            var bytes = Encoding.UTF8.GetBytes(message);

            foreach (var (id, socket) in _connections)
            {
                if (socket.State == WebSocketState.Open)
                {
                    await socket.SendAsync(bytes, WebSocketMessageType.Text, true, CancellationToken.None);
                }
                else
                {
                    _connections.TryRemove(id, out _);
                }
            }
        }
    }
}
