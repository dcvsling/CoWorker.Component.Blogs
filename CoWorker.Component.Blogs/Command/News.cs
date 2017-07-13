
namespace CoWorker.Component.Blogs.Models.Blog.Query
{
    using System;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using CoWorker.Component.Blog.Models;

    public class News : GetBlogByTitle
    {
        public const string NEWS = "News";

        public News() : base(NEWS) { }
    }
}
