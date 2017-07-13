using System.Linq;
using CoWorker.Component.Blog.Models;
using Microsoft.Extensions.Logging;

namespace CoWorker.Component.Blogs.Models.Blog.Query
{
    public interface IBlogQuery
    {
        IQueryable<Post> Get(BlogDbContext context);
    }
}