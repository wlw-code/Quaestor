using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Microsoft.Extensions.DependencyInjection;
using Quaestor.Services;

namespace Quaestor.Common
{
    public abstract class Module : ModuleBase
    {
        private readonly Messenger _messenger;

        protected Module(IServiceProvider serviceProvider)
        {
            _messenger = serviceProvider.GetRequiredService<Messenger>();
        }

        public async Task ReplyAsync(string message)
        {
            await _messenger.ReplyAsync(Context.User, Context.Channel, message);
        }

        public async Task ReplyErrorAsync(string message)
        {
            await _messenger.ReplyErrorAsync(Context.User, Context.Channel, message);
        }

        public async Task SendAsync(string message, string title = null, Color? color = null, string imageUrl = null)
        {
            await _messenger.SendAsync(Context.Channel, message, title, color, imageUrl);
        }

        public async Task DmAsync(IUser user, string message, string title = null, Color? color = null, string imageUrl = null)
        {
            var userDm = await user.GetOrCreateDMChannelAsync();

            await _messenger.SendAsync(userDm, message, title, color, imageUrl);
        }

        public async Task SendImageAsync(string message, string imageUrl)
        {
            await _messenger.SendImageAsync(Context.Channel, message, imageUrl);
        }
    }
}
