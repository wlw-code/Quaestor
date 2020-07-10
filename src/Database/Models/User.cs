namespace Quaestor.Database.Models
{
    public partial class User : Model
    {
        public User(ulong userId, ulong guildId)
        {
            UserId = userId;
            GuildId = guildId;
        }

        // Numbers

        public ulong UserId { get; set; }

        public ulong GuildId { get; set; }

        // Bools

        public bool HasBeenMuted { get; set; } = false;

        public bool HasBeenKicked { get; set; } = false;

        public bool HasBeenBanned { get; set; } = false;
    }
}