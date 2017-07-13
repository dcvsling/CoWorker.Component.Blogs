
namespace CoWorker.Component.Blogs.Models.Blog.Query
{
    using System;
    using CoWorker.Component.Blog.Models;

    public static class BlogExtensions
    {
        public static PostRelated WithParent(this PostRelated related, Guid parentId = default(Guid))
        {
            related.Parent = new PostRelated()
            {
                Current = new Post() { Id = parentId }
            };
            return related;
        }
    }
}
