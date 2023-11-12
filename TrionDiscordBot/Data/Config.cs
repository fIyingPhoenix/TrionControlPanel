using DSharpPlus;
using DSharpPlus.CommandsNext;

namespace TrionDiscordBot.Data
{
    public class Config
    {
        public static DiscordClient DiscordClient { get; set; }
        public static CommandsNextExtension Commands { get; set; }

    }
}
