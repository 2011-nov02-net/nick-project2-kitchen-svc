using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using KitchenService.Api.Model;
using KitchenService.Api.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

        // GET: api/users/test
        [HttpGet("test")]
        [Authorize]
        public IActionResult Test()
        {
            return new JsonResult(User, new JsonSerializerOptions(JsonSerializerDefaults.Web)
            {
                ReferenceHandler = ReferenceHandler.Preserve
            });
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

        // DELETE: api/users/abc@123.com
        [HttpDelete("{email}")]
        [Authorize]
        public IActionResult DeleteUser(string theEmail)
        {
            var email = User.FindFirst(ct => ct.Type.Contains("nameidentifier")).Value;
            if (email != theEmail)
            {
                return StatusCode(StatusCodes.Status403Forbidden);
            }

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
