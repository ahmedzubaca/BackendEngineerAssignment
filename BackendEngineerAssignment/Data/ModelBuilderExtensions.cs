using BackendEngineerAssignment.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BackendEngineerAssignment.Data
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>().HasData(
                new Post()
                {
                    Id = 1,
                    Slug = "augmented-reality-ios-application",
                    Title = "Augmented Reality iOS Application",
                    Description = "Rubicon Software Development and Gazzda furniture are proud to launch an augmented reality app.",
                    Body = "The app is simple to use, and will help you decide on your best furniture fit.",
                    CreatedAt = DateTime.Now,
                },

                 new Post()
                 {
                     Id = 2,
                     Slug = "second-title",
                     Title = "Second Title",
                     Description = "Second post description",
                     Body = "This is the body of second post",
                     CreatedAt = DateTime.Now,
                 },
                 new Post()
                 {
                     Id = 3,
                     Slug = "third-title",
                     Title = "Third Title",
                     Description = "Third post description",
                     Body = "This is the body of third post",
                     CreatedAt = DateTime.Now,
                 }
             );

            modelBuilder.Entity<Comment>().HasData(
                new Comment()
                {
                    Id = 1,
                    CreatedAt = DateTime.Now,
                    Body = "Great blog",
                    PostSlug = "augmented-reality-ios-application",
                },
                new Comment()
                {
                    Id = 2,
                    CreatedAt = DateTime.Now,
                    Body = "First post comment",
                    PostSlug = "second-title",
                },
                new Comment()
                {
                    Id = 3,
                    CreatedAt = DateTime.Now,
                    Body = "Second post comment",
                    PostSlug = "third-title",
                }
            );

            modelBuilder.Entity<Tag>().HasData(
                new Tag()
                {
                    Id = 1,
                    Name = "iOS"
                },
                new Tag()
                {
                    Id = 2,
                    Name = "AR"
                },
                new Tag()
                {
                    Id = 3,
                    Name = "General"
                },
                new Tag()
                {
                    Id = 4,
                    Name = "Second"
                },
                new Tag()
                {
                    Id = 5,
                    Name = "Third"
                }
            );
            modelBuilder.Entity<PostTag>().HasData(
                new PostTag()
                {
                    Id = 1,
                    PostId = 1,
                    TagId = 1
                },
                new PostTag()
                {
                    Id = 2,
                    PostId = 1,
                    TagId = 2
                },
                new PostTag()
                {
                    Id = 3,
                    PostId = 1,
                    TagId = 3
                },
                new PostTag()
                {
                    Id = 4,
                    PostId = 2,
                    TagId = 3
                },
                new PostTag()
                {
                    Id = 5,
                    PostId = 2,
                    TagId = 4
                },
                new PostTag()
                {
                    Id = 6,
                    PostId = 3,
                    TagId = 3
                },
                new PostTag()
                {
                    Id = 7,
                    PostId = 3,
                    TagId = 5
                }
            );
        }
    }
}
