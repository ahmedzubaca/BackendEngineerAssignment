using System.ComponentModel.DataAnnotations.Schema;

namespace BackendEngineerAssignment.Models.Domain
{
    public class Comment
    {
        public int Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string Body { get; set; }

        [ForeignKey("PostSlug")]
        public string PostSlug { get; set; }        
    }
}
