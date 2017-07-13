
namespace CoWorker.Middlewarer.Tests
{
    using System.IO;
    using System.Threading.Tasks;

    /// <summary>
    /// it's interceptor class,use interface to intercept then write string
    /// </summary>
    public class WriteToMiddlewarer : IWriteTo
    {
        private IWriteTo writeTo;
        public WriteToMiddlewarer(IWriteTo writeTo)
        {
            this.writeTo = writeTo;
        }

        async public Task WriteTo(StringWriter writer)
        {
            await writer.WriteAsync(NewMiddlewarerTests.MiddlewareWord);
            await writeTo.WriteTo(writer);
        }
    }

    /// <summary>
    /// it's interceptor class, use it's self to write string
    /// </summary>
    public class WriteToMiddlewarerNoInterface : TestClass
    {
        private TestClass testclass;
        public WriteToMiddlewarerNoInterface(TestClass testclass)
        {
            this.testclass = testclass;
        }

        async public override Task WriteTo(StringWriter writer)
        {
            await writer.WriteAsync(NewMiddlewarerTests.MiddlewareWord);
            await testclass.WriteTo(writer);
        }
    }
}
