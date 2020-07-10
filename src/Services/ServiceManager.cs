using System;
using Discord.Commands;
using Microsoft.Extensions.DependencyInjection;
using Quaestor.Common.Structures;

namespace Quaestor.Services
{
    public class ServiceManager
    {
        public IServiceProvider ServiceProvider { get; }

        public ServiceManager(CommandService commandService, Credentials credentials, Scribe scribe)
        {
            var services = new ServiceCollection()
                .AddSingleton(commandService)
                .AddSingleton(credentials)
                .AddSingleton(scribe)
                .AddSingleton(new Messenger());

            ServiceProvider = services.BuildServiceProvider();
        }
    }
}
