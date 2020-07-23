using System;
using Discord.Commands;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Quaestor.Common.Structures;
using Quaestor.Database.Models;
using Quaestor.Database.Repositories;

namespace Quaestor.Services
{
    public class ServiceManager
    {
        private readonly Credentials _credentials;

        public IServiceProvider ServiceProvider { get; }

        public ServiceManager(CommandService commandService, Credentials credentials, Scribe scribe)
        {
            _credentials = credentials;

            var database = ConfigureDatabase();

            var services = new ServiceCollection()
                .AddSingleton(commandService)
                .AddSingleton(credentials)
                .AddSingleton(scribe)
                .AddSingleton(new Messenger())
                .AddSingleton(database.GetCollection<Guild>("guilds"))
                .AddSingleton(database.GetCollection<Mute>("mutes"))
                .AddSingleton(database.GetCollection<User>("users"))
                .AddSingleton<GuildRepository>()
                .AddSingleton<MuteRepository>()
                .AddSingleton<UserRepository>()
                .AddSingleton<ModerationService>();

            ServiceProvider = services.BuildServiceProvider();
        }

        public IMongoDatabase ConfigureDatabase()
        {
            var mongoClient = new MongoClient(_credentials.DatabaseConnectionString);

            return mongoClient.GetDatabase(_credentials.DatabaseName);
        }
    }
}
