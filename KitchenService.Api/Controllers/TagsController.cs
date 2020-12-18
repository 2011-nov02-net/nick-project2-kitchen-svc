using System.Collections.Generic;
using System.Linq;
using KitchenService.Api.Model;
using KitchenService.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace KitchenService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly INoteRepository _noteRepository;

        public TagsController(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        // GET: api/tags
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<Tag> tags = _noteRepository.GetAllTags();
            return Ok(tags);
        }

        // GET: api/tags/asdf/notes
        [HttpGet("{tag}/notes")]
        public IActionResult GetTagNotes(string tag)
        {
            IEnumerable<Note> notes = _noteRepository.GetAll()
                .Where(n => n.Tags.Any(t => t.Name == tag));
            return Ok(notes);
        }
    }
}
