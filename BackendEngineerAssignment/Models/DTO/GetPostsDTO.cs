using BackendEngineerAssignment.Models.Domain;

namespace BackendEngineerAssignment.Models.DTO
{
    public class GetPostsDTO
    {
        public IEnumerable<PostDTO> BlogPost { get; set; }

        public int PostsCount { get; set; }
    }
}
