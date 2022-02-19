using Discord.Interactions;
using Discord.WebSocket;

namespace DNetV3Template.Handlers
{
    internal partial class EventHandler : IHandler
    {
        private readonly DiscordSocketClient _client;
        private readonly Logger _logger;
        private readonly Config _config;
        private readonly InteractionService _commands;

        public EventHandler(
            DiscordSocketClient client,
            Logger logger,
            Config config,
            InteractionService commands)
        {
            _client = client;
            _logger = logger;
            _config = config;
            _commands = commands;
        }

        public Task InitializeAsync()
        {
            _client.Ready += Ready;

            return Task.CompletedTask;
        }
    }
}