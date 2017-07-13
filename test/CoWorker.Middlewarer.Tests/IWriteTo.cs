
namespace CoWorker.Middlewarer.Tests
{
    using System.IO;
    using System.Threading.Tasks;
    using Abstractions;

    /// <summary>
    /// a write to interface for write word to StringWriter
    /// </summary>
    public interface IWriteTo
    {
        Task WriteTo(StringWriter writer);
    }
}
