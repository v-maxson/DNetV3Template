using Discord;

namespace DNetV3Template
{
    internal static class LogMessageExtensions
    {
        public static string Stringify(this LogMessage @this)
        {
            var time = DateTime.Now;
            return $"[{time.ToShortDateString()} {time.ToShortTimeString()}][{@this.Severity}][{@this.Source}] {@this.Message}{(@this.Exception == null ? "" : $" | {@this.Exception}")}";
        }
    }
}