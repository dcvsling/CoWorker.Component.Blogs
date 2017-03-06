
namespace CoWorker.Helper.Options
{
    using Microsoft.Extensions.Options;
    using System.Collections.Generic;
    using System.Linq;
    public class AggregateConfigureOptions<T> : IAggregateConfigureOptions<T> where T : class
    {
        private IEnumerable<IConfigureOptions<T>> configures;
        public AggregateConfigureOptions(IEnumerable<IConfigureOptions<T>> configures)
        {
            this.configures = configures;
        }

        public void Configure(T options)
        {
            configures.Each(x => x.Configure(options));
        }
    }
}
