using Discord.Commands;
using System.Threading.Tasks;

namespace Quaestor.Modules.General
{
    public partial class GeneralModule
    {
        [Command("Echo")]
        [Alias("say", "embed")]
        [Summary("Repeat the provided text in an embedded message.")]
        [Remarks("This is an embedded message!")]
        public async Task Echo([Summary("The text you want the bot to embed.")][Remainder] string message)
        {
            await SendAsync(message);
        }
    }
}