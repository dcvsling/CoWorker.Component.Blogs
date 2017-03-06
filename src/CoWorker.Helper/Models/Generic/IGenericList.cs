
namespace CoWorker.Helper.Models.Generic
{
    using CoWorker.Helper.Models;
    using System;
    using System.Collections.Generic;
    public interface IGenericList : IDictionary<Type, Maybe<object>>
    {
    }
}
