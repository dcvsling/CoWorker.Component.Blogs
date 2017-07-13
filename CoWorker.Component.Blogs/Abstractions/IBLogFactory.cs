using System.Threading.Tasks;
using CoWorker.Component.Blogs.Models.Blog.Query;

namespace CoWorker.Component.Blog.Models
{
    public interface IBlogFactory
    {
        Task<Blogs> GetBlogs(IBlogQuery query);
        Task Post(PostRelated related);
    }
}