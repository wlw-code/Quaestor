using System.Threading.Tasks;
using Discord;
using Quaestor.Common;

namespace Quaestor.Services
{
    public class Messenger
    {
        public async Task SendAsync(IMessageChannel channel, string message, string title = null, Color? color = null, string imageUrl = null)
        {
            var builder = new EmbedBuilder
            {
                Description = message,
                Title = title,
                Color = color ?? Configuration.GetRandomColor(),
                ImageUrl = imageUrl
            };

            await channel.SendMessageAsync("", false, builder.Build());
        }

        public async Task ReplyAsync(IUser user, IMessageChannel channel, string message, string title = null, Color? color = null)
        {
            await SendAsync(channel, user.Mention + ", " + message, title, color);
        }

        public async Task ReplyErrorAsync(IUser user, IMessageChannel channel, string message)
        {
            await ReplyAsync(user, channel, message, null, Configuration.ErrorColor);
        }

        public async Task SendImageAsync(IMessageChannel channel, string message, string imageUrl, string title = null)
        {
            await SendAsync(channel, message, title, null, imageUrl);
        }
    }
}
