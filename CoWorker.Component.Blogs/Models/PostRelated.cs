using System.Collections;

namespace CoWorker.Component.Blog.Models
{
    using System;
    using System.Collections.Generic;
    using CoWorker.Component.Blogs.Models.Blog;

    public class PostRelated : IEntity
    {
        //public virtual Guid Id { get; set; }
        public virtual User User { get; set; }
        public virtual Post Current { get; set; }
        public virtual PostRelated Parent { get; set; }
        public virtual IEnumerable<PostRelated> Posts { get; set; }
        //public virtual EventTimer Visible { get; set; }
        public Int32 Level { get; set; }
        public virtual DateTimeOffset CreateDate { get; set; }
        public virtual DateTimeOffset ModifyDate { get; set; }
        public virtual string Creator { get; set; }
        public virtual string Modifier { get; set; }
        public virtual DateTime StartDate { get; set; }
        public virtual DateTime EndDate { get; set; }
    }
}
