
namespace CoWorker.Middlewarer.Abstractions
{
    /// <summary>
    /// return complete service
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IMiddlewarerOptions<T>
    {
        T Value { get; }
    }
}
