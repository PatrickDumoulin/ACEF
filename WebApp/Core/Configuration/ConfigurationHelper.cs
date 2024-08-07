namespace WebApp.Core.Configuration
{
    public static class ConfigurationHelper
    {
        public static void Initialize(IConfiguration Configuration)
        {
            Config = Configuration;
        }

        public static IConfiguration Config { get; private set; }
    }
}
