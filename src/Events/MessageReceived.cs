using System;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Quaestor.Common;
using Quaestor.Common.Structures;
using Quaestor.Services;

namespace Quaestor.Events
{
    public class MessageReceived
    {
        private readonly QuaestorClient _client;
        private readonly IServiceProvider _serviceProvider;
        private readonly CommandService _commandService;
        private readonly Messenger _messenger;

        public MessageReceived(QuaestorClient client)
        {
            _client = client;
            _serviceProvider = client.ServiceProvider;
            _commandService = _serviceProvider.GetRequiredService<CommandService>();
            _messenger = _serviceProvider.GetRequiredService<Messenger>();

            _client.MessageReceived += HandleMessageAsync;
        }

        private async Task HandleMessageAsync(SocketMessage socketMessage)
        {
            if (!(socketMessage is SocketUserMessage message)) return;
            if (message.Source != MessageSource.User) return;

            var argPos = 0;
            var context = new Context(_serviceProvider, message, _client);

            if (!(message.Channel is IDMChannel))
            {
                await context.InitializeAsync();

                if (message.Content == $"{_client.CurrentUser.Mention}") await _messenger.SendAsync(context.Channel, $"The prefix for this server is `{context.DbGuild.Prefix}`.\nYou can view available commands with `{context.DbGuild.Prefix}help`.");

                if (!message.HasStringPrefix(context.DbGuild.Prefix, ref argPos)) return;
                if (context.DbGuild.IgnoredChannels.Any(x => x == context.Channel.Id)) return;

                var args = context.Message.Content.Split(' ');
                var commandName = args.First().StartsWith(context.DbGuild.Prefix) ? args.First().Remove(0, context.DbGuild.Prefix.Length) : args[1];

                if (context.DbGuild.DisabledCommands.Any(x => x == commandName.ToLower()))
                {
                    return;
                }

                if (context.DbGuild.CustomCommands.Any())
                {
                    var customCommand = context.DbGuild.CustomCommands.SingleOrDefault(x => x.Name.ToLower() == commandName.ToLower());

                    if (customCommand.Name != null)
                    {
                        await _messenger.SendAsync(context.Channel, customCommand.Value.AsString);
                        return;
                    }
                }
            }

            var result = await _commandService.ExecuteAsync(context, argPos, _serviceProvider);

            if (!result.IsSuccess)
            {
                await _messenger.ReplyErrorAsync(context.User, context.Channel, result.ErrorReason);
            }
        }
    }
}
