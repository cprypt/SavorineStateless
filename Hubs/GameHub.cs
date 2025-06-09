using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Savorine.AsyncServer.Hubs
{
    public class GameHub : Hub
    {
        public async Task JoinRoom(string roomName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
            await Clients.Group(roomName)
                         .SendAsync("PlayerJoined", Context.ConnectionId);
        }

        public async Task LeaveRoom(string roomName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
            await Clients.Group(roomName)
                         .SendAsync("PlayerLeft", Context.ConnectionId);
        }

        public async Task BroadcastPlayerState(string roomName, string stateJson)
        {
            await Clients.OthersInGroup(roomName)
                         .SendAsync("ReceivePlayerState", Context.ConnectionId, stateJson);
        }

        public async Task DistributeItems(string roomName, string itemJson)
        {
            await Clients.Group(roomName)
                         .SendAsync("DistributeItems", itemJson);
        }
    }
}
