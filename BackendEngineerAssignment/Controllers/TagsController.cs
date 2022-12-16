using BackendEngineerAssignment.Models.Domain;
using BackendEngineerAssignment.Models.DTO;
using BackendEngineerAssignment.Repositories.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendEngineerAssignment.Controllers
{
    [Route("api/tags")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly ITagRepository _tagRepository;

        public TagsController(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTags()
        {
            var tags = await _tagRepository.GetAllTags();
            if(tags == null)
            {
                return NotFound();
            }

            List<string> stringTag = new List<string>();            

            foreach(var tag in tags)
            {
               stringTag.Add(tag.Name);
            }
            string[] tagArr = stringTag.ToArray();

            return Ok(new GetTagsDTO { Tags = tagArr});            
        }
    }
}
