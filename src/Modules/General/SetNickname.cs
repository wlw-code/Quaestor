using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace Quaestor.Modules.General
{
    public partial class GeneralModule
    {
        [Command("SetNick")]
        [Alias("nick", "changenick", "updatenick")]
        [Summary("Changes the nickname of a specified user.")]
        [Remarks("@Franklin#0146 A GOOD KID")]
        public async Task SetNick(IGuildUser user, [Remainder] string nick = null)
        {
            if (user.Id != Context.User.Id)
            {
                if (user.Id != Context.Client.CurrentUser.Id)
                {
                    var permissionLevel = _moderationService.GetPermissionLevel(Context.DbGuild, user);

                    if (permissionLevel > 0)
                    {
                        var position = permissionLevel switch
                        {
                            1 => "a moderator",
                            2 => "an administrator",
                            _ => "an owner"
                        };

                        await ReplyErrorAsync(
                            $"**{user.Mention}** is {position}, so you cannot change their nickname.");
                        return;
                    }
                }
            }

            try
            {
                await user.ModifyAsync(x => x.Nickname = nick);

                var message = $"you have successfully set **{user.Mention}**'s nickname to ";

                message += nick == null ? "nothing" : $"{nick}";

                await ReplyAsync(message + ".");
            }
            catch
            {
                await ReplyErrorAsync($"I cannot change **{user.Mention}**'s nickname, likely due to insufficient permission or their highest role being higher than mine.");
            }
        }
    }
}
