using System;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Quaestor.Common.Extensions.System;

namespace Quaestor.Modules.General
{
    public partial class GeneralModule
    {
        [Command("RoleInfo")]
        [Alias("getroleinfo", "roleinformation", "getroleinformation", "role")]
        [Summary("Shows information about a given role.")]
        [Remarks("@Member")]
        public async Task RoleInfo(IRole role = null)
        {
            role ??= (Context.User as SocketGuildUser)?.Roles.OrderByDescending(x => x.Position).FirstOrDefault();

            if (role == null)
            {
                await ReplyErrorAsync("you need to provide or have at least one role.");
                return;
            }

            var permissions = "";

            if (!role.Permissions.Administrator)
            {
                permissions = role.Permissions.ToList().Aggregate(permissions, (current, permission) => current + $"{permission},\n");
            }
            else
            {
                permissions += "Administrator,\n";
            }

            await SendAsync($"**Created at:** {role.CreatedAt.Date + role.CreatedAt.TimeOfDay} ({Math.Floor((DateTime.Now - role.CreatedAt).TotalDays)} days old)\n**Color:** {role.Color}\n**Mentionable:** {role.IsMentionable.ToString().WithUppercaseFirstCharacter()}\n**Position:** {role.Position}\n**Permissions:**\n{permissions.Remove(permissions.Length - 2)}.", $"{role.Name} ({role.Id})", role.Color.ToString() != "#0" ? role.Color : (Color?)null);
        }
    }
}
