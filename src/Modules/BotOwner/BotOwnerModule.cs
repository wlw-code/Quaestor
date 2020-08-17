using System;
using Discord.Commands;
using Microsoft.Extensions.DependencyInjection;
using Quaestor.Common;
using Quaestor.Services;

namespace Quaestor.Modules.BotOwner
{
    [Name("BotOwner")]
    [Summary("Commands that only the owners of this bot can use.")]
    [RequireContext(ContextType.Guild)]
    public partial class BotOwnerModule : Module
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ModerationService _moderationService;

        public BotOwnerModule(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _moderationService = _serviceProvider.GetRequiredService<ModerationService>();
        }
    }
}