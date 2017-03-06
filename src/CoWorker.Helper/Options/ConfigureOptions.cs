
namespace CoWorker.Helper.Options
{
    using Microsoft.Extensions.Options;
    using Microsoft.Extensions.Configuration;

    using System;
    public class ConfigureOptions<T> : IConfigureOptions<T> where T : class
    {
        private IConfiguration config;
        public ConfigureOptions(IConfiguration config)
        {
            this.config = config;
        }

        public void Configure(T options)
            => config.Bind(options);
    }
}
