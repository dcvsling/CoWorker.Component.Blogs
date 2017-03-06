
namespace CoWorker.Helper.Models
{
    using System;
    using System.Collections.Generic;
    public interface IFlag<T> : IEquatable<T>,IEquatable<IEnumerable<T>>,IEnumerable<T> where T:IEnumerator<T>
    {
    }
}