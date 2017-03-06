namespace CoWorker.Helper.Options
{
    using Microsoft.Extensions.Options;
    public interface IAggregateConfigureOptions<T>:IConfigureOptions<T> where T : class
    {
    }
}
