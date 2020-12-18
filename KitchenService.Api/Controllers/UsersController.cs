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
    public class UsersController : ControllerBase
    {
        private readonly INoteRepository _noteRepository;

        public UsersController(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        // GET: api/users
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<User> users = _noteRepository.GetAllUsers();
            return Ok(users);
        }

        // GET: api/users/5
        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            if (_noteRepository.GetAllUsers().FirstOrDefault(u => u.Id == id) is User user)
            {
                return Ok(user);
            }
            return NotFound();
        }

        // DELETE: api/users/5
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        // GET: api/users/5/notes
        [HttpGet("{id}/notes")]
        public IActionResult GetUserNotes(int id)
        {
            IEnumerable<Note> notes;
            try
            {
                notes = _noteRepository.GetAllByUser(id);
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
            return Ok(notes);
        }
    }
}
