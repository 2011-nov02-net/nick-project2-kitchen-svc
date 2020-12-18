using System;
using System.Collections.Generic;
using System.Linq;
using KitchenService.Api.Model;
using KitchenService.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace KitchenService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INoteRepository _noteRepository;

        public NotesController(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        // GET: api/notes
        [HttpGet]
        public ActionResult<IEnumerable<Note>> Get([FromQuery] DateTime? since = null, [FromQuery] int count = -1)
        {
            if (count > 200)
            {
                return BadRequest("too many");
            }
            IEnumerable<Note> notes = _noteRepository.GetAll(since);
            if (count > -1)
            {
                notes = notes.Take(count);
            }
            return notes.ToList();
        }

        // GET: api/notes/5
        [HttpGet("{id}")]
        public ActionResult<Note> GetById(int id)
        {
            if (_noteRepository.GetById(id) is Note note)
            {
                return note;
            }
            return NotFound();
        }

        // TODO: support adding with tags
        // POST: api/notes
        [HttpPost]
        public IActionResult Post([FromBody] Note note)
        {
            _noteRepository.Add(note);
            // id is now set
            return CreatedAtAction(nameof(GetById), new { id = note.Id }, note);
        }

        // TODO: support changing the tags
        // PUT: api/notes/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Note note)
        {
            if (_noteRepository.GetById(id) is Note oldNote)
            {
                oldNote.DateModified = DateTime.Now;
                oldNote.Author = note.Author;
                oldNote.IsPublic = note.IsPublic;
                oldNote.Text = note.Text;
                return NoContent();
            }
            return NotFound();
        }

        // DELETE: api/notes/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (_noteRepository.GetById(id) is Note note)
            {
                _noteRepository.Remove(note);
                return NoContent();
            }
            return NotFound();
        }
    }
}
