using System;
using Discord.Commands;
using Quaestor.Common;

namespace Quaestor.Modules.General
{
    [Name("General")]
    [Summary("General commands that all users may access.")]
    [RequireContext(ContextType.Guild)]
    public partial class GeneralModule : Module
    {
        private readonly IServiceProvider _serviceProvider;

        public GeneralModule(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
    }
}
