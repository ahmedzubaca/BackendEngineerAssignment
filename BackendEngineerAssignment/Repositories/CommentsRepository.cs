using BackendEngineerAssignment.Data;
using BackendEngineerAssignment.Models.Domain;
using BackendEngineerAssignment.Models.DTO;
using BackendEngineerAssignment.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace BackendEngineerAssignment.Repositories
{
    public class CommentsRepository : ICommentsRepository
    {
        private readonly BlogDbContext _db;
        public CommentsRepository(BlogDbContext db)
        {
            _db = db;
        }
        public async Task<List<Comment>> GetAllComments(string slug)
        {
            var postComments = await _db.Comments.Where(c => c.PostSlug == slug).ToListAsync();
            if(postComments == null)
            {
                return null!;
            }
            return postComments!;
        }

        public async Task<Comment> AddComment(Comment comment)
        {
            await _db.Comments.AddAsync(comment);
            _db.SaveChanges();
            return comment;
        }

        public bool DeleteComment(string slug, int id)
        {
            var commentToDelete = _db.Comments.Where(c => c.PostSlug == slug).FirstOrDefault(c => c.Id == id);            
            if(commentToDelete == null)
            {
                return false;
            }
            _db.Comments.Remove(commentToDelete);
            _db.SaveChanges();
            return true;
        }
    }
}
