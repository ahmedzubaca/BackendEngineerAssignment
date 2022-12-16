using BackendEngineerAssignment.Models.Domain;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace BackendEngineerAssignment.Models.DTO
{
    public class AddPostDTO
    {        
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Body { get; set; }
        
        public string[]? TagList { get; set; }
    }
}
