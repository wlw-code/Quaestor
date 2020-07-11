using System;
using Discord.Commands;
using System.Threading.Tasks;
using Discord;
using Quaestor.Common.Extensions.System;

namespace Quaestor.Modules.General
{
    public partial class GeneralModule
    {
        [Command("UserInfo")]
        [Alias("whois", "user", "who")]
        [Summary("View some basic information about a user.")]
        [Remarks("Gibraltar#1044")]
        public async Task Who([Summary("The user you wish to inspect.")] IUser user = null)
        {
            user ??= Context.User;
            
            var activity = "";

            if (user.Activity != null)
            {
                activity = $" {user.Activity.Type.ToString().WithLowercaseFirstCharacter()} {user.Activity.Name}";
            }

            await SendImageAsync(
                $"**Created at:** {user.CreatedAt.Date + user.CreatedAt.TimeOfDay} ({Math.Floor((DateTime.Now - user.CreatedAt).TotalDays)} days old)\n**Status:** {user.Status}{activity}\n**Bot:** {user.IsBot.ToString().WithUppercaseFirstCharacter()}",
                user.GetAvatarUrl(size: 256), $"{user.Username}#{user.Discriminator} ({user.Id})");
        }
    }
}