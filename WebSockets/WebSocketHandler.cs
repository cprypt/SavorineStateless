using System.Net.WebSockets;
using System.Text;
using System.Collections.Concurrent;

namespace Savorine.AsyncServer.WebSockets
{
    public static class WebSocketHandler
    {
        private static ConcurrentDictionary<string, WebSocket> clients = new();

        public static async Task HandleSocketAsync(WebSocket socket)
        {
            var id = Guid.NewGuid().ToString();
            clients[id] = socket;

            var buffer = new byte[1024 * 4];

            while (socket.State == WebSocketState.Open)
            {
                var result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                if (result.MessageType == WebSocketMessageType.Close)
                    break;

                var msg = Encoding.UTF8.GetString(buffer, 0, result.Count);
                Console.WriteLine($"[{id}] → {msg}");

                // 브로드캐스트
                foreach (var other in clients)
                {
                    if (other.Key == id) continue;
                    await other.Value.SendAsync(
                        new ArraySegment<byte>(Encoding.UTF8.GetBytes(msg)),
                        WebSocketMessageType.Text,
                        true,
                        CancellationToken.None
                    );
                }
            }

            clients.TryRemove(id, out _);
            await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
            Console.WriteLine($"[{id}] 연결 종료됨");
        }
    }

}