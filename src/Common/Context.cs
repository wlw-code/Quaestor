using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Quaestor.Database.Models;
using Quaestor.Database.Repositories;

namespace Quaestor.Common
{
    public class Context : SocketCommandContext
    {
        public User DbUser { get; private set; }
        public Guild DbGuild { get; private set; }
        public IGuildUser GuildUser { get; }
        //public DiscordSocketClient Client { get; set; }

        private readonly UserRepository _userRepository;
        private readonly GuildRepository _guildRepository;

        public Context(IServiceProvider serviceProvider, SocketUserMessage message, DiscordSocketClient client) :
            base(client, message)
        {
            _userRepository = serviceProvider.GetRequiredService<UserRepository>();
            _guildRepository = serviceProvider.GetRequiredService<GuildRepository>();

            GuildUser = User as IGuildUser;
        }

        public async Task InitializeAsync()
        {
            DbGuild = await _guildRepository.GetGuildAsync(Guild.Id);
            DbUser = await _userRepository.GetUserAsync(GuildUser.Id, GuildUser.GuildId);
        }
    }
}
