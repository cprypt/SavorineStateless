using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Savorine.AsyncServer.Hubs
{
    public class GameHub : Hub
    {
        /// <summary>
        /// 클라이언트가 룸(멀티플레이 세션)에 입장할 때 호출
        /// </summary>
        public async Task JoinRoom(string roomName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
            await Clients.Group(roomName)
                         .SendAsync("PlayerJoined", Context.ConnectionId);
        }

        /// <summary>
        /// 클라이언트가 룸에서 나갈 때 호출
        /// </summary>
        public async Task LeaveRoom(string roomName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
            await Clients.Group(roomName)
                         .SendAsync("PlayerLeft", Context.ConnectionId);
        }

        /// <summary>
        /// 클라이언트 상태(위치, 애니메이션 등)를 룸 내 다른 플레이어에게 브로드캐스트
        /// </summary>
        public async Task BroadcastPlayerState(string roomName, object state)
        {
            await Clients.OthersInGroup(roomName)
                         .SendAsync("ReceivePlayerState", Context.ConnectionId, state);
        }
    }
}