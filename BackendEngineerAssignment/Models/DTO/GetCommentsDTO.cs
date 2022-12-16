using BackendEngineerAssignment.Models.Domain;

namespace BackendEngineerAssignment.Models.DTO
{
    public class GetCommentsDTO
    {
        public IEnumerable<CommentDTO> Comments { get; set; }        
    }
}
