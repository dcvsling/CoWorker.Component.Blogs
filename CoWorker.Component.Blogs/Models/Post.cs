
namespace CoWorker.Component.Blog.Models
{
    using System;
    public class Post : IEntity
    {
        public String Title { get; set; }
        public String Description { get; set; }
        public String Content { get; set; }
        public String Source { get; set; }
        //public PostRelated Related { get; set; }
        public virtual Guid Id { get; set; }
        public virtual DateTimeOffset CreateDate { get; set; }
        public virtual DateTimeOffset ModifyDate { get; set; }
        public virtual string Creator { get; set; }
        public virtual string Modifier { get; set; }
    }
}
