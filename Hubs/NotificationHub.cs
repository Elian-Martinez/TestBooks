using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using TestBooks.Models;

namespace TestBooks.Hubs
{
    public class NotificationHub:Hub
    {
        public async Task ReceiveNotification(Notification notification)
        {
            await Clients.All.SendAsync("ReceiveNotification", notification);
        }
    }
}
