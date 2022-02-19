// This is the entry point for your bot.
// Most of your bots configuration will occur here.

using Discord.WebSocket;
using Discord.Interactions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Discord;
using DNetV3Template.Handlers;

namespace DNetV3Template
{
    public class Program
    {
        private static DiscordSocketClient _client;
        private static Logger _logger;

        public static void Main()
        {
            using IHost _host = Host.CreateDefaultBuilder()
                .ConfigureServices((_, services) =>
                {
                    services
                        .AddSingleton(x =>
                        {
                            var SocketConfig = new DiscordSocketConfig
                            {
                                GatewayIntents =
                                    GatewayIntents.Guilds |
                                    GatewayIntents.GuildMessages,
                                AlwaysDownloadUsers = true
                            };

                            return new DiscordSocketClient(SocketConfig);
                        })
                        .AddTransient<Logger>() // A new instance should be created upon each injection.
                        .AddSingleton(x => Config.LoadConfig())
                        .AddSingleton(x => new InteractionService(x.GetRequiredService<DiscordSocketClient>()))
                        .AddSingleton<InteractionHandler>()
                        .AddSingleton<Handlers.EventHandler>();
                })
                .Build();

            MainAsync(_host).GetAwaiter().GetResult();
        }

        public static async Task MainAsync(IHost host)
        {
            using IServiceScope ServiceScope = host.Services.CreateScope();
            IServiceProvider ServiceProvider = ServiceScope.ServiceProvider;

            var commands = ServiceProvider.GetRequiredService<InteractionService>();
            _client = ServiceProvider.GetRequiredService<DiscordSocketClient>();
            _logger = ServiceProvider.GetRequiredService<Logger>();
            var config = ServiceProvider.GetRequiredService<Config>();

            _logger.Info("Initializing Interaction Handler...");
            await ServiceProvider.GetRequiredService<InteractionHandler>().InitializeAsync();

            _logger.Info("Initializing Client Events...");
            await ServiceProvider.GetRequiredService<Handlers.EventHandler>().InitializeAsync();

            _logger.Info("Connecting...");
            await _client.LoginAsync(TokenType.Bot, config.Token);
            await _client.StartAsync();

            await Task.Delay(-1);
        }
    }
}