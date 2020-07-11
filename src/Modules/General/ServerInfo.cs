using System;
using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;

namespace Quaestor.Modules.General
{
    public partial class GeneralModule
    {
        [Command("ServerInfo")]
        [Alias("serverinformation", "guildinfo", "guildinformation")]
        [Summary("View information about the server this command is ran in.")]
        public async Task ServerInfo()
        {
            var quaestorInfo = "";

            quaestorInfo += $"**Moderation cases:** {Context.DbGuild.CaseNumber}\n";

            if (Context.DbGuild.CustomCommands.Any())
            {
                var customCommands = Context.DbGuild.CustomCommands.Aggregate("", (current, command) => current + $"{Context.DbGuild.Prefix}{command.Name} returns \"{command.Value}\",\n");

                quaestorInfo += "**Custom commands:**\n" + customCommands.Remove(customCommands.Length - 2) + "\n";
            }

            if (Context.DbGuild.DisabledCommands.Any())
            {
                var disabledCommands = Context.DbGuild.DisabledCommands.Select(command => _commandService.Commands.SingleOrDefault(x => x.Name.ToLower() == command.ToLower())).Aggregate("", (current, foundCommand) => current + (foundCommand == null ? null : $"{Context.DbGuild.Prefix}{foundCommand.Name},\n"));

                quaestorInfo += "**Disabled commands:**\n" + disabledCommands.Remove(disabledCommands.Length - 2) + "\n";
            }

            if (Context.DbGuild.IgnoredChannels.Any())
            {
                var ignoredChannels = "";

                foreach (var channel in Context.DbGuild.IgnoredChannels)
                {
                    try
                    {
                        var foundChannel = Context.Guild.GetChannel(channel);

                        ignoredChannels += $"#{foundChannel.Name},\n";
                    }
                    catch
                    {
                        // ignored
                    }
                }

                quaestorInfo += "**Ignored channels:**\n" + ignoredChannels.Remove(ignoredChannels.Length - 2) + "\n";
            }

            try
            {
                var logChannel = Context.Guild.GetChannel(Context.DbGuild.ModLogChannelId);

                quaestorInfo += $"**Mod log channel:** #{logChannel.Name}\n";
            }
            catch
            {
                // ignored
            }

            if (Context.DbGuild.ModRoles.Any())
            {
                var modRoles = Context.DbGuild.ModRoles.Aggregate("", (current, role) => current + $"**@{role.Name}**\n");

                quaestorInfo += "**Mod roles:**\n" + modRoles.Remove(modRoles.Length - 2) + "\n";
            }

            try
            {
                var mutedRole = Context.Guild.GetRole(Context.DbGuild.MutedRoleId);

                quaestorInfo += $"**Muted role:** {mutedRole.Mention}\n";
            }
            catch
            {
                // ignored
            }

            try
            {
                var newUserRole = Context.Guild.GetRole(Context.DbGuild.NewUserRole);

                quaestorInfo += $"**New user role:** {newUserRole.Mention}\n";
            }
            catch
            {
                // ignored
            }

            if (Context.DbGuild.WelcomeMessage.Length > 0) quaestorInfo += $"**Welcome message:** {Context.DbGuild.WelcomeMessage}\n";

            await SendAsync($"**Created at:** {Context.Guild.CreatedAt.DateTime + Context.Guild.CreatedAt.TimeOfDay} ({Math.Floor((DateTime.Now - Context.Guild.CreatedAt).TotalDays)} days old)\n**Member count:** {Context.Guild.MemberCount}\n**Owner:** {Context.Guild.Owner.Mention} {(quaestorInfo == "" ? null : "\n\n" + quaestorInfo)}", $"{Context.Guild.Name} ({Context.Guild.Id})");
        }
    }
}
