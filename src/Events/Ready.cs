using System.Threading.Tasks;
using Quaestor.Common;
using Quaestor.Common.Structures;

namespace Quaestor.Events
{
    public class Ready
    {
        private readonly QuaestorClient _client;

        public Ready(QuaestorClient client)
        {
            _client = client;

            _client.Ready += HandleReadyAsync;
        }

        private async Task HandleReadyAsync()
        {
            await _client.SetGameAsync(Configuration.Game);

            _client.Scribe.Inform($"{_client.CurrentUser.Username}#{_client.CurrentUser.Discriminator} ({_client.CurrentUser.Id}) v{Configuration.Version} has connected to Discord.");
        }
    }
}
