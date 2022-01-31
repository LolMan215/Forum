using ForumDAL.Entities;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForumDAL
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public virtual DbSet<Forum> Forums { get; set; }

        public virtual DbSet<Post> Posts { get; set; }

        public virtual DbSet<PostHistory> PostsHistory { get; set; }

        public virtual DbSet<Comment> Comments { get; set; }

        public virtual DbSet<CommentHistory> CommentsHistory { get; set; }

        public virtual DbSet<ForumModerator> ForumsModerators { get; set; }
        /*public ApplicationDbContext()
            : base()
        {
        }*/
        public ApplicationDbContext(
           DbContextOptions options,
           IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        /*public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }*/


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            // Forum

            modelBuilder.Entity<Forum>()
                .HasKey(f => f.Id);

            modelBuilder.Entity<Forum>()
                .HasOne(f => f.Parent)
                .WithMany(f => f.SubForums)
                .HasForeignKey(f => f.ParentId);

            modelBuilder.Entity<Forum>()
                .Property(f => f.Name).IsRequired();

            modelBuilder.Entity<Forum>()
                .Property(f => f.Created).IsRequired();

            // Post

            modelBuilder.Entity<Post>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Post>()
                .HasOne(p => p.User)
                .WithMany()
                .HasForeignKey(p => p.UserId);

            modelBuilder.Entity<Post>()
                .HasOne(p => p.Forum)
                .WithMany(f => f.Posts)
                .HasForeignKey(p => p.ForumId);

            modelBuilder.Entity<Post>()
                .Property(p => p.UserId).IsRequired();

            modelBuilder.Entity<Post>()
                .Property(p => p.ForumId).IsRequired();

            modelBuilder.Entity<Post>()
                .Property(p => p.Title).IsRequired();

            modelBuilder.Entity<Post>()
                .Property(p => p.Body).IsRequired();

            modelBuilder.Entity<Post>()
                .Property(p => p.Created).IsRequired();

            // PostHistory

            modelBuilder.Entity<PostHistory>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<PostHistory>()
                .HasOne(p => p.Post)
                .WithMany()
                .HasForeignKey(p => p.PostId);

            modelBuilder.Entity<PostHistory>()
                .Property(p => p.PostId).IsRequired();

            modelBuilder.Entity<PostHistory>()
                .Property(p => p.Title).IsRequired();

            modelBuilder.Entity<PostHistory>()
                .Property(p => p.Body).IsRequired();

            modelBuilder.Entity<PostHistory>()
                .Property(p => p.Created).IsRequired();


            // Comment

            modelBuilder.Entity<Comment>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Post)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.PostId);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.NoAction);
                

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Parent)
                .WithMany(c => c.Children)
                .HasForeignKey(c => c.ParentId);

            modelBuilder.Entity<Comment>()
                .Property(c => c.PostId).IsRequired();

            modelBuilder.Entity<Comment>()
                .Property(c => c.UserId).IsRequired();


            modelBuilder.Entity<Comment>()
                .Property(c => c.Body).IsRequired();

            modelBuilder.Entity<Comment>()
                .Property(c => c.Created).IsRequired();


            // CommentHistory

            modelBuilder.Entity<CommentHistory>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<CommentHistory>()
                .HasOne(c => c.Comment)
                .WithMany()
                .HasForeignKey(c => c.CommentId);

            modelBuilder.Entity<CommentHistory>()
                .Property(c => c.CommentId).IsRequired();

            modelBuilder.Entity<CommentHistory>()
                .Property(c => c.Body).IsRequired();

            modelBuilder.Entity<CommentHistory>()
                .Property(c => c.Created).IsRequired();



            // ForumModerator

            modelBuilder.Entity<ForumModerator>()
                .HasKey(f => new { f.ForumId, f.UserId });

            modelBuilder.Entity<ForumModerator>()
                .HasOne(f => f.Forum)
                .WithMany()
                .HasForeignKey(f => f.ForumId);

            modelBuilder.Entity<ForumModerator>()
                .HasOne(f => f.User)
                .WithMany()
                .HasForeignKey(f => f.UserId);

            modelBuilder.Entity<ForumModerator>()
                .Property(f => f.UserId).IsRequired();

            modelBuilder.Entity<ForumModerator>()
                .Property(f => f.ForumId).IsRequired();

            modelBuilder.Entity<ForumModerator>()
                .Property(f => f.Created).IsRequired();

        }
    }

}

