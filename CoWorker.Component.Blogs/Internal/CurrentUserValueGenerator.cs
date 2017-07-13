using System.Security.Claims;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
namespace CoWorker.Component.Blog.Models
{
    using CoWorker.Component.Blogs.Models.Blog;
    using Microsoft.EntityFrameworkCore;
    using JetBrains.Annotations;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using Microsoft.AspNetCore.Http;

    public class CurrentUserValueGenerator : ValueGenerator<string>
    {
        private readonly IHttpContextAccessor _accessor;

        public CurrentUserValueGenerator(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public override Boolean GeneratesTemporaryValues => false;

        public override String Next(EntityEntry entry)
        {
            var context = _accessor.HttpContext;
            return context.User.Identity.IsAuthenticated ? context.User.FindFirst(ClaimTypes.Sid).Value : GetIp46(context);
        }
        private string GetIp46(HttpContext context)
            => $"{context.Connection.LocalIpAddress.MapToIPv4()}|{context.Connection.LocalIpAddress.MapToIPv6()}";
    }


}
