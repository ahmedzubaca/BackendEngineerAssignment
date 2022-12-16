using BackendEngineerAssignment.Models.Domain;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BackendEngineerAssignment.Models.DTO
{
    public class AddPostRequestDTO
    {        
        public AddPostDTO BlogPost { get; set; }
    }
}
