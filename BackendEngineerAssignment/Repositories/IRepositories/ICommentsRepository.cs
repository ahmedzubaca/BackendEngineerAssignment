using BackendEngineerAssignment.Models.Domain;
using BackendEngineerAssignment.Models.DTO;

namespace BackendEngineerAssignment.Repositories.IRepositories
{
    public interface ICommentsRepository
    {
        Task<List<Comment>> GetAllComments(string slug);
        Task<Comment> AddComment(Comment comment);
        bool DeleteComment(string slug, int id);
    }
}
