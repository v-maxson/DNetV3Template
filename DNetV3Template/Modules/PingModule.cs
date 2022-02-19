using Discord.Interactions;
using Discord.WebSocket;
using Humanizer;
using Humanizer.Localisation;
using System.Diagnostics;

namespace DNetV3Template.Modules
{
    public class PingModule : InteractionModuleBase<SocketInteractionContext>
    {
        private readonly DiscordSocketClient _client;

        public PingModule(DiscordSocketClient client)
        {
            _client = client;
        }

        [SlashCommand("ping", "Replies with latency and uptime.")]
        public async Task PingAsync()
        {
            var Proc = Process.GetCurrentProcess();

            await RespondAsync($"**Client Latency:** {_client.Latency}ms\n**Uptime:** {(DateTime.Now - Proc.StartTime).Humanize(precision: 16, maxUnit: TimeUnit.Year, minUnit: TimeUnit.Second)}", ephemeral: true);
        }
    }
}