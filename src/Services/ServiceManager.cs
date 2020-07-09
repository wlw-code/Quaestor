using System;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Quaestor.Common.Extensions;
using Quaestor.Common.Structures;

namespace Quaestor.Services
{
    public class ServiceManager
    {
        private readonly CommandService _commandService;
        private readonly Credentials _credentials;
        private readonly Scribe _scribe;

        public IServiceProvider ServiceProvider { get; }

        public ServiceManager(DiscordSocketClient client, CommandService commandService, Credentials credentials, Scribe scribe)
        {
            _commandService = commandService;
            _credentials = credentials;
            _scribe = scribe;

            var services = new ServiceCollection()
                .AddSingleton(_commandService)
                .AddSingleton(_credentials)
                .AddSingleton(_scribe);

            ServiceProvider = services.BuildServiceProvider();
        }
    }
}
