
namespace CoWorker.Middlewarer.Tests
{
    using System.IO;
    using System.Text;
    using Abstractions;

    /// <summary>
    /// get actual string when testclass invoke WriteTo
    /// </summary>
    public class TestStringWriter : StringWriter
    {
        private string actual;
        public TestStringWriter()
        {
            actual = string.Empty;
        }

        public override Encoding Encoding => Encoding.UTF8;

        public override void Write(string value)
        {
            actual += value;
        }

        public override string ToString()
            => actual;
    }
}
