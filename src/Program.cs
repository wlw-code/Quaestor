using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Newtonsoft.Json;
using Quaestor.Common.Extensions;
using Quaestor.Common.Structures;
using Quaestor.Services;

namespace Quaestor
{
    internal class Program
    {
        private static void Main() => StartAsync().GetAwaiter().GetResult();

        private static async Task StartAsync()
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

            var commandService = new CommandService();
            var serviceManager = new ServiceManager(new DiscordSocketClient(), commandService, credentials, scribe);
            var serviceProvider = serviceManager.ServiceProvider;
            var quaestorClient = new QuaestorClient(serviceProvider);

            quaestorClient.InitializeTimersAndEvents();

            await commandService.AddModulesAsync(Assembly.GetEntryAssembly(), serviceProvider);
            await quaestorClient.LoginAsync(TokenType.Bot, credentials.Token);
            await quaestorClient.StartAsync();

            await Task.Delay(-1);
        }
    }
}
