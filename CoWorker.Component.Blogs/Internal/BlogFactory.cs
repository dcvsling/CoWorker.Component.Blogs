using Microsoft.EntityFrameworkCore.Infrastructure;
using System;

namespace CoWorker.Component.Blog.Models
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using System.Threading.Tasks;
    using CoWorker.Component.Blogs.Models.Blog.Query;

    public partial class BlogFactory : IBlogFactory
    {
        private readonly BlogDbContext _context;
        private readonly IHttpContextAccessor _accessor;


        public BlogFactory(BlogDbContext context, IHttpContextAccessor accessor)
        {
            _context = context;
            _accessor = accessor;
        }
        async public Task Post(PostRelated related)
        {
            //_context.ChangeTracker.TrackGraph(related,o => o.)
            CompletePost(related);
            related.Parent = await _context.Set<PostRelated>().FindAsync(related.Parent.Current.Id);
            await _context.AddAsync(related);
            
            
            await _context.SaveChangesAsync();
        }
        private void CompletePost(PostRelated related)
        {
            var user = related.Modifier = related.Current.Modifier = new CurrentUserValueGenerator(_accessor).Next(null);
            var date = related.ModifyDate = related.Current.ModifyDate = DateTimeOffset.Now;
            related.Creator = string.IsNullOrEmpty(related.Creator) ? user : related.Creator;
            related.Current.Creator = string.IsNullOrEmpty(related.Current.Creator) ? user : related.Current.Creator;
            related.CreateDate = related.CreateDate == default(DateTimeOffset) ? date : related.CreateDate;
            related.Current.CreateDate = related.Current.CreateDate == default(DateTimeOffset) ? date : related.Current.CreateDate;
            related.EndDate = DateTime.MaxValue;
            related.StartDate = DateTime.MinValue;
        }

        public void UpperBoundTrackGraph<T>(EntityEntryGraphNode node,string memberName,Func<T> boundMemberCreator,string keyPostfix = null)
        {
            keyPostfix = keyPostfix ?? "Id";
            var property = node.Entry.Property(memberName);
            if ((Guid)node.Entry.Property($"{memberName}{keyPostfix}").CurrentValue != default(Guid))
            {
                property.CurrentValue = property.EntityEntry.GetDatabaseValues();
                var newnode = new EntityEntryGraphNode(node.Entry.GetInfrastructure(), property.EntityEntry.GetInfrastructure(), node.InboundNavigation);
                UpperBoundTrackGraph<T>(newnode, memberName, boundMemberCreator, keyPostfix);
            }
            else
            {
                node.Entry.Property("Parent").CurrentValue = boundMemberCreator();
            }
        }

        async public Task<Blogs> GetBlogs(IBlogQuery query)
            =>  new Blogs(await query.Get(_context).ToArrayAsync());
    }
}
