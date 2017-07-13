
namespace CoWorker.Middlewarer.Tests
{
    using System.IO;
    using System.Threading.Tasks;
    using Abstractions;

    /// <summary>
    /// it's what i intercept class and method
    /// </summary>
    public class TestClass : IWriteTo
    {
        async public virtual Task WriteTo(StringWriter writer)
            => await writer.WriteAsync(NewMiddlewarerTests.MainWord);
    }
}
