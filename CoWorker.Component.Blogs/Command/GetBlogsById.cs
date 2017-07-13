
namespace CoWorker.Component.Blogs.Models.Blog.Query
{
    using System;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using CoWorker.Component.Blog.Models;

    public class GetBlogsById : IBlogQuery
    {
        private readonly Guid _id;

        public GetBlogsById(Guid id)
        {
            _id = id;
        }

        public IQueryable<Post> Get(BlogDbContext context)
            => context.Set<Post>()
                .Where(post => post.Id == _id);
    }
}