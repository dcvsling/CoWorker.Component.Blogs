
namespace CoWorker.Middlewarer.Tests
{
    using CoWorker.Middlewarer;
    using Microsoft.Extensions.DependencyInjection;
    using System.IO;
    using System.Threading.Tasks;
    using Xunit;
    
    public class NewMiddlewarerTests
    {
        /// <summary>
        /// basic actual word we get from StringWriter
        /// </summary>
        public const string MainWord = "write in main class test";

        /// <summary>
        /// if middlewarer is work, i will get this word in from of main word
        /// </summary>
        public const string MiddlewareWord = "write in middleware then ";

        /// <summary>
        /// use IwriteTo interface and WriteToMiddlewarer interceptor to add middleware word in from of main word
        /// </summary>
        [Fact]
        public void test_Middlewarer()
        {
            var service = new ServiceCollection()
                .AddSingleton<IWriteTo, TestClass>()
                .WithMiddleware<IWriteTo, WriteToMiddlewarer>()
                .AddSingleton<StringWriter, TestStringWriter>()
                .BuildServiceProvider();

            var writer = service.GetService<StringWriter>();
            var myclass = service.GetServiceWithMiddleware<IWriteTo>();
            var task = Task.Run(() => myclass.WriteTo(writer));
            task.Wait();
            Assert.Equal(MiddlewareWord + MainWord, writer.ToString());
        }

        /// <summary>
        /// use TestClass itse;f and WriteToMiddlewarer interceptor to add middleware word in from of main word
        /// </summary>
        [Fact]
        public void test_middleware_without_interface()
        {
            var service = new ServiceCollection()
                .AddSingleton<TestClass>()
                .WithMiddleware<TestClass, WriteToMiddlewarerNoInterface>()
                .AddSingleton<StringWriter, TestStringWriter>()
                .BuildServiceProvider();

            var writer = service.GetService<StringWriter>();
            var myclass = service.GetServiceWithMiddleware<TestClass>();
            var task = Task.Run(() => myclass.WriteTo(writer));
            task.Wait();
            Assert.Equal(MiddlewareWord + MainWord, writer.ToString());
        }

    }
}
