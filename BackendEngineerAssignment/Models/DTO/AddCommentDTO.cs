using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendEngineerAssignment.Models.DTO
{
    public class AddCommentDTO
    {
        [Required]
        public string Body { get; set; }
    }
}
