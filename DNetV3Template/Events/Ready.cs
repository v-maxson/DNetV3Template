namespace DNetV3Template.Handlers
{
    internal partial class EventHandler
    {
        private async Task Ready()
        {
            _logger.Info("Connected...");

            // Register SlashCommands
            if (_config.RegisterCommandsGlobally)
            {
                await _commands.RegisterCommandsGloballyAsync(true);
            }
            else
            {
                await _commands.RegisterCommandsToGuildAsync(_config.MainGuild, true);
            }
        }
    }
}