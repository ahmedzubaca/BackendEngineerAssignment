using BackendEngineerAssignment.Data;
using BackendEngineerAssignment.Models;
using BackendEngineerAssignment.Models.Domain;
using BackendEngineerAssignment.Models.DTO;
using BackendEngineerAssignment.Repositories.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace BackendEngineerAssignment.Controllers
{
    [Route("api/posts")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostRepository _postRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IPostTagRepository _postTagRepository;

        public PostsController(
            IPostRepository postRepository, 
            ITagRepository tagRepository,
            IPostTagRepository postTagRepository)
        {
            _postRepository = postRepository;
            _tagRepository = tagRepository;
            _postTagRepository = postTagRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPosts([FromQuery] string? tagName)
        {            
            var posts = await _postRepository.GetAllPosts();
            if(posts == null)
            {
                return NotFound();
            }
            return Ok(posts);
        }

        [HttpGet]
        [Route("{slug}")]
        public async Task<IActionResult> GetPostBySlug(string slug)
        {
            var post = await _postRepository.GetPostBySlug(slug);
            if (post == null)
            {
                return NotFound();
            }
            return Ok(post);
        }

        [HttpPost]
        public async Task<IActionResult> AddPost([FromBody] AddPostRequestDTO request)
        {
            var addPost = request.BlogPost;            

            //add post and slug
            var addedPost = await _postRepository.AddPost(addPost);

            //add tags and get added tags Ids
            var tagsDomain = new List<Tag>();
            var tagsIds = new List<int>();
            if ((addPost.TagList?.Length ?? 0) > 0)
            {
                var tagsPosted = addPost!.TagList!.ToList();
                tagsDomain = tagsPosted.Select(tag => new Tag
                {
                    Name = tag
                }).ToList();

                tagsIds = await _tagRepository.AddTags(tagsDomain);
            }  

            // add postTag
            if(tagsIds.Count > 0)
            {
                _postTagRepository.AddPostTag(addedPost.Id, tagsIds!);
            }            

            //create return type
            var postDTO = new PostDTO()
            {
                Slug = addedPost.Slug,
                Title = addedPost.Title,
                Description = addedPost.Description,
                Body = addedPost.Body,
                TagList = addPost.TagList,
                CreatedAt = addedPost.CreatedAt
            };

            var getSinglePostDTO = new GetSinglePostDTO
            {
                BlogPost = postDTO
            };           

            return CreatedAtAction(nameof(GetPostBySlug), new {slug=getSinglePostDTO.BlogPost.Slug}, getSinglePostDTO);
        }

        [HttpPut]
        [Route("{slug}")]
        public async Task<IActionResult> UpdatePost([FromRoute] string slug, [FromBody] UpdatePostRequestDTO? request)
        {  
            if(request!.BlogPost != null)
            {
                var updatePostDTO = request.BlogPost;
                var updatedPost = await _postRepository.UpdatePost(slug, updatePostDTO);
                if (updatedPost == null)
                {
                    return NotFound();
                }

                //create return type
                var postDTO = new PostDTO()
                {
                    Slug = updatedPost.Slug,
                    Title = updatedPost.Title,
                    Description = updatedPost.Description,
                    Body = updatedPost.Body,
                    TagList = updatedPost.PostTags!.Select(postTag => postTag.Tag.Name ?? string.Empty).ToArray(),
                    CreatedAt = updatedPost.CreatedAt,
                    UpdatedAt = updatedPost.UpdatedAt
                };
                var getSinglePostDTO = new GetSinglePostDTO
                {
                    BlogPost = postDTO
                };
                return CreatedAtAction(nameof(GetPostBySlug), new { slug = getSinglePostDTO.BlogPost.Slug }, getSinglePostDTO);
            }
            return BadRequest();
        }

        [HttpDelete]
        [Route("{slug}")]
        public async Task<IActionResult>  DeletePost(string slug)
        {
            var deletedPost = await _postRepository.DeletePost(slug);
            if(deletedPost == null)
            {
                NotFound();
            }
            //delete posttag
            _postTagRepository.DeletePostTag(deletedPost!.Id);

            //create return type
            var postDTO = new PostDTO
            {
                Slug = deletedPost!.Slug,
                Title = deletedPost.Title,
                Description = deletedPost.Description,
                Body = deletedPost.Body,
                TagList = deletedPost.PostTags!.Select(postTag => postTag.Tag.Name ?? string.Empty).ToArray(),
                CreatedAt = deletedPost.CreatedAt,
                UpdatedAt = deletedPost.UpdatedAt
            };
            var getSinglePostDTO = new GetSinglePostDTO
            {
                BlogPost = postDTO
            };

            return CreatedAtAction(nameof(GetPostBySlug), new { slug = getSinglePostDTO.BlogPost.Slug }, getSinglePostDTO);           
        }
    }
}
