using System.ComponentModel.DataAnnotations;

namespace BackendEngineerAssignment.Models.Domain
{
    public class Slug
    {
        public int Id { get; set; }        
        public string Name { get; set; }
        public int SlugIdentifier { get; set; } = 0;
    }
}
