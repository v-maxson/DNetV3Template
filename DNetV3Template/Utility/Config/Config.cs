using Newtonsoft.Json;

namespace DNetV3Template
{
    internal class Config
    {
        [JsonProperty("token")]
        public string Token { get; set; } = "TOKEN HERE"; // <-- This is a default, do not put your token here.

        [JsonProperty("prefix")]
        public string Prefix { get; set; } = "!";

        [JsonProperty("register_commands_globally")]
        public bool RegisterCommandsGlobally { get; set; } = false;

        [JsonProperty("main_guild")]
        public ulong MainGuild { get; set; } = 0;

        public static Config LoadConfig()
        {
            var _logger = new Logger();

            if (!Directory.Exists("./config"))
            {
                try
                {
                    Directory.CreateDirectory("./config");
                }
                catch (Exception e)
                {
                    _logger.Error("Could not load Config...", exception: e);
                }
            }

            if (!File.Exists("./config/Config.json"))
            {
                try
                {
                    var newConfig = new Config();
                    File.WriteAllText("./config/Config.json", JsonConvert.SerializeObject(newConfig, Formatting.Indented));
                }
                catch (Exception e)
                {
                    _logger.Error("Could not load Config...", exception: e);
                }
            }

            try
            {
                var conf = JsonConvert.DeserializeObject<Config>(File.ReadAllText("./config/Config.json"));
                File.WriteAllText("./config/Config.json", JsonConvert.SerializeObject(conf, Formatting.Indented));

                return conf;
            }
            catch (Exception e)
            {
                _logger.Error("Could not load Config...", exception: e);
                throw;
            }
        }
    }
}