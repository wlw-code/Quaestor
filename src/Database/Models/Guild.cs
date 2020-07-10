using MongoDB.Bson;
using Quaestor.Common;

namespace Quaestor.Database.Models
{
    public partial class Guild : Model
    {
        public Guild(ulong guildId)
        {
            GuildId = guildId;
        }

        public ulong GuildId { get; set; }

        // BsonDocuments

        public BsonDocument ModRoles { get; set; } = new BsonDocument();

        public BsonDocument RankRoles { get; set; } = new BsonDocument();

        public BsonDocument CustomCommands { get; set; } = new BsonDocument();

        // Strings

        public string WelcomeMessage { get; set; } = string.Empty;

        public string Prefix { get; set; } = Configuration.Prefix;

        // Numbers

        public int CaseNumber { get; set; } = 1;

        public ulong NewUserRole { get; set; }

        public ulong MutedRoleId { get; set; }

        public ulong ModLogChannelId { get; set; }

        // Arrays

        public string[] DisabledCommands { get; set; } = new string[] { };

        public ulong[] IgnoredChannels { get; set; } = new ulong[] { };
    }
}
