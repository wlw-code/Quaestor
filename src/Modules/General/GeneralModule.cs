using System;
using Discord.Commands;
using Microsoft.Extensions.DependencyInjection;
using Quaestor.Common;

namespace Quaestor.Modules.General
{
    [Name("General")]
    [Summary("General commands that all users may access.")]
    [RequireContext(ContextType.Guild)]
    public partial class GeneralModule : Module
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly CommandService _commandService;

        public GeneralModule(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _commandService = _serviceProvider.GetRequiredService<CommandService>();
        }
    }
}
