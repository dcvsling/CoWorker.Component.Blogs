using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;
namespace CoWorker.Component.Blog.Models
{
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    public class CurrentDateTimeValueGenerator : ValueGenerator<DateTimeOffset>
    {
        public override Boolean GeneratesTemporaryValues => false;

        public override DateTimeOffset Next(EntityEntry entry)
            => DateTimeOffset.Now;
    }


}
