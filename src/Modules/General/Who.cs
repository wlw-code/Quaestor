using Discord.Commands;
using System.Threading.Tasks;
using Discord;
using Quaestor.Common.Extensions.System;

namespace Quaestor.Modules.General
{
    public partial class GeneralModule
    {
        [Command("Who")]
        [Alias("whois", "user", "userinfo")]
        [Summary("View some basic information about a user.")]
        [Remarks("Gibraltar#1044")]
        public async Task Who([Summary("The user you wish to inspect.")] IUser user = null)
        {
            user ??= Context.User;
            
            var activity = "";

            if (user.Activity != null)
            {
                activity = $" in {user.Activity.Name} for {user.Activity.Details}";
            }

            await SendImageAsync(
                $"**Created at:** {user.CreatedAt.Date + user.CreatedAt.TimeOfDay}\n**Status:** {user.Status}{activity}\n**Bot:** {user.IsBot.ToString().WithUppercaseFirstCharacter()}",
                user.GetAvatarUrl(size: 256), $"{user.Username}#{user.Discriminator} ({user.Id})");
        }
    }
}