using System;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Quaestor.Common.Extensions;
using Quaestor.Events;

namespace Quaestor.Common.Structures
{
    public class QuaestorClient : DiscordSocketClient
    {
        public IServiceProvider ServiceProvider { get; set; }
        public Scribe Scribe { get; set; }

        public QuaestorClient(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
            Scribe = serviceProvider.GetRequiredService<Scribe>();
        }

        public void InitializeTimersAndEvents()
        {
            new Ready(this);
            new MessageReceived(this);
        }
    }
}
