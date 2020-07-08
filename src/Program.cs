using System;
using System.IO;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Newtonsoft.Json;
using Quaestor.Common.Extensions;
using Quaestor.Common.Structures;

namespace Quaestor
{
    internal class Program
    {
        static void Main() => new Program().StartAsync().GetAwaiter().GetResult();

        private async Task StartAsync()
        {
            var scribe = new Scribe();

            Credentials credentials;

            try
            {
                credentials = JsonConvert.DeserializeObject<Credentials>(await File.ReadAllTextAsync(
                    AppContext.BaseDirectory.Substring(0, AppContext.BaseDirectory.IndexOf("src")) +
                    "src/Credentials.json"));
            }
            catch (IOException exception)
            {
                scribe.InformOfException(
                    "An exception occurred while loading Credentials.json (the src/Credentials.json file was not found or was not properly formatted.", exception);
                return;
            }

            scribe.Inform("Credentials initialized.");

            var client = new DiscordSocketClient();

            scribe.Inform("Client initialized.");

            var commandService = new CommandService(new CommandServiceConfig
            {
                DefaultRunMode = RunMode.Async
            });

            scribe.Inform("CommandService initialized.");

            await client.LoginAsync(TokenType.Bot, credentials.Token);
            await client.StartAsync();

            await Task.Delay(-1);
        }
    }
}
