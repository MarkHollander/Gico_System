using Microsoft.Extensions.Configuration;

namespace Nop.Web.Framework
{
    public class ConfigSetting
    {
        public static string DataConnectionString;

        public static void LoadConfig(IConfigurationRoot configuration)
        {
            DataConnectionString = configuration["DataConnectionString"];
        }
    }
}
