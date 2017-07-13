
namespace CoWorker.Component.Blogs.Models.Blog.Query
{
    using System;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using CoWorker.Component.Blog.Models;

    public class GetBlogByTitle : IBlogQuery
    {
        private readonly string _title;

        public GetBlogByTitle(string title)
        {
            _title = title;
        }
        public IQueryable<Post> Get(BlogDbContext context)
            => context.Set<PostRelated>()
                  .Include(related => related.Current)
                  .Where(related => related.Current.Title == _title)
                  .Where(related => related.StartDate <= DateTime.Now)
                  .Where(related => related.EndDate >= DateTime.Now)
                  .Take(1)
                  .Select(related => related.Current);
    }
}
