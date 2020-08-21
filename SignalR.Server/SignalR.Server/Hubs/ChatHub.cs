using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace SignalR.Server.Hubs
{
    public class ChatHub : Hub
    {
        //All: Todos van a recibir el mensaje
        //Caller: El emisor es el que recibirá el mensaje
        //Others: Todos excepto el emisor recibirán el mensaje
        //Group: Todos los miembros de un grupo dado recibirán el mensaje
        //Client: Un conjunto de personas especificas recibirán el mensaje
        //User: Un conjunto de usuarios registrador en tu página recibirá el mensaje

        public async Task SendMessageAll(string user, string message)
            => await Clients.All.SendAsync("ReceiveMessageAll", user, message);
        
        public async Task SendMessageCaller(string user, string message)
            => await Clients.Caller.SendAsync("ReceiveMessageCaller", user, message);
        
        public async Task SendMessageOthers(string user, string message)
            => await Clients.Others.SendAsync("ReceiveMessageOthers", user, message);

        public async Task SendMessageToGroup(string user, string message, string group)
            => await Clients.Group(group).SendAsync("ReceiveMessageGroup", user, message);
 
        public async Task SendMessageToClient(string user, string message, string group)
            => await Clients.Client(group).SendAsync("ReceiveMessageClient", user, message);
        
        public async Task SendMessageToUser(string user, string message, string group)
            => await Clients.User(group).SendAsync("ReceiveMessageUser", user, message);
        
        public async Task AddToGroup(string group)
            => await Groups.AddToGroupAsync(Context.ConnectionId, group);
    
        public async Task AddToGroupSendMessage(string user, string message,string group)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, group);
            await SendMessageToGroup(user, message, group);
        }

        public override async Task OnConnectedAsync() => await base.OnConnectedAsync();
        
        public override async Task OnDisconnectedAsync(Exception exception)
            => await base.OnDisconnectedAsync(exception);
        
        public async Task OnDisconnectedAsynFromGroup(string group)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, group);
            Exception ex = new Exception();
            await base.OnDisconnectedAsync(ex);
        }
    }
}