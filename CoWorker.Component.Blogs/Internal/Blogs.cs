
namespace CoWorker.Component.Blog.Models
{
    using System.Collections;
    using System.Collections.Generic;

    public class Blogs : IEnumerable<Post>
    {
        private readonly IEnumerable<Post> _posts;
        public Blogs(IEnumerable<Post> posts)
        {
            _posts = posts;
        }
        public IEnumerator<Post> GetEnumerator()
            => _posts.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator()
            => this.GetEnumerator();
    }
}
