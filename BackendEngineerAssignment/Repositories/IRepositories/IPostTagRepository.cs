namespace BackendEngineerAssignment.Repositories.IRepositories
{
    public interface IPostTagRepository
    {
        void AddPostTag(int postId, List<int> tagsIds);
        void DeletePostTag(int postId);
    }
}
