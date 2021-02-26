using BookingSample.Domain;

namespace BookingSample.WebApi
{
    public class SqlConnectionStringProvider : IConnectionStringProvider
    {
        private readonly IConfigurationLookup _configurationProvider;

        public SqlConnectionStringProvider(IConfigurationLookup configurationProvider)
        {
            _configurationProvider = configurationProvider;
        }

        public string Provide()
        {
            return _configurationProvider.GetValue("SQLConnectionString") ??
                   "Data Source=.;Initial Catalog=BookingSample;Integrated Security=true";
        }
    }
}