﻿using Discord;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;
using Quaestor.Database.Models;

namespace Quaestor.Database.Repositories
{
    public sealed class UserRepository : BaseRepository<User>
    {
        public UserRepository(IMongoCollection<User> users) : base(users) { }

        public async Task<User> GetUserAsync(ulong userId, ulong guildId)
        {
            var dbUser = await GetAsync(x => x.UserId == userId && x.GuildId == guildId);

            if (dbUser != null) return dbUser;
            
            var createdUser = new User(userId, guildId);

            await InsertAsync(createdUser);

            return createdUser;

        }

        public async Task ModifyUserAsync(IGuildUser user, Action<User> function)
        {
            await ModifyAsync(await GetUserAsync(user.Id, user.GuildId), function);
        }
    }
}