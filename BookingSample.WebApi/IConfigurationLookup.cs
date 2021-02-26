using System.Configuration;

namespace BookingSample.WebApi
{
    public interface IConfigurationLookup
    {
        string GetValue(string key);
    }

    public class ConfigurationLookup : IConfigurationLookup
    {
        public string GetValue(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}