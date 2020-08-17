using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace Quaestor.Modules.BotOwner
{
    public partial class BotOwnerModule
    {
        [Command("TestCommand")]
        [Alias("test")]
        [Summary("A test command that never stays the same.")]
        [Remarks("")]
        public async Task TestCommand([Remainder] IGuildUser user = null)
        {
            user ??= Context.GuildUser;
            
            var permissionLevel = _moderationService.GetPermissionLevel(Context.DbGuild, user);

            await ReplyAsync($"The permission level of {user.Mention} is {permissionLevel}");
        }
    }
}
