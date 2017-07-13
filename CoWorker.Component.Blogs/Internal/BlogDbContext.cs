using System.Reflection;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Immutable;
namespace CoWorker.Component.Blog.Models
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Microsoft.EntityFrameworkCore.ValueGeneration;
    using System;
    using System.Linq;
    using CoWorker.Component.Blogs.Models.Blog;

    public class BlogDbContext : DbContext
    {
        private const string CURRENT_ID = "CurrentId";
        private const string PARENT_ID = "ParentId";
        private const string USER_ID = "UserId";
        public BlogDbContext() { }
        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder
                .EnableSensitiveDataLogging(true)
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .UseSqlServer(@"Data Source=(localdb)\ProjectsV13;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            
            modelBuilder.Entity<PostRelated>(
                related =>
                {
                    related.Property<Guid>(CURRENT_ID);
                    related.HasKey(CURRENT_ID);

                    
                    //related.HasOne(r => r.Parent)
                    //    .WithOne()
                    //    .HasForeignKey<Post>(p => p.Id)
                    //    .HasPrincipalKey<PostRelated>("ParentId")
                    //    .OnDelete(DeleteBehavior.Restrict);

                    related.HasOne(r => r.Current)
                        .WithOne()
                        .HasForeignKey<Post>(p => p.Id)
                        .HasPrincipalKey<PostRelated>(CURRENT_ID)
                        .OnDelete(DeleteBehavior.Restrict);

                    related.Property<Guid>(PARENT_ID)
                        .IsRequired()
                        .HasDefaultValue(default(Guid))
                        .ValueGeneratedNever();
                    related.HasIndex(PARENT_ID);
                    related.HasOne(r => r.Parent)
                        .WithOne()
                        .HasForeignKey<PostRelated>(CURRENT_ID)
                        .HasPrincipalKey<PostRelated>(PARENT_ID)
                        .OnDelete(DeleteBehavior.Restrict);

                    related.HasMany(rs => rs.Posts)
                        .WithOne(pr => pr.Parent)
                        .HasForeignKey(CURRENT_ID)
                        .HasPrincipalKey(PARENT_ID)
                        .OnDelete(DeleteBehavior.Restrict);

                    related.Property<Guid>(USER_ID)
                        .IsRequired()
                        .HasDefaultValue(default(Guid))
                        .ValueGeneratedNever();
                    related.HasIndex(USER_ID);
                    related.HasOne(r => r.User)
                        .WithOne()
                        .HasForeignKey<User>(user => user.Id)
                        .HasPrincipalKey<PostRelated>(USER_ID)
                        .OnDelete(DeleteBehavior.Restrict);

                    related.ToTable("Related");
                });

            modelBuilder.Entity<Post>(post =>
            {
                post.HasKey(p => p.Id);
                post.Property(p => p.Id).ValueGeneratedOnAdd().HasValueGenerator<GuidValueGenerator>();

                //post.HasOne(p => p.Related)
                //       .WithOne(r => r.Current)
                //       .HasForeignKey<PostRelated>("CurrentId")
                //       .HasPrincipalKey<Post>(p => p.Id)
                //       .OnDelete(DeleteBehavior.Restrict);
                
                post.ToTable("Post");
            });
            base.OnModelCreating(modelBuilder);
            modelBuilder.Ignore<User>();
            modelBuilder.Ignore<EventTimer>();
        }
    }


}
