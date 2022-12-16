using BackendEngineerAssignment.Models.Domain;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace BackendEngineerAssignment.Models.DTO
{
    public class UpdatePostRequestDTO
    {       
        public UpdatePostDTO BlogPost { get; set; }
    }
}
