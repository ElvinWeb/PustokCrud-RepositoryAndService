using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using PustokMVC.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PustokMVC.Business.Hubs
{
    public class ChatHub : Hub
    {
        private readonly UserManager<User> _userManager;
        public ChatHub(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            if (Context.User.Identity.IsAuthenticated)
            {
                var user = await _userManager.FindByNameAsync(Context.User.Identity.Name);

                if (user is not null)
                {
                    user.ConnectionId = null;
                    await _userManager.UpdateAsync(user);
                    await Clients.All.SendAsync("DisConnect", user.Id);
                }
            }
        }
        public override async Task OnConnectedAsync()
        {
            if (Context.User.Identity.IsAuthenticated)
            {
                var user = await _userManager.FindByNameAsync(Context.User.Identity.Name);

                if (user is not null)
                {
                    user.ConnectionId = Context.ConnectionId;
                    await _userManager.UpdateAsync(user);
                    await Clients.All.SendAsync("OnConnect", user.Id);
                }
            }

        }
    }
}
