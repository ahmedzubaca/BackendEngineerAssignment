using BackendEngineerAssignment.Models.Domain;

namespace BackendEngineerAssignment.Repositories.IRepositories
{
    public interface ITagRepository
    {
        
        Task<List<int>> AddTags(List<Tag> tags);
        Task<List<Tag>> GetAllTags();
        List<Tag> GetTagsByPost();
    }
}
