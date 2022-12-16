using BackendEngineerAssignment.Data;
using BackendEngineerAssignment.Models.Domain;
using BackendEngineerAssignment.Models.DTO;
using BackendEngineerAssignment.Repositories.IRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Hosting;
using System.Linq;

namespace BackendEngineerAssignment.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly BlogDbContext _db;

        public PostRepository(BlogDbContext db)
        {
            _db = db;
        }
        public async Task<GetPostsDTO> GetAllPosts()
        {
            var posts = await _db.Posts.Include(x => x.PostTags!).ThenInclude(x => x.Tag).ToListAsync();
            if (!posts.Any())
            {
                return null!;
            }

            var postsDTO = posts.Select(post => new PostDTO
            {
                Slug = post.Slug,
                Title = post.Title,
                Description = post.Description,
                Body = post.Body,
                TagList = post.PostTags?.Select(postTag => postTag.Tag.Name ?? string.Empty).ToArray(),
                CreatedAt = post.CreatedAt,
                UpdatedAt = post.UpdatedAt
            }).ToList();

            return new GetPostsDTO
            {
                BlogPost = postsDTO.OrderByDescending(p => p.CreatedAt),
                PostsCount = posts.Count()
            };
        }

        public async Task<GetSinglePostDTO> GetPostBySlug(string slug)
        {
            var post = await _db.Posts.Include(x => x.PostTags!).ThenInclude(x => x.Tag).FirstOrDefaultAsync(p => p.Slug == slug);
            if (post == null)
            {
                return null!;
            }

            var postDTO = new PostDTO()
            {
                Slug = post.Slug,
                Title = post.Title,
                Description = post.Description,
                Body = post.Body,
                TagList = post.PostTags!.Select(postTag => postTag.Tag.Name ?? string.Empty).ToArray(),
                CreatedAt = post.CreatedAt,
                UpdatedAt = post.UpdatedAt
            };

            return new GetSinglePostDTO
            {
                BlogPost = postDTO
            };
        }

        public async Task<Post> AddPost(AddPostDTO addPost)
        {
            var slug = Utility.SlugFromString.StringToSlug(addPost.Title);

            var postDomain = new Post
            {
                Slug = slug,
                Title = addPost.Title,
                Description = addPost.Description,
                Body = addPost.Body,
                CreatedAt = DateTime.Now
            };
            if (!_db.Posts.Any(p => p.Slug == slug))
            {
                await _db.Posts.AddAsync(postDomain);
            }
            else
            {
                var existingSlugs = await _db.Posts.Where(p => p.Slug!.Contains(slug)).ToListAsync();
                postDomain.Slug += "-" + existingSlugs.Count().ToString();
                await _db.Posts.AddAsync(postDomain);
            }
            _db.SaveChanges();

            return postDomain;
        }

        public async Task<Post> UpdatePost(string slug, UpdatePostDTO requestedPost)
        {
            var postDb = await _db.Posts.Include(x => x.PostTags!).ThenInclude(x => x.Tag).FirstOrDefaultAsync(p => p.Slug == slug);
            if (postDb == null)
            {
                return null!;
            }
            if (requestedPost.Title != null)
            {
                slug = Utility.SlugFromString.StringToSlug(requestedPost.Title);
                var existingSlugs = await _db.Posts.Where(p => p.Slug!.Contains(slug)).ToListAsync();
                if (existingSlugs.Count() > 0)
                {
                    slug += "-" + existingSlugs.Count().ToString();
                }
                postDb.Title = requestedPost.Title;
            }

            postDb.Slug = slug;
            if (requestedPost.Description != null)
            {
                postDb.Description = requestedPost.Description;
            }
            if (requestedPost.Body != null)
            {
                postDb.Body = requestedPost.Body;
            }
            postDb.UpdatedAt = DateTime.Now;

            _db.SaveChanges();

            return postDb;
        }

        public async Task<Post> DeletePost(string slug)
        {
            var postToDelete = await _db.Posts.Include(x => x.PostTags!).ThenInclude(x => x.Tag).FirstOrDefaultAsync(p => p.Slug == slug);
            if (postToDelete == null)
            {
                return null!;
            }
            _db.Posts.Remove(postToDelete);
            await _db.SaveChangesAsync();

            return postToDelete;
        }
    }
}
