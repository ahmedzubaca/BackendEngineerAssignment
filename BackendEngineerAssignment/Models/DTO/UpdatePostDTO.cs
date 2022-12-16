using BackendEngineerAssignment.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace BackendEngineerAssignment.Models.DTO
{
    public class UpdatePostDTO
    { 
        public string? Title { get; set; }        
        public string? Description { get; set; }        
        public string? Body { get; set; }
    }
}
