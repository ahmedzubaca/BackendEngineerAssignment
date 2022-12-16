using BackendEngineerAssignment.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace BackendEngineerAssignment.Models.DTO
{
    public class PostDTO
    {       
        public string? Slug { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Body { get; set; }
        public string[]? TagList { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
