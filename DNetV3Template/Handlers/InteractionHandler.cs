using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using System.Reflection;

namespace DNetV3Template.Handlers
{
    internal class InteractionHandler : IHandler
    {
        private readonly DiscordSocketClient _client;
        private readonly InteractionService _commands;
        private readonly IServiceProvider _services;
        private readonly Logger _logger;

        public InteractionHandler(DiscordSocketClient client, InteractionService commands, IServiceProvider services, Logger logger)
        {
            _client = client;
            _commands = commands;
            _services = services;
            _logger = logger;
        }

        public async Task InitializeAsync()
        {
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
            _logger.Info($"Registered {_commands.SlashCommands.Count} command{(_commands.SlashCommands.Count != 1 ? "s" : "")}");

            _client.InteractionCreated += HandleInteraction;

            _commands.SlashCommandExecuted += SlashCommandExecuted;
            _commands.ContextCommandExecuted += ContextCommandExecuted;
            _commands.ComponentCommandExecuted += ComponentCommandExecuted;
        }

        private async Task HandleInteraction(SocketInteraction interaction)
        {
            // Create interaction context.
            var Context = new SocketInteractionContext(_client, interaction);
            await _commands.ExecuteCommandAsync(Context, _services);
        }

        private Task SlashCommandExecuted(SlashCommandInfo info, IInteractionContext context, IResult result)
        {
            return Task.CompletedTask;
        }

        private Task ContextCommandExecuted(ContextCommandInfo info, IInteractionContext context, IResult result)
        {
            return Task.CompletedTask;
        }

        private Task ComponentCommandExecuted(ComponentCommandInfo info, IInteractionContext context, IResult result)
        {
            return Task.CompletedTask;
        }
    }
}