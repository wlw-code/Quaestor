using System;
using System.Linq;
using Discord;
using Quaestor.Database.Models;

namespace Quaestor.Services
{
    public class ModerationService
    {
        private readonly IServiceProvider _serviceProvider;

        public ModerationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public int GetPermissionLevel(Guild dbGuild, IGuildUser user)
        {
            if (user.Guild.OwnerId == user.Id)
            {
                return 3;
            }

            var permLevel = 0;

            if (dbGuild.ModRoles != 0)
            {
                foreach (var role in dbGuild.ModRoles.OrderBy(x => x.Value))
                {
                    if (user.Guild.GetRole(ulong.Parse(role.Name)) != null)
                    {
                        if (user.RoleIds.Any(x => x.ToString() == role.Name))
                        {
                            permLevel = role.Value.AsInt32;
                        }
                    }
                }
            }

            return user.GuildPermissions.Administrator && permLevel < 2 ? 2 : permLevel;
        }
    }
}
