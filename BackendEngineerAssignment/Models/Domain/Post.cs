using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendEngineerAssignment.Models.Domain
{
    public class Post
    {
        public int Id { get; set; }
        public string? Slug { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Body { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [NotMapped]
        public List<string>? Tags { get; set; }
        public List<PostTag>? PostTags { get; set; }        
    }
}
