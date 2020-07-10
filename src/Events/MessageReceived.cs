using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Quaestor.Common;
using Quaestor.Common.Structures;

namespace Quaestor.Events
{
    public class MessageReceived
    {
        private readonly QuaestorClient _client;
        private readonly IServiceProvider _serviceProvider;
        private readonly CommandService _commandService;

        public MessageReceived(QuaestorClient client)
        {
            _client = client;
            _serviceProvider = client.ServiceProvider;
            _commandService = _serviceProvider.GetRequiredService<CommandService>();

            _client.MessageReceived += HandleMessageAsync;
        }

        private async Task HandleMessageAsync(SocketMessage socketMessage)
        {
            if (!(socketMessage is SocketUserMessage message)) return;
            if (message.Source != MessageSource.User) return;

            var argPos = 0;

            if (!message.HasStringPrefix(Configuration.Prefix, ref argPos)) return;

            var context = new SocketCommandContext(_client, message);
            var result = await _commandService.ExecuteAsync(context, argPos, _serviceProvider);

            if (!result.IsSuccess)
            {
                if (result.Error == CommandError.UnknownCommand)
                {
                    return;
                }

                await context.Channel.SendMessageAsync(result.ErrorReason);
            }
        }
    }
}
