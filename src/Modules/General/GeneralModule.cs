using System;
using Discord.Commands;
using Microsoft.Extensions.DependencyInjection;
using Quaestor.Common;
using Quaestor.Common.Structures;
using Quaestor.Services;

namespace Quaestor.Modules.General
{
    [Name("General")]
    [Summary("General commands that all users may access.")]
    [RequireContext(ContextType.Guild)]
    public partial class GeneralModule : Module
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly CommandService _commandService;
        private readonly ModerationService _moderationService;

        public GeneralModule(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _commandService = _serviceProvider.GetRequiredService<CommandService>();
            _moderationService = _serviceProvider.GetRequiredService<ModerationService>();
        }
    }
}
