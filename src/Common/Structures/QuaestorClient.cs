using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Discord;
using Discord.Net;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Quaestor.Events;
using Quaestor.Services;

namespace Quaestor.Common.Structures
{
    public class QuaestorClient : DiscordSocketClient
    {
        public IServiceProvider ServiceProvider { get; set; }
        public Scribe Scribe { get; set; }
        public Messenger Messenger { get; set; }

        public QuaestorClient(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
            Scribe = serviceProvider.GetRequiredService<Scribe>();
            Messenger = serviceProvider.GetRequiredService<Messenger>();
        }

        public void InitializeTimersAndEvents()
        {
            new Ready(this);
            new MessageReceived(this);
        }

        public async Task TryLoginAsync(string token)
        {
            try
            {
                await LoginAsync(TokenType.Bot, token);
            }
            catch (HttpException exception) when (exception.HttpCode == HttpStatusCode.Unauthorized)
            {
                Scribe.InformOfException("Your Discord client token was invalid. Please check Credentials.json.",
                    exception);
            }
            catch (HttpRequestException exception)
            {
                Scribe.InformOfException("Unable to connect to Discord. Please check your internet connection or Discord server status.", exception);
            }
        }
    }
}
