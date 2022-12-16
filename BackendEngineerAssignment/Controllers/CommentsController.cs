using BackendEngineerAssignment.Data;
using BackendEngineerAssignment.Models.Domain;
using BackendEngineerAssignment.Models.DTO;
using BackendEngineerAssignment.Repositories.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace BackendEngineerAssignment.Controllers
{
    [Route("api/posts/{slug}/comments")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentsRepository _commentsRepository;
        
        public CommentsController(ICommentsRepository commentsRepository)
        {       
            _commentsRepository = commentsRepository;
        }

        [HttpGet] 
        public async Task<IActionResult> GetComments([FromRoute] string slug)
        {
            var comments = await _commentsRepository.GetAllComments(slug);
            if(comments == null)
            {
                return NotFound(); 
            }            

            var commentsDTO = comments.Select( comment => new CommentDTO
            {
                Id = comment.Id,
                CreatedAt = comment.CreatedAt,
                UpdatedAt = comment.UpdatedAt,
                Body = comment.Body,
            }).ToList();

            return Ok( new GetCommentsDTO { Comments = commentsDTO });
        }

        [HttpPost]        
        public async Task<IActionResult> AddComment(string slug, AddCommentRequestDTO request)
        {
            var addComment = request.Comment;
            var commentDomain = new Comment
            {
                CreatedAt = DateTime.Now,
                UpdatedAt = null,
                Body = addComment.Body,
                PostSlug = slug,
            };
            var addedComment = await _commentsRepository.AddComment(commentDomain);
            if(addedComment == null)
            {
                return BadRequest();
            }

            var commentDTO = new CommentDTO
            {
                Id = addedComment.Id,
                CreatedAt = addedComment.CreatedAt,
                UpdatedAt = addedComment.UpdatedAt,
                Body = addedComment.Body                
            };

            return Ok(new GetSingleCommentDTO { Comment = commentDTO });
        }

        [HttpDelete]
        [Route("{id}")]

        public IActionResult DeleteComment([FromRoute] string slug, int id)
        {
            var isDeleted = _commentsRepository.DeleteComment(slug, id);
            if(!isDeleted)
            {
                return BadRequest();
            }
            return Ok();
        }        
    }
}
